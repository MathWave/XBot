using System;

using Xamarin.Forms;

namespace XBot.SettingsOnStart
{
    public class Intro1 : ContentPage
    {
        Button ok;
        Button ignore;
        Button help;
        async void Pop(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        public Intro1()
        {
            if (Device.RuntimePlatform == "iOS")
            {
                Title = "Цветовая тема";
                t = "";
            }
            else
                NavigationPage.SetHasNavigationBar(this, false);
            MakeContent();
            dark.Clicked += (object sender, EventArgs e) =>
            {
                App.Current.Properties["back"] = "30 30 30";
                App.Current.Properties["user"] = "86 156 214";
                App.Current.Properties["bot"] = "80 80 80";
                MakeContent();
            };
            light.Clicked += (object sender, EventArgs e) =>
            {
                App.Current.Properties["back"] = "255 255 255";
                App.Current.Properties["user"] = "0 0 255";
                App.Current.Properties["bot"] = "128 0 128";
                MakeContent();
            };
        }

        string t = "\nЦветовая тема\n";
        Button dark = new Button { Text="     ", BackgroundColor = Color.FromRgb(30, 30, 30), BorderColor = Color.FromRgb(255,255,255), BorderWidth = 1, HorizontalOptions = LayoutOptions.FillAndExpand };
        Button light = new Button { Text = "     ", BackgroundColor = Color.FromRgb(255, 255, 255), BorderColor = Color.FromRgb(30, 30, 30), BorderWidth = 1, HorizontalOptions = LayoutOptions.FillAndExpand };

        async void Help(object sender, EventArgs e)
        {
            await DisplayAlert("Помощь", "Выбери цветовую тему приложения", "Хорошо!");
        }

        void MakeContent()
        {
            ignore = new Button
            {
                Text = "Назад",
                BackgroundColor = Styles.BackColor,
                TextColor = Styles.UserColor,
                BorderColor = Styles.UserColor,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            ok = new Button
            {
                Text = "Далее",
                BackgroundColor = Styles.BackColor,
                TextColor = Styles.UserColor,
                BorderColor = Styles.UserColor,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            help = new Button
            {
                Text = "?",
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Styles.BackColor,
                TextColor = Styles.UserColor,
                BorderColor = Styles.UserColor,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            ok.Clicked += (object sender, EventArgs e) => Navigation.PushAsync(new Intro2());
            ignore.Clicked += Pop;
            help.Clicked += Help;
            BackgroundColor = Styles.BackColor;
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
                    new Frame
                    {
                        Content = new StackLayout
                        {
                            Children =
                            {
                                new Frame
                                {
                                    Content = new Label { TextColor = Xamarin.Forms.Color.White, Text = "Привет, как тебя зовут?", BackgroundColor = Styles.UserColor, FontSize = Styles.Size },
                                    BorderColor = Styles.UserColor,
                                    HorizontalOptions = LayoutOptions.End,
                                    BackgroundColor = Styles.UserColor,
                                    CornerRadius = 30,
                                    HasShadow = false
                                },
                                new Frame
                                {
                                    Content = new Label { TextColor = Xamarin.Forms.Color.White, Text = "Меня зовут XBot!", BackgroundColor = Styles.BotColor, FontSize = Styles.Size },
                                    BorderColor = Styles.BotColor,
                                    HorizontalOptions = LayoutOptions.Start,
                                    BackgroundColor = Styles.BotColor,
                                    CornerRadius = 30,
                                    HasShadow = false
                                }
                            }
                        },
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HasShadow = false,
                        BackgroundColor = Styles.BackColor
                    },
                    new StackLayout
                    {
                        Children =
                        {
                            light,
                            dark
                        },
                        Orientation = StackOrientation.Horizontal
                    },
                    new StackLayout
                    {
                        Children = {ignore, help, ok},
                        Orientation = StackOrientation.Horizontal
                    }
                }
            };
        }
    }
}

