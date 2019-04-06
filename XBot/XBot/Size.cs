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
            string[] subs;
            if (Device.RuntimePlatform == "iOS")
            {
                subs = new string[] { "     очень мелкий", "     мелкий", "     средний", "     большой", "     очень большой" };
            }
            else
            {
                subs = new string[] { "очень мелкий", "мелкий", "средний", "большой", "очень большой" };
            }
            int[] code = new int[] { 8, 10, 14, 18, 24 };
            for (int i = 0; i < subs.Length; i++)
            {
                Button button = new Button
                {
                    FontSize = 20,
                    ClassId = i.ToString(),
                    FontAttributes = FontAttributes.Bold,
                    BorderColor = Styles.UserColor,
                    HorizontalOptions = LayoutOptions.Start,
                    BackgroundColor = Styles.BackColor
                };
                button.Clicked += (object sender, EventArgs e) =>
                {
                    App.Current.Properties["size"] = code[int.Parse(((Button)sender).ClassId)];
                    MakeContent();
                    main.Display();
                };
                if (Device.RuntimePlatform == "iOS")
                {
                    button.Text = " ";
                    button.BackgroundColor = (int)App.Current.Properties["size"] == code[i] ? Color.Green : Styles.UserColor;
                }
                else
                {
                    button.Text = "✔";
                    button.TextColor = (int)App.Current.Properties["size"] == code[i] ? Color.Green : Styles.UserColor;
                }
                Frame newf = new Frame
                {
                    BorderColor = Styles.UserColor,
                    BackgroundColor = Styles.BackColor,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            button,
                            new Label
                            {
                                Text = subs[i],
                                TextColor = Styles.UserColor,
                                HorizontalTextAlignment = TextAlignment.Start,
                                VerticalTextAlignment = TextAlignment.Center
                            }
                        },
                        Orientation = StackOrientation.Horizontal
                    }
                };
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