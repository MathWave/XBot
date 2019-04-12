using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XBot.SettingsOnStart
{
    public class Intro6 : ContentPage
    {
        Entry entry = new Entry();

        string t = "\nМои подписки\n";
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
        Button help = new Button
        {
            Text = "?",
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };
        async void Help(object sender, EventArgs e)
        {
            await DisplayAlert("Помощь", "Выбери ключевые слова, по которым ты хотел бы получать нвоости", "Хорошо!");
        }
        async void Pop(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        public Intro6()
        {
            ok.Clicked += (object sender, EventArgs e) => Navigation.PushAsync(new Intro7());
            ignore.Clicked += Pop;
            help.Clicked += Help;
            if (Device.RuntimePlatform == "iOS")
            {
                Title = "Мои подписки";
                t = "";
            }
            else
                NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Styles.BackColor;
            MakeContent();
            Display();
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
            entry = new Entry
            {
                Placeholder = "Дополнительная подписка",
                FontSize = 14,
                PlaceholderColor = Xamarin.Forms.Color.LightSkyBlue,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                TextColor = Styles.UserColor,
                BackgroundColor = Styles.BackColor
            };
            entry.Completed += Add;
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
                    BorderColor = Styles.UserColor,
                    HorizontalOptions = LayoutOptions.Start,
                    BackgroundColor = Styles.BackColor
                };
                button.Clicked += (object sender, EventArgs e) =>
                {
                    subs.RemoveAt(int.Parse(((Button)sender).ClassId));
                    App.Current.Properties["subscribes"] = Formats.FromListIntoString(subs);
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
                        Children = {ignore, help, ok },
                        Orientation = StackOrientation.Horizontal
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

        async void Display()
        {
            await DisplayAlert("Важная информация!", "Выбери ключевые слова, по которым будет производиться поиск", "Понятно!");
        }
    }
}

