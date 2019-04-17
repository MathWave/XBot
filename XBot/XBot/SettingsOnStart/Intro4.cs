using System;

using Xamarin.Forms;

namespace XBot.SettingsOnStart
{
    public class Intro4 : ContentPage
    {
        string t = "\nРазмер шрифта\n";
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
        Slider slider = new Slider
        {
            Minimum = 0,
            Maximum = 18,
            ThumbColor = Styles.UserColor,
            MaximumTrackColor = Styles.UserColor,
            MinimumTrackColor = Styles.UserColor,
            Value = (int)App.Current.Properties["size"] - 8
        };
        Label my = new Label { TextColor = Xamarin.Forms.Color.White, Text = "Привет, как тебя зовут?", BackgroundColor = Styles.UserColor, FontSize = Styles.Size };
        Label bot = new Label { TextColor = Xamarin.Forms.Color.White, Text = "Меня зовут XBot!", BackgroundColor = Styles.BotColor, FontSize = Styles.Size };
        Frame fr;
        int size = (int)App.Current.Properties["size"];
        async void Help(object sender, EventArgs e)
        {
            await DisplayAlert("Помощь", "Выбери размер шрифта в диалоге", "Хорошо!");
        }
        async void Pop(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        public Intro4()
        {
            slider.ValueChanged += Slider_ValueChanged;
            ok.Clicked += (object sender, EventArgs e) =>
            {
                App.Current.Properties["size"] = size;
                Navigation.PushAsync(new Intro5());
            };

            ignore.Clicked += Pop;
            help.Clicked += Help;
            if (Device.RuntimePlatform == "iOS")
            {
                Title = "Размер шрифта";
                t = "";
            }
            else
                NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Styles.BackColor;
            fr = new Frame
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Frame
                        {
                            Content = my,
                            BorderColor = Styles.UserColor,
                            HorizontalOptions = LayoutOptions.End,
                            BackgroundColor = Styles.UserColor,
                            CornerRadius = 30,
                            HasShadow = false
                        },
                        new Frame
                        {
                            Content = bot,
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
            };
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
                    fr,
                    slider,
                    new StackLayout
                    {
                        Children = { ignore, help, ok },
                        Orientation = StackOrientation.Horizontal
                    }
                }
            };
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            size = (int)e.NewValue + 8;
            my.FontSize = size;
            bot.FontSize = size;
        }

    }
}

