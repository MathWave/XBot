using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class SubscribesIntro : ContentPage
    {

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
        public SubscribesIntro()
        {
            if (Device.RuntimePlatform == "iOS")
                Title = "Мои подписки";
            else
                NavigationPage.SetHasNavigationBar(this, false);
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
                                "Добро пожаловать в раздел \"Мои подписки\"!\n\n" +
                                "В этом разделе можно добавить ключевые слова, по которым будет автоматически проводиться поиск.\n\n" +
                                "Обратите внимание, что если ключевое слово внесено в родительский контроль, система поиска будет его игнорировать.",
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
            await Navigation.PushAsync(new Subscribes());
            Navigation.RemovePage(Navigation.NavigationStack[2]);
        }

        async void IgnoreClick(object sender, EventArgs e)
        {
            App.Current.Properties["subscribes_intro"] = false;
            await Navigation.PushAsync(new Subscribes());
            Navigation.RemovePage(Navigation.NavigationStack[2]);
        }

    }
}