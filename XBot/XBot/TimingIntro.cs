﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class TimingIntro : ContentPage
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
        public TimingIntro()
        {
            if (Device.RuntimePlatform == "iOS")
                Title = "Область поиска";
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
                                "Добро пожаловать в раздел \"Область поиска\"!\n\n" +
                                "В этом разделе можно выбрать промежуток времени, в который будет проводиться поиск.\n\n" +
                                "Обратите внимание, что при выборе большого промежутка, новости могут быть устаревшими.",
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
            await Navigation.PushAsync(new Timing());
            Navigation.RemovePage(Navigation.NavigationStack[2]);
        }

        async void IgnoreClick(object sender, EventArgs e)
        {
            App.Current.Properties["frequency_intro"] = false;
            await Navigation.PushAsync(new Timing());
            Navigation.RemovePage(Navigation.NavigationStack[2]);
        }
    }
}