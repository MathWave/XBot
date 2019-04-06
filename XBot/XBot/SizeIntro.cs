using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class SizeIntro : ContentPage
    {
        MainPage main;
        Button ok = new Button
        {
            Text = "OK",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Button ignore = new Button
        {
            Text = "Больше не показывать",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        public SizeIntro(MainPage m)
        {
            if (Device.RuntimePlatform == "iOS")
                Title = "Размер шрифта";
            else
                NavigationPage.SetHasNavigationBar(this, false);
            main = m;
            ok.Clicked += OkClick;
            ignore.Clicked += IgnoreClick;
            BackgroundColor = Styles.BackColor;
            Content = new StackLayout
            {
                Children =
                {
                    new ScrollView
                    {
                        Content = new Frame
                        {
                            Content = new Label
                            {
                                Text =
                                "Добро пожаловать в раздел \"Размер шрифта\"!\n\n" +
                                "В этом разделе можно выбрать размер шрифта сообщений в чате.\n\n" +
                                "Обратите внимание, что размер шрифта в первую очередь зависит от разрешения экрана.",
                                TextColor = Styles.UserColor,
                                FontSize = 24
                            },
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = Styles.BackColor,
                            HasShadow = false
                        },
                        VerticalOptions = LayoutOptions.FillAndExpand
                    },
                    new StackLayout
                    {
                        Children = {ignore, ok},
                        Orientation = StackOrientation.Horizontal
                    }
                }
            };
        }

        async void OkClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Size(main));
            Navigation.RemovePage(Navigation.NavigationStack[2]);
        }

        async void IgnoreClick(object sender, EventArgs e)
        {
            App.Current.Properties["size_intro"] = false;
            await Navigation.PushAsync(new Size(main));
            Navigation.RemovePage(Navigation.NavigationStack[2]);
        }

    }
}