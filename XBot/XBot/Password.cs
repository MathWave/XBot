using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class Password : ContentPage
    {

        Entry pass = new Entry
        {
            Placeholder = "Пароль",
            PlaceholderColor = Xamarin.Forms.Color.LightSkyBlue,
            BackgroundColor = Styles.BackColor,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            TextColor = Styles.UserColor,
            HorizontalTextAlignment = TextAlignment.Center,
            IsPassword = true
        };

        Entry repeat = new Entry
        {
            Placeholder = "Повторите пароль",
            PlaceholderColor = Xamarin.Forms.Color.LightSkyBlue,
            BackgroundColor = Styles.BackColor,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            TextColor = Styles.UserColor,
            HorizontalTextAlignment = TextAlignment.Center,
            IsPassword = true
        };

        Button enter = new Button
        {
            Text = "Войти",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End
        };

        string t = "\nРодительский контроль\n";

        public Password()
        {
            if (Device.RuntimePlatform == "iOS")
            {
                Title = "Родительский контроль";
                t = "";
            }
            else
                NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Styles.BackColor;
            StackLayout sl = new StackLayout();
            sl.Children.Add
                (new Label
                {
                    Text = t,
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                    TextColor = Styles.UserColor,
                    FontAttributes = FontAttributes.Bold,
                    BackgroundColor = Styles.BackColor
                });
            if (!(bool)App.Current.Properties["blocked"])
            {
                pass.Placeholder = "Придумайте пароль";
                sl.Children.Add
                    (new Frame
                    {
                        Content = pass,
                        BorderColor = Styles.UserColor,
                        BackgroundColor = Styles.BackColor,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    });
                sl.Children.Add
                    (new Frame
                    {
                        Content = repeat,
                        BorderColor = Styles.UserColor,
                        BackgroundColor = Styles.BackColor,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    });
                enter.Clicked += Register;
            }
            else
            {
                sl.Children.Add
                    (new Frame
                    {
                        Content = pass,
                        BorderColor = Styles.UserColor,
                        BackgroundColor = Styles.BackColor,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    });
                enter.Clicked += Login;
                pass.Completed += Login;
            }
            sl.Children.Add(enter);
            Content = sl;
        }

        async void Register(object sender, EventArgs e)
        {
            if (pass.Text == "" || pass.Text == null)
                await DisplayAlert("Ошибка!", "Поле пароля пустое!", "Ок");
            else if (pass.Text != repeat.Text)
                await DisplayAlert("Ошибка!", "Пароли не совпадают!", "Ок");
            else
            {
                App.Current.Properties["password"] = pass.Text;
                App.Current.Properties["blocked"] = true;
                await Navigation.PushAsync(new Control());
                Navigation.RemovePage(Navigation.NavigationStack[2]);
            }
        }

        async void Login(object sender, EventArgs e)
        {
            if (pass.Text == (string)App.Current.Properties["password"])
            {
                await Navigation.PushAsync(new Control());
                Navigation.RemovePage(Navigation.NavigationStack[2]);
            }
            else
                await DisplayAlert("Ошибка!", "Неверно введен пароль!", "Ок");
        }
    }
}