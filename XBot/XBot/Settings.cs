using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class Settings : ContentPage
    {
        Frame frame;
        Entry entry;
        Switch OnStart = new Switch();

        Picker amount = new Picker { Items = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, WidthRequest = 30, TextColor = Color.Blue };

        public Settings()
        {
            if ((bool)App.Current.Properties["onstart"])
                OnStart.IsToggled = true;
            OnStart.Toggled += (object sender, ToggledEventArgs e) => 
            {
                if ((bool)App.Current.Properties["onstart"])
                    App.Current.Properties["onstart"] = false;
                else
                    App.Current.Properties["onstart"] = true;
            };
;           frame = MakeSubscribtions();
            amount.SelectedIndex = (int)App.Current.Properties["count"] - 1;
            amount.SelectedIndexChanged += (object sender, EventArgs e) => { App.Current.Properties["count"] = amount.SelectedIndex + 1; };
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = System.Drawing.Color.FromName((string)App.Current.Properties["color"]);
            MakeContent();
        }

        Frame MakeSubscribtions()
        {
            Frame f = new Frame { BorderColor = Xamarin.Forms.Color.Blue };
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
                        TextColor = Xamarin.Forms.Color.Blue,
                        FontAttributes = FontAttributes.Bold
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
                    BorderColor = Xamarin.Forms.Color.Blue,
                    HorizontalOptions = LayoutOptions.Start,
                    BackgroundColor = Xamarin.Forms.Color.White
                };
                button.Clicked += (object sender, EventArgs e) => 
                {
                    subs.RemoveAt(int.Parse(((Button)sender).ClassId)) ;
                    App.Current.Properties["subscribes"] = Formats.FromListIntoString(subs);
                    frame = MakeSubscribtions();
                    MakeContent();
                };
                Frame newf = new Frame
                {
                    BorderColor = Xamarin.Forms.Color.Blue,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            button,
                            new Label
                            {
                                Text = subs[i],
                                TextColor = Xamarin.Forms.Color.Blue,
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
                BackgroundColor = Xamarin.Forms.Color.White,
                BorderColor = Xamarin.Forms.Color.Blue,
                HorizontalOptions = LayoutOptions.Start,
            };
            button1.Clicked += (object sender, EventArgs e) =>
            {
                if (entry.Text != null && entry.Text.Length != 0)
                {
                    subs.Add(entry.Text);
                    App.Current.Properties["subscribes"] = Formats.FromListIntoString(subs);
                    frame = MakeSubscribtions();
                    MakeContent();
                }
            };
            entry = new Entry
            {
                Placeholder = "Дополнительная подписка",
                FontSize = 14,
                PlaceholderColor = Xamarin.Forms.Color.LightSkyBlue,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Xamarin.Forms.Color.Blue
            };
            Frame newframe = new Frame
            {
                BorderColor = Xamarin.Forms.Color.Blue,
                Content = new StackLayout
                {
                    Children =
                    {
                        button1,
                        entry
                    },
                    Orientation = StackOrientation.Horizontal
                }
            };
            sl.Children.Add(newframe);
            f.Content = sl;
            return f;
        }

        void MakeContent()
        {
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Frame
                        {
                            Content = new StackLayout
                            {
                                Children =
                                {
                                    new Label
                                    {
                                        TextColor = Xamarin.Forms.Color.Blue,
                                        Text = "Количество новостей за раз",
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalTextAlignment = TextAlignment.Start
                                    },
                                    amount
                                },
                                Orientation = StackOrientation.Horizontal
                            },
                            BorderColor = Xamarin.Forms.Color.Blue,
                            VerticalOptions = LayoutOptions.End,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
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
                                        TextColor = Xamarin.Forms.Color.Blue,
                                        Text = "Показывать подписки при старте",
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        VerticalOptions = LayoutOptions.Center,
                                        HorizontalTextAlignment = TextAlignment.Start
                                    },
                                    OnStart
                                },
                                Orientation = StackOrientation.Horizontal
                            },
                            BorderColor = Xamarin.Forms.Color.Blue,
                            VerticalOptions = LayoutOptions.End,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                        }
                    }
                }
            };
        }

    }
}