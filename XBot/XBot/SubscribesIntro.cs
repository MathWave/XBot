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
            BackgroundColor = Colors.BackColor,
            TextColor = Colors.UserColor,
            BorderColor = Colors.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Button ignore = new Button
        {
            Text = "Больше не показывать",
            BackgroundColor = Colors.BackColor,
            TextColor = Colors.UserColor,
            BorderColor = Colors.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        public SubscribesIntro()
        {
            ok.Clicked += OkClick;
            ignore.Clicked += IgnoreClick;
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Colors.BackColor;
            Content = new StackLayout
            {
                Children =
                {
                    new Frame
                    {
                        Content = new Label
                        {
                            Text =
                            "Добро пожаловать в раздел \"Мои подписки\"!\n\n" +
                            "В этом разделе можно добавить ключевые слова, по которым будет автоматически проводиться поиск.\n\n" +
                            "Обратите внимание, что если ключевое слово внесено в родительский контроль, система поиска будет его игнорировать.",
                            TextColor = Colors.UserColor,
                            FontSize = 24
                        },
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        BackgroundColor = Colors.BackColor,
                        HasShadow = false
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