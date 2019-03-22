﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class Subscribes : ContentPage
    {

        Entry entry = new Entry();

        public Subscribes()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Colors.BackColor;
            MakeContent();
        }

        void MakeContent()
        {
            StackLayout sl = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "\nМои подписки\n",
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        TextColor = Colors.UserColor,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Colors.BackColor
                    }
                }
            };
            Button button1 = new Button
            {
                Text = "⊕",
                FontSize = 20,
                TextColor = Xamarin.Forms.Color.Green,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Colors.BackColor,
                BorderColor = Colors.UserColor,
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
                TextColor = Colors.UserColor,
                BackgroundColor = Colors.BackColor
            };
            entry.Completed += Add;
            Frame newframe = new Frame
            {
                BorderColor = Colors.UserColor,
                Content = new StackLayout
                {
                    Children =
                    {
                        button1,
                        entry
                    },
                    Orientation = StackOrientation.Horizontal
                },
                BackgroundColor = Colors.BackColor
            };
            sl.Children.Add(newframe);
            List<string> subs = Formats.FromStringIntoList((string)App.Current.Properties["subscribes"]);
            for (int i = subs.Count - 1; i >= 0; i--)
            {
                Button button = new Button
                {
                    Text = "⊖",
                    FontSize = 20,
                    ClassId = i.ToString(),
                    TextColor = Xamarin.Forms.Color.Red,
                    FontAttributes = FontAttributes.Bold,
                    BorderColor = Colors.UserColor,
                    HorizontalOptions = LayoutOptions.Start,
                    BackgroundColor = Colors.BackColor
                };
                button.Clicked += (object sender, EventArgs e) =>
                {
                    subs.RemoveAt(int.Parse(((Button)sender).ClassId));
                    App.Current.Properties["subscribes"] = Formats.FromListIntoString(subs);
                    MakeContent();
                };
                Frame newf = new Frame
                {
                    BorderColor = Colors.UserColor,
                    BackgroundColor = Colors.BackColor,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            button,
                            new Label
                            {
                                Text = subs[i],
                                TextColor = Colors.UserColor,
                                HorizontalTextAlignment = TextAlignment.Start,
                                VerticalTextAlignment = TextAlignment.Center
                            }
                        },
                        Orientation = StackOrientation.Horizontal
                    }
                };
                sl.Children.Add(newf);
            }
            Content = new ScrollView { Content = sl };
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