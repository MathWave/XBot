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
        Frame frame = new Frame();
        Entry entry = new Entry();
        Switch OnStart = new Switch();
        Button Dark = new Button();

        Picker amount = new Picker { Items = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, WidthRequest = 30, TextColor = MainPage.UserColor };
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

        Frame MakeSubscribtions()
        {
            Frame f = new Frame { BorderColor = MainPage.UserColor, BackgroundColor = MainPage.BackColor };
            StackLayout sl = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "Мои подписки\n",
                        HorizontalTextAlignment = TextAlignment.Center,
                        //FontSize = 20,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        TextColor = MainPage.UserColor,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = MainPage.BackColor
                    }
                }
            };
            List<string> subs = Formats.FromStringIntoList((string)App.Current.Properties["subscribes"]);
            for (int i = 0; i < subs.Count; i++)
            {
                Button button = new Button
                {
                    Text = "⊖",
                    FontSize = 20,
                    ClassId = i.ToString(),
                    TextColor = Xamarin.Forms.Color.Red,
                    FontAttributes = FontAttributes.Bold,
                    BorderColor = MainPage.UserColor,
                    HorizontalOptions = LayoutOptions.Start,
                    BackgroundColor = MainPage.BackColor
                };
                button.Clicked += (object sender, EventArgs e) => 
                {
                    subs.RemoveAt(int.Parse(((Button)sender).ClassId)) ;
                    App.Current.Properties["subscribes"] = Formats.FromListIntoString(subs);
                    MakeContent();
                };
                Frame newf = new Frame
                {
                    BorderColor = MainPage.UserColor,
                    BackgroundColor = MainPage.BackColor,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            button,
                            new Label
                            {
                                Text = subs[i],
                                TextColor = MainPage.UserColor,
                                HorizontalTextAlignment = TextAlignment.Start,
                                VerticalTextAlignment = TextAlignment.Center
                            }
                        },
                        Orientation = StackOrientation.Horizontal
                    }
                };
                sl.Children.Add(newf);
            }
            Button button1 = new Button
            {
                Text = "⊕",
                FontSize = 20,
                TextColor = Xamarin.Forms.Color.Green,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = MainPage.BackColor,
                BorderColor = MainPage.UserColor,
                HorizontalOptions = LayoutOptions.Start,
            };
            button1.Clicked += Add;
            entry = new Entry
            {
                Placeholder = "Дополнительная подписка",
                FontSize = 14,
                PlaceholderColor = Xamarin.Forms.Color.LightSkyBlue,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = MainPage.UserColor,
                BackgroundColor = MainPage.BackColor
            };
            entry.Completed += Add;
            Frame newframe = new Frame
            {
                BorderColor = MainPage.UserColor,
                Content = new StackLayout
                {
                    Children =
                    {
                        button1,
                        entry
                    },
                    Orientation = StackOrientation.Horizontal
                },
                BackgroundColor = MainPage.BackColor
            };
            sl.Children.Add(newframe);
            f.Content = sl;
            return f;
        }

        void MakeContent()
        {
            BackgroundColor = MainPage.BackColor;
            frame = MakeSubscribtions();
            Dark = new Button
            {
                Text = "Сменить",
                BackgroundColor = MainPage.BackColor,
                TextColor = MainPage.UserColor,
                BorderColor = MainPage.UserColor
            };
            if ((bool)App.Current.Properties["onstart"])
                OnStart.IsToggled = true;
            Dark.Clicked += MakeDark;
            amount.TextColor = MainPage.UserColor;
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Frame
                        {
                            BackgroundColor = MainPage.BackColor,
                            Content = new StackLayout
                            {
                                Children =
                                {
                                    new Label
                                    {
                                        TextColor = MainPage.UserColor,
                                        Text = "Количество новостей за раз",
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalTextAlignment = TextAlignment.Start,
                                        BackgroundColor = MainPage.BackColor
                                    },
                                    amount
                                },
                                Orientation = StackOrientation.Horizontal
                            },
                            BorderColor = MainPage.UserColor,
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
                                        TextColor = MainPage.UserColor,
                                        Text = "Цветовая тема",
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalTextAlignment = TextAlignment.Start
                                    },
                                    Dark
                                },
                                Orientation = StackOrientation.Horizontal
                            },
                            BorderColor = MainPage.UserColor,
                            VerticalOptions = LayoutOptions.End,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = MainPage.BackColor
                        },
                        frame,
                        new Frame
                        {
                            Content = new StackLayout
                            {
                                Children =
                                {
                                    new Label
                                    {
                                        TextColor = MainPage.UserColor,
                                        Text = "Показывать подписки при старте",
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalTextAlignment = TextAlignment.Start
                                    },
                                    OnStart
                                },
                                Orientation = StackOrientation.Horizontal
                            },
                            BorderColor = MainPage.UserColor,
                            VerticalOptions = LayoutOptions.End,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = MainPage.BackColor
                        },
                    }
                }
            };
            
        }

        void Add(object sender, EventArgs e)
        {
            List<string> subs = Formats.FromStringIntoList((string)App.Current.Properties["subscribes"]);
            if (entry.Text != null && entry.Text.Length != 0)
            {
                subs.Add(entry.Text);
                App.Current.Properties["subscribes"] = Formats.FromListIntoString(subs);
                MakeContent();
            }
        }

    }
}