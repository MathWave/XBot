using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class ControlIntro : ContentPage
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
        public ControlIntro()
        {
            ok.Clicked += OkClick;
            ignore.Clicked += IgnoreClick;
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Colors.BackColor;
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
                                "Добро пожаловать в раздел \"Родительский контроль\"!\n\n" +
                                "В этом разделе можно добавить ключевые слова, по которым будет игнорироваться поиск.\n\n" +
                                "Обратите внимание, что если ключевое слово внесено в родительский контроль, система поиска будет его игнорировать.",
                                TextColor = Colors.UserColor,
                                FontSize = 24
                            },
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = Colors.BackColor,
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
            await Navigation.PushAsync(new Password());
            Navigation.RemovePage(Navigation.NavigationStack[2]);
        }

        async void IgnoreClick(object sender, EventArgs e)
        {
            App.Current.Properties["control_intro"] = false;
            await Navigation.PushAsync(new Password());
            Navigation.RemovePage(Navigation.NavigationStack[2]);
        }
    }
}