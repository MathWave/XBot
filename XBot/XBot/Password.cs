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
            BackgroundColor = Colors.BackColor,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            TextColor = Colors.UserColor,
            HorizontalTextAlignment = TextAlignment.Center
        };

        Entry repeat = new Entry
        {
            Placeholder = "Повторите пароль",
            PlaceholderColor = Xamarin.Forms.Color.LightSkyBlue,
            BackgroundColor = Colors.BackColor,
            VerticalOptions = LayoutOptions.CenterAndExpand,
            TextColor = Colors.UserColor,
            HorizontalTextAlignment = TextAlignment.Center
        };

        Button enter = new Button
        {
            Text = "Войти",
            BackgroundColor = Colors.BackColor,
            TextColor = Colors.UserColor,
            BorderColor = Colors.UserColor,
            VerticalOptions = LayoutOptions.End
        };

        public Password()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Colors.BackColor;
            StackLayout sl = new StackLayout();
            sl.Children.Add
                (new Label
                {
                    Text = "\nРодительский контроль\n",
                    HorizontalTextAlignment = TextAlignment.Center,
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.Start,
                    TextColor = Colors.UserColor,
                    FontAttributes = FontAttributes.Bold,
                    BackgroundColor = Colors.BackColor
                });
            if (!(bool)App.Current.Properties["blocked"])
            {
                pass.Placeholder = "Придумайте пароль";
                sl.Children.Add
                    (new Frame
                    {
                        Content = pass,
                        BorderColor = Colors.UserColor,
                        BackgroundColor = Colors.BackColor,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    });
                sl.Children.Add
                    (new Frame
                    {
                        Content = repeat,
                        BorderColor = Colors.UserColor,
                        BackgroundColor = Colors.BackColor,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    });
                enter.Clicked += Register;
                pass.Completed += Register;
            }
            else
            {
                pass.IsPassword = true;
                sl.Children.Add
                    (new Frame
                    {
                        Content = pass,
                        BorderColor = Colors.UserColor,
                        BackgroundColor = Colors.BackColor,
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
                Navigation.PushAsync(new Control());
                Navigation.RemovePage(Navigation.NavigationStack[2]);
            }
        }

        async void Login(object sender, EventArgs e)
        {
            if (pass.Text == (string)App.Current.Properties["password"])
            {
                Navigation.PushAsync(new Control());
                Navigation.RemovePage(Navigation.NavigationStack[2]);
            }
            else
                await DisplayAlert("Ошибка!", "Неверно введен пароль!", "Ок");
        }
    }
}