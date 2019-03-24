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
        Entry entry = new Entry();
        Switch OnStart = new Switch();
        Button Dark = new Button();

        Picker amount = new Picker { Items = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, WidthRequest = 30, TextColor = Colors.UserColor };
        MainPage main;

        public Settings(MainPage m)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            main = m;
            OnStart.Toggled += (object sender, ToggledEventArgs e) => 
            {
                if ((bool)App.Current.Properties["onstart"])
                    App.Current.Properties["onstart"] = false;
                else
                    App.Current.Properties["onstart"] = true;
            };
            amount.SelectedIndex = (int)App.Current.Properties["count"] - 1;
            amount.SelectedIndexChanged += (object sender, EventArgs e) => { App.Current.Properties["count"] = amount.SelectedIndex + 1; };
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
            b1.Clicked += (object sender, EventArgs e) => Navigation.PushAsync(new Subscribes());
            Button b2 = new Button
            {
                Text = "Родительский контроль",
                BackgroundColor = Colors.BackColor,
                TextColor = Colors.UserColor,
                BorderColor = Colors.UserColor,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            b2.Clicked += (object sender, EventArgs e) => Navigation.PushAsync(new Password());
            if ((bool)App.Current.Properties["onstart"])
                OnStart.IsToggled = true;
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
                                        Text = "Показывать подписки при старте",
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