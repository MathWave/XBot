using System;
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
            Text = "Настроить",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };
        Button ignore = new Button
        {
            Text = "Пропустить",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };
        public Welcome()
        {
            ok.Clicked += OkClick;
            ignore.Clicked += IgnoreClick;
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
                                "Привет, я XBot! Я буду присылать тебе новости, но на этом мои функции не ограничиваются!\n\n" +
                                "Еще я умею обрабатывать запросы, сохранять их, вычислять курс валют, а также ограничивать доступ к ненужному контенту.\n\n" +
                                "Я использую открытое API Mediametrics и центрального банка России, за что им огромное спасибо!\n\n" +
                                "Я являюсь некоммерческим проектом и создан в образовательных целях. Если у тебя возникли замечания или предложения, напиши моему создателю: emmtvv@icloud.com.\n\n" +
                                "Нажми на кнопку \"Настроить\", чтобы я работал максимально комфортно для тебя!\n\n" +
                                "Приятного пользования!",
                                TextColor = Styles.UserColor,
                                FontSize = 18
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
            await Navigation.PushAsync(new XBot.SettingsOnStart.Intro1());
        }

        async void IgnoreClick(object sender, EventArgs e)
        {
            App.Current.Properties["welcome"] = false;
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(Navigation.NavigationStack[0]);
        }
    }
}