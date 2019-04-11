using System;

using Xamarin.Forms;

namespace XBot.SettingsOnStart
{
    public class Intro7 : ContentPage
    {
        Button ok = new Button
        {
            Text = "Начать!",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };
        public Intro7()
        {
            ok.Clicked += OkClick;
            NavigationPage.SetHasNavigationBar(this, false);
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
                                "Моя настройка закончена и я готов к работе!",
                                TextColor = Styles.UserColor,
                                FontSize = 18
                            },
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = Styles.BackColor,
                            HasShadow = false
                        },
                        VerticalOptions = LayoutOptions.FillAndExpand
                    },
                    ok
                }
            };
        }

        async void OkClick(object sender, EventArgs e)
        {
            App.Current.Properties["welcome"] = false;
            await Navigation.PushAsync(new MainPage());
            for (int i = 0; i < 8; i++)
                Navigation.RemovePage(Navigation.NavigationStack[0]);

        }

    }
}

