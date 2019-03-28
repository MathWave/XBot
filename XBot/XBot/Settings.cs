using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XBot
{
    public class Settings : ContentPage
    {
        Picker OnStart = new Picker { Items = { "🔝Последние новости", "🤵Мои подписки", "📈Курс валют" }, WidthRequest = 30 };
        Button Dark = new Button();

        Picker amount = new Picker { Items = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, WidthRequest = 30, TextColor = Colors.UserColor };
        MainPage main;

        public Settings(MainPage m)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            main = m;
            amount.SelectedIndex = (int)App.Current.Properties["count"] - 1;
            amount.SelectedIndexChanged += (object sender, EventArgs e) => { App.Current.Properties["count"] = amount.SelectedIndex + 1; };
            OnStart.SelectedIndex = (string)App.Current.Properties["onstart"] == "news" ? 0 : (string)App.Current.Properties["onstart"] == "subscribes" ? 1 : 2;
            OnStart.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                if (OnStart.SelectedIndex == 0)
                    App.Current.Properties["onstart"] = "news";
                else if (OnStart.SelectedIndex == 1)
                    App.Current.Properties["onstart"] = "subscribes";
                else
                    App.Current.Properties["onstart"] = "currency";
            };
            MakeContent();
        }

         void MakeDark(object sender, EventArgs e)
         {
            if ((string)App.Current.Properties["back"] == "30 30 30")
            {
                App.Current.Properties["back"] = "255 255 255";
                App.Current.Properties["user"] = "0 0 255";
                App.Current.Properties["bot"] = "128 0 128";
            }
            else
            {
                App.Current.Properties["back"] = "30 30 30";
                App.Current.Properties["user"] = "86 156 214";
                App.Current.Properties["bot"] = "255 255 255";
            }
            main.Display();
            MakeContent();
         }

        

        void MakeContent()
        {
            BackgroundColor = Colors.BackColor;
            Dark = new Button
            {
                Text = "Сменить",
                BackgroundColor = Colors.BackColor,
                TextColor = Colors.UserColor,
                BorderColor = Colors.UserColor
            };
            Button b = new Button
            {
                Text = "Очистить диалоговое окно",
                BackgroundColor = Colors.BackColor,
                TextColor = Colors.UserColor,
                BorderColor = Colors.UserColor,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            b.Clicked += (object sender, EventArgs e) =>
            {
                App.Current.Properties["messages"] = "";
                main.Display();
            };
            Button b1 = new Button
            {
                Text = "Мои подписки",
                BackgroundColor = Colors.BackColor,
                TextColor = Colors.UserColor,
                BorderColor = Colors.UserColor,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            b1.Clicked += (object sender, EventArgs e) =>
            {
                if ((bool)App.Current.Properties["subscribes_intro"])
                    Navigation.PushAsync(new SubscribesIntro());
                else
                    Navigation.PushAsync(new Subscribes());
            };
            Button b2 = new Button
            {
                Text = "Родительский контроль",
                BackgroundColor = Colors.BackColor,
                TextColor = Colors.UserColor,
                BorderColor = Colors.UserColor,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            b2.Clicked += (object sender, EventArgs e) =>
            {
                if ((bool)App.Current.Properties["control_intro"])
                    Navigation.PushAsync(new ControlIntro());
                else
                    Navigation.PushAsync(new Control());
            };
            Dark.Clicked += MakeDark;
            amount.TextColor = Colors.UserColor;
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Frame
                        {
                            BackgroundColor = Colors.BackColor,
                            Content = new StackLayout
                            {
                                Children =
                                {
                                    new Label
                                    {
                                        TextColor = Colors.UserColor,
                                        Text = "Количество новостей за раз",
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalTextAlignment = TextAlignment.Start,
                                        BackgroundColor = Colors.BackColor
                                    },
                                    amount
                                },
                                Orientation = StackOrientation.Horizontal
                            },
                            BorderColor = Colors.UserColor,
                            VerticalOptions = LayoutOptions.End,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                        },
                        new Frame
                        {
                            Content = new StackLayout
                            {
                                Children =
                                {
                                    new Label
                                    {
                                        TextColor = Colors.UserColor,
                                        Text = "Цветовая тема",
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalTextAlignment = TextAlignment.Start
                                    },
                                    Dark
                                },
                                Orientation = StackOrientation.Horizontal
                            },
                            BorderColor = Colors.UserColor,
                            VerticalOptions = LayoutOptions.End,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Colors.BackColor
                        },
                        new Frame
                        {
                            Content = new StackLayout
                            {
                                Children =
                                {
                                    new Label
                                    {
                                        TextColor = Colors.UserColor,
                                        Text = "При старте показывать",
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalTextAlignment = TextAlignment.Start
                                    },
                                    OnStart
                                },
                                Orientation = StackOrientation.Horizontal
                            },
                            BorderColor = Colors.UserColor,
                            VerticalOptions = LayoutOptions.End,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Colors.BackColor
                        },
                        new Frame
                        {
                            Content = b1,
                            BorderColor = Colors.UserColor,
                            BackgroundColor = Colors.BackColor
                        },
                        new Frame
                        {
                            Content = b2,
                            BorderColor = Colors.UserColor,
                            BackgroundColor = Colors.BackColor
                        },
                        new Frame
                        {
                            Content = b,
                            BorderColor = Colors.UserColor,
                            BackgroundColor = Colors.BackColor
                        }
                    }
                }
            };
            
        }

    }
}