﻿using System;
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
        public ControlIntro()
        {
            if (Device.RuntimePlatform == "iOS")
                Title = "Родительский контроль";
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
                                "Добро пожаловать в раздел \"Родительский контроль\"!\n\n" +
                                "В этом разделе можно добавить ключевые слова, по которым будет игнорироваться поиск.\n\n" +
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