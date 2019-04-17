using System;
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
        Button favs;

        public MainPage()
        {
            Title = "Диалог";
            NavigationPage.SetHasNavigationBar(this, false);
            message = new Entry
            {
                Placeholder = "Сообщение",
                PlaceholderColor = Xamarin.Forms.Color.LightSkyBlue,
                FontSize = 20,
                BackgroundColor = Styles.BackColor,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Styles.UserColor
            };
            message.Completed += ButtonClick;
            settings = new Button
            {
                Text = "⚙️",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Styles.BackColor,
                WidthRequest = 50
            };
            subscribes = new Button
            {
                Text = "🤵",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Styles.BackColor,
                WidthRequest = 50
            };
            news = new Button
            {
                Text = "🔝",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Styles.BackColor,
                WidthRequest = 50
            };
            currency = new Button
            {
                Text = "📈",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Styles.BackColor,
                WidthRequest = 50
            };
            favs = new Button
            {
                Text = "⭐️",
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                BackgroundColor = Styles.BackColor,
                WidthRequest = 50
            };
            news.Clicked += NewsClick;
            settings.Clicked += SettingsClick;
            subscribes.Clicked += SubscribesClick;
            currency.Clicked += CurrencyClick;
            favs.Clicked += FavsClick;
            string line = (string)App.Current.Properties["messages"];
            if (line.Length == 0)
                frame = new Frame { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Styles.BackColor };
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

        private void FavsClick(object sender, EventArgs e)
        {
            if ((string)App.Current.Properties["save"] == "")
                Chat.Add("Список закладок пуст!", true);
            else
            {
                if ((string)App.Current.Properties["messages"] != "")
                    App.Current.Properties["messages"] += Formats.parse.ToString();
                App.Current.Properties["messages"] += $"BЗакладки:\n\n֍֍{((string)App.Current.Properties["save"]).Replace(Formats.parse.ToString(), "֍")}";
            }
            Display();
        }

        public void Active(bool act)
        {
            message.IsEnabled = act;
            settings.IsEnabled = act;
            subscribes.IsEnabled = act;  
            news.IsEnabled = act;
            currency.IsEnabled = act;
            favs.IsEnabled = act;
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
            if ((string)App.Current.Properties["currency"] == "")
            {
                Chat.Add("Список валют пуст! Заполните его в настройках!", true);
                Display();
            }
            else
            {
                try
                {
                    Chat.Add("Вычисляю курс валют...", true);
                    Display();
                    Bot.Currency(this);
                }
                catch { }
            }
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
                            currency,
                            favs
                        },
                        Orientation = StackOrientation.Horizontal
                    },
                }
            };
            BackgroundColor = Styles.BackColor;
            message.TextColor = Styles.UserColor;
            message.BackgroundColor = Styles.BackColor;
            settings.BackgroundColor = Styles.BackColor;
            subscribes.BackgroundColor = Styles.BackColor;
            news.BackgroundColor = Styles.BackColor;
            currency.BackgroundColor = Styles.BackColor;
            favs.BackgroundColor = Styles.BackColor;
            if (scroll != null)
                scroll.ScrollToAsync(stack, ScrollToPosition.End, false);
        }

        public void MakeFrame()
        {
            List<string> mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
            frame = new Frame { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Styles.BackColor };
            stack = new StackLayout { VerticalOptions = LayoutOptions.End };
            for (int i = 0; i < mes.Count; i++)
            {
                Frame f = new Frame { BackgroundColor = Styles.BotColor, CornerRadius = 30, HasShadow = false };
                if (mes[i][0] == 'U')
                    f = new Frame
                    {
                        Content = new Label { TextColor = Xamarin.Forms.Color.White, Text = mes[i].Substring(1), BackgroundColor = Styles.UserColor, FontSize = Styles.Size },
                        BorderColor = Styles.UserColor,
                        HorizontalOptions = LayoutOptions.End,
                        BackgroundColor = Styles.UserColor,
                        CornerRadius = 30,
                        HasShadow = false
                    };
                else
                {
                    if (mes[i][1] == 'C')
                    {
                        string[] cur = mes[i].Substring(2, mes[i].Length - 3).Split('\n');
                        StackLayout sl = new StackLayout();
                        sl.Children.Add(new Label { TextColor = Xamarin.Forms.Color.White, Text = cur[0], BackgroundColor = Styles.BotColor, FontSize = Styles.Size, FontAttributes = FontAttributes.Bold });
                        for (int j = 1; j < cur.Length; j++)
                            sl.Children.Add(new Label { TextColor = Xamarin.Forms.Color.White, Text = cur[j], BackgroundColor = Styles.BotColor, FontSize = Styles.Size * 3 / 2 });
                        f.Content = sl;
                    }
                    else if (mes[i].Split('֍').Length == 1)
                    {
                        Label l = new Label { TextColor = Xamarin.Forms.Color.White, Text = mes[i].Substring(1), BackgroundColor = Styles.BotColor, FontSize = Styles.Size };
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
                            Label l = new Label { TextColor = Xamarin.Forms.Color.White, Text = info[2 * j], FontSize = Styles.Size, BackgroundColor = Styles.BotColor };
                            if (j > 0)
                            {
                                var tapGestureRecognizer = new TapGestureRecognizer();
                                tapGestureRecognizer.Tapped += (s, e) =>
                                {
                                    string tmp = dict[l.Text];
                                    Navigation.PushAsync(new ShowContent(tmp, l.Text));
                                };
                                l.GestureRecognizers.Add(tapGestureRecognizer);
                            }
                            else
                                l.FontAttributes = FontAttributes.Bold;
                            sl.Children.Add(l);
                        }
                        f.Content = sl;
                    }
                    f.BorderColor = Styles.BotColor;
                    f.HorizontalOptions = LayoutOptions.Start;
                }
                f.VerticalOptions = LayoutOptions.End;
                stack.Children.Add(new Label { FontSize = Styles.Size / 2, Text = "\n" });
                stack.Children.Add(f);
            }
            scroll = new ScrollView { Content = stack };
            frame.Content = scroll;
        }

    }
}
