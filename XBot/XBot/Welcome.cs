﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class Welcome : ContentPage
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
        public Welcome()
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
                            "Привет, я XBot! Я буду присылать тебе новости, но на этом мои функции не ограничиваются!\n\n" +
                            "Еще я умею обрабатывать запросы, сохранять их, вычислять курс валют, а также ограничивать доступ к ненужному контенту.\n\n" +
                            "Я использую открытое API Mediametrics и центрального банка России, за что им огромное спасибо!\n\n" +
                            "Я являюсь некоммерческим проектом и создан в образовательных целях. Если у тебя возникли замечания или предложения, напиши моему создателю: emmtvv@icloud.com.\n\n" +
                            "Приятного пользования!",
                            TextColor = Colors.UserColor,
                            FontSize = 18
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
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(Navigation.NavigationStack[0]);
        }

        async void IgnoreClick(object sender, EventArgs e)
        {
            App.Current.Properties["welcome"] = false;
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(Navigation.NavigationStack[0]);
        }
    }
}