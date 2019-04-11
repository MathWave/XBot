using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XBot.SettingsOnStart
{
    public class Intro5 : ContentPage
    {
        Picker entry = new Picker();
        Button ignore = new Button
        {
            Text = "Назад",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        Button ok = new Button
        {
            Text = "Далее",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        async void Pop(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        string t = "\nВалюта\n";

        public Intro5()
        {
            ok.Clicked += (object sender, EventArgs e) => Navigation.PushAsync(new Intro6());
            ignore.Clicked += Pop;
            if (Device.RuntimePlatform == "iOS")
            {
                Title = "Валюта";
                t = "";
            }
            else
                NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Styles.BackColor;
            MakeContent();
        }

        void MakeContent()
        {
            StackLayout sl = new StackLayout();
            Button button1 = new Button
            {
                Text = "⊕",
                FontSize = 20,
                TextColor = Xamarin.Forms.Color.Green,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Styles.BackColor,
                BorderColor = Styles.UserColor,
                HorizontalOptions = LayoutOptions.Start,
            };
            button1.Clicked += Add;
            entry = new Picker
            {
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Styles.UserColor,
                BackgroundColor = Styles.BackColor,
                Title = "Добавить валюту",
                TitleColor = Xamarin.Forms.Color.LightSkyBlue
            };
            /*
            if (Device.RuntimePlatform == "Android")
                entry.SelectedIndexChanged += Add;
                */
            List<string> subs = Formats.FromStringIntoList((string)App.Current.Properties["currency"]);
            foreach (string c in Currency.CurrencyNum.Keys)
                if (!subs.Contains(c))
                    entry.Items.Add(c);
            Frame newframe = new Frame
            {
                CornerRadius = 30,
                BorderColor = Styles.UserColor,
                Content = new StackLayout
                {
                    Children =
                    {
                        button1,
                        entry
                    },
                    Orientation = StackOrientation.Horizontal
                },
                BackgroundColor = Styles.BackColor
            };
            for (int i = subs.Count - 1; i >= 0; i--)
            {
                Button button = new Button
                {
                    Text = "⊖",
                    FontSize = 20,
                    ClassId = i.ToString(),
                    TextColor = Xamarin.Forms.Color.Red,
                    FontAttributes = FontAttributes.Bold,
                    BorderColor = Styles.UserColor,
                    HorizontalOptions = LayoutOptions.Start,
                    BackgroundColor = Styles.BackColor
                };
                button.Clicked += (object sender, EventArgs e) =>
                {
                    subs.RemoveAt(int.Parse(((Button)sender).ClassId));
                    App.Current.Properties["currency"] = Formats.FromListIntoString(subs);
                    MakeContent();
                };
                Frame newf = new Frame
                {
                    CornerRadius = 30,
                    BorderColor = Styles.UserColor,
                    BackgroundColor = Styles.BackColor,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            button,
                            new Label
                            {
                                Text = subs[i],
                                TextColor = Styles.UserColor,
                                HorizontalTextAlignment = TextAlignment.Start,
                                VerticalTextAlignment = TextAlignment.Center
                            }
                        },
                        Orientation = StackOrientation.Horizontal
                    }
                };
                sl.Children.Add(newf);
            }
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = t,
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        TextColor = Styles.UserColor,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Styles.BackColor
                    },
                    newframe,
                    new ScrollView { Content = sl, VerticalOptions = LayoutOptions.FillAndExpand },
                    new StackLayout
                    {
                        Children = {ignore, ok},
                        Orientation = StackOrientation.Horizontal
                    }

                }
            };
        }

        void Add(object sender, EventArgs e)
        {
            List<string> subs = Formats.FromStringIntoList((string)App.Current.Properties["currency"]);
            if (entry.SelectedItem != null && ((string)entry.SelectedItem).Length != 0)
            {
                subs.Add((string)entry.SelectedItem);
                App.Current.Properties["currency"] = Formats.FromListIntoString(subs);
                MakeContent();
            }
        }
    }
}

