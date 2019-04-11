using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class Size : ContentPage
    {

        MainPage main;
        string t = "\nРазмер шрифта\n";

        public Size(MainPage m)
        {
            main = m;
            if (Device.RuntimePlatform == "iOS")
            {
                Title = "Размер шрифта";
                t = "";
            }
            else
                NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Styles.BackColor;
            MakeContent();
        }

        void MakeContent()
        {
            StackLayout sl = new StackLayout();
            string[] subs = new string[] { "очень мелкий", "мелкий", "средний", "большой", "очень большой" };
            int[] code = new int[] { 8, 10, 14, 18, 24 };
            for (int i = 0; i < subs.Length; i++)
            {
                Button button = new Button
                {
                    FontSize = code[i],
                    ClassId = i.ToString(),
                    FontAttributes = FontAttributes.Bold,
                    BorderColor = Styles.UserColor,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Styles.BackColor,
                    Text = subs[i],
                    TextColor = Styles.UserColor,
                    CornerRadius = 30
                };
                button.Clicked += (object sender, EventArgs e) =>
                {
                    App.Current.Properties["size"] = code[int.Parse(((Button)sender).ClassId)];
                    MakeContent();
                    main.Display();
                };
                Frame newf = new Frame
                {
                    BorderColor = Styles.UserColor,
                    BackgroundColor = Styles.BackColor,
                    CornerRadius = 30,
                    Content = button,
                    HasShadow = false
                };
                if ((int)App.Current.Properties["size"] == code[i])
                {
                    button.TextColor = Color.White;
                    button.BackgroundColor = Styles.UserColor;
                    newf.BackgroundColor = Styles.UserColor;
                }
                sl.Children.Add(newf);
            }
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = t,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        TextColor = Styles.UserColor,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Styles.BackColor
                    },
                    new ScrollView { Content = sl }
                }
            };
        }

    }
}