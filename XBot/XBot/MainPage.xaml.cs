﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using System.Drawing;



namespace XBot
{

    public partial class MainPage : ContentPage
    {
        Frame frame;
        Entry message;
        ScrollView scroll;
        StackLayout stack;
        Button settings;
        Button news;
        Button subscribes;
        Button currency;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            message = new Entry
            {
                Placeholder = "Сообщение",
                PlaceholderColor = Xamarin.Forms.Color.LightSkyBlue,
                FontSize = 20,
                BackgroundColor = Colors.BackColor,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Colors.UserColor
            };
            message.Completed += ButtonClick;
            settings = new Button
            {
                Text = "⚙️",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Colors.BackColor,
                WidthRequest = 50
            };
            subscribes = new Button
            {
                Text = "🤵",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Colors.BackColor,
                WidthRequest = 50
            };
            news = new Button
            {
                Text = "🔝",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Colors.BackColor,
                WidthRequest = 50
            };
            currency = new Button
            {
                Text = "📈",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Colors.BackColor,
                WidthRequest = 50
            };
            news.Clicked += NewsClick;
            settings.Clicked += SettingsClick;
            subscribes.Clicked += SubscribesClick;
            currency.Clicked += CurrencyClick;
            string line = (string)App.Current.Properties["messages"];
            if (line.Length == 0)
                frame = new Frame { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Colors.BackColor };
            else
                MakeFrame();
            MakeContent();
            if ((string)App.Current.Properties["onstart"] == "news")
                NewsClick(new object(), new EventArgs());
            else if ((string)App.Current.Properties["onstart"] == "subscribes")
                SubscribesClick(new object(), new EventArgs());
            else
                CurrencyClick(new object(), new EventArgs());

        }

        public void Active(bool act)
        {
            message.IsEnabled = act;
            settings.IsEnabled = act;
            subscribes.IsEnabled = act;  
            news.IsEnabled = act;
            currency.IsEnabled = act;
        }

        private void SubscribesClick(object sender, EventArgs e)
        {
            if ((string)App.Current.Properties["subscribes"] == "")
            {
                Chat.Add("Список подписок пуст! Заполните его в настройках!", true);
                Display();
            }
            else
            {
                Chat.Add("Ищу подписки...", true);
                Display();
                Bot.Search(this, Formats.FromStringIntoList((string)App.Current.Properties["subscribes"]), true);
            }

        }

        private void SettingsClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings(this));
        }

        void CurrencyClick(object sender, EventArgs e)
        {
            try
            {
                Chat.Add("Вычисляю курс валют...", true);
                Display();
                Bot.Currency(this);
            }
            catch { }
        }

        private void NewsClick(object sender, EventArgs e)
        {
            try
            {
                Chat.Add("Ищу новости...", true);
                Display();
                Bot.GetNews(this);
            }
            catch { }
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            List<string> mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
            if (message.Text == null || message.Text.Length == 0)
                return;
            else
            {
                Chat.Add(message.Text, false);
                Chat.Add($"Выполняю поиск по запросу \"{message.Text}\"...", true);
                Display();
                Bot.Search(this, message.Text.Split(' '), false);
            }
            message.Text = "";
        }

        public void Display()
        {
            MakeFrame();
            MakeContent();
        }

        public void MakeContent()
        {
            Content = new StackLayout
            {
                Children =
                {
                    frame,
                    message,
                    new StackLayout
                    {
                        Children =
                        {
                            settings,
                            subscribes,
                            news,
                            currency
                        },
                        Orientation = StackOrientation.Horizontal
                    },
                }
            };
            BackgroundColor = Colors.BackColor;
            message.TextColor = Colors.UserColor;
            message.BackgroundColor = Colors.BackColor;
            settings.BackgroundColor = Colors.BackColor;
            subscribes.BackgroundColor = Colors.BackColor;
            news.BackgroundColor = Colors.BackColor;
            currency.BackgroundColor = Colors.BackColor;
            if (scroll != null)
                scroll.ScrollToAsync(stack, ScrollToPosition.End, false);
        }

        public void MakeFrame()
        {
            List<string> mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
            frame = new Frame { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Colors.BackColor };
            stack = new StackLayout { VerticalOptions = LayoutOptions.End };
            for (int i = 0; i < mes.Count; i++)
            {
                Frame f = new Frame { BackgroundColor = Colors.BackColor };
                if (mes[i][0] == 'U')
                    f = new Frame
                    {
                        Content = new Label { TextColor = Colors.UserColor, Text = mes[i].Substring(1), BackgroundColor = Colors.BackColor },
                        BorderColor = Colors.UserColor,
                        HorizontalOptions = LayoutOptions.End,
                        BackgroundColor = Colors.BackColor
                    };
                else
                {
                    if (mes[i].Split('֍').Length == 1)
                    {
                        Label l = new Label { TextColor = Colors.BotColor, Text = mes[i].Substring(1), BackgroundColor = Colors.BackColor };
                        /*
                        if (mes[i].Contains('$') && mes[i].Contains('€'))
                            l.FontSize = 24;
                            */
                        f.Content = l;
                    }
                    else
                    {
                        string[] info = mes[i].Substring(1).Split('֍');
                        StackLayout sl = new StackLayout();
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        for (int j = 0; j < info.Length / 2; j++)
                        {
                            dict[info[2 * j]] = info[2 * j + 1];
                            Label l = new Label { TextColor = Colors.BotColor, Text = info[2 * j] };
                            if (j > 0)
                            {
                                var tapGestureRecognizer = new TapGestureRecognizer();
                                tapGestureRecognizer.Tapped += (s, e) =>
                                {
                                    int k = j;
                                    string tmp = dict[l.Text];
                                    Device.OpenUri(new Uri("http://" + tmp));
                                };
                                l.GestureRecognizers.Add(tapGestureRecognizer);
                            }
                            sl.Children.Add(l);
                        }
                        f.Content = sl;
                    }
                    f.BorderColor = Colors.BotColor;
                    f.HorizontalOptions = LayoutOptions.Start;
                }
                f.VerticalOptions = LayoutOptions.End;
                stack.Children.Add(f);
            }
            scroll = new ScrollView { Content = stack };
            frame.Content = scroll;
        }

    }
}
