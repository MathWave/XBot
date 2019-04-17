using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class Size : ContentPage
    {

        MainPage main;
        string t = "\nРазмер шрифта\n";
        Slider slider = new Slider
        {
            Minimum = 0,
            Maximum = 18,
            ThumbColor = Styles.UserColor,
            MaximumTrackColor = Styles.UserColor,
            MinimumTrackColor = Styles.UserColor,
            Value = (int)App.Current.Properties["size"] - 8
        };
        Button ok = new Button
        {
            Text = "Применить",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };
        int size = Styles.Size;
        Label my = new Label { TextColor = Xamarin.Forms.Color.White, Text = "Привет, как тебя зовут?", BackgroundColor = Styles.UserColor, FontSize = Styles.Size };
        Label bot = new Label { TextColor = Xamarin.Forms.Color.White, Text = "Меня зовут XBot!", BackgroundColor = Styles.BotColor, FontSize = Styles.Size };
        Frame fr;
        public Size(MainPage m)
        {
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
            main = m;
            if (Device.RuntimePlatform == "iOS")
            {
                Title = "Размер шрифта";
                t = "";
            }
            else
                NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Styles.BackColor;
            slider.ValueChanged += Slider_ValueChanged;
            ok.Clicked += Ok_Clicked;
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
                    ok
                }
            };
        }

        async void Ok_Clicked(object sender, EventArgs e)
        {
            App.Current.Properties["size"] = size;
            main.Display();
            await Navigation.PopAsync();
        }


        void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            size = (int)(e.NewValue + 8);
            my.FontSize = size;
            bot.FontSize = size;
        }


        

    }
}