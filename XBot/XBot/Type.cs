using System;

using Xamarin.Forms;

namespace XBot
{
    public class Type : ContentPage
    {
        string t = "\nТематика новостей\n";
        public Type()
        {
            if (Device.RuntimePlatform == "iOS")
            {
                Title = "Тематика новостей";
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
                subs = new string[] { "     Общие", "     LifeStyle", "     Hi-tech", "     Спорт", "     Бизнес" };
            else
                subs = new string[] { "Общие", "LifeStyle", "Hi-tech", "Спорт", "Бизнес" };
            string[] code = new string[] { "", "magazine/", "hitech/", "sport/", "business/" };
            for (int i = 0; i < subs.Length; i++)
            {
                Button button = new Button
                {
                    FontSize = 20,
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
                    App.Current.Properties["type"] = code[int.Parse(((Button)sender).ClassId)];
                    MakeContent();
                };
                Frame newf = new Frame
                {
                    BorderColor = Styles.UserColor,
                    BackgroundColor = Styles.BackColor,
                    CornerRadius = 30,
                    Content = button,
                    HasShadow = false
                };
                if ((string)App.Current.Properties["type"] == code[i])
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

