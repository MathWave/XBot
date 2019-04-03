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
        Picker OnStart = new Picker { Items = { "🔝Последние новости", "🤵Мои подписки", "📈Курс валют" }, WidthRequest = 30 };
        Button Dark = new Button();
        Button time = new Button();
        Switch Hints = new Switch();
        Picker amount = new Picker { Items = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }, WidthRequest = 30, TextColor = Styles.UserColor };
        MainPage main;

        public Settings(MainPage m)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            main = m;
            amount.SelectedIndex = (int)App.Current.Properties["count"] - 1;
            amount.SelectedIndexChanged += (object sender, EventArgs e) => { App.Current.Properties["count"] = amount.SelectedIndex + 1; };
            OnStart.SelectedIndex = (string)App.Current.Properties["onstart"] == "news" ? 0 : (string)App.Current.Properties["onstart"] == "subscribes" ? 1 : 2;
            Hints.IsToggled = (bool)App.Current.Properties["hint"];
            Hints.Toggled += (object sender, ToggledEventArgs e) => App.Current.Properties["hint"] = !(bool)App.Current.Properties["hint"];
            OnStart.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                if (OnStart.SelectedIndex == 0)
                    App.Current.Properties["onstart"] = "news";
                else if (OnStart.SelectedIndex == 1)
                    App.Current.Properties["onstart"] = "subscribes";
                else
                    App.Current.Properties["onstart"] = "currency";
            };
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

        void MakeContent()
        {
            BackgroundColor = Styles.BackColor;
            Dark = new Button
            {
                Text = "Сменить",
                BackgroundColor = Styles.BackColor,
                TextColor = Styles.UserColor,
                BorderColor = Styles.UserColor
            };
            time = new Button
            {
                Text = "Выбрать",
                BackgroundColor = Styles.BackColor,
                TextColor = Styles.UserColor,
                BorderColor = Styles.UserColor
            };
            time.Clicked += (object sender, EventArgs e) =>
            {
                if ((bool)App.Current.Properties["frequency_intro"])
                    Navigation.PushAsync(new TimingIntro());
                else
                    Navigation.PushAsync(new Timing());
            };
            Button b = new Button
            {
                Text = "Очистить диалоговое окно",
                BackgroundColor = Styles.BackColor,
                TextColor = Styles.UserColor,
                BorderColor = Styles.UserColor,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            b.Clicked += (object sender, EventArgs e) =>
            {
                App.Current.Properties["messages"] = "";
                main.Display();
            };
            Button b1 = new Button
            {
                Text = "Мои подписки",
                BackgroundColor = Styles.BackColor,
                TextColor = Styles.UserColor,
                BorderColor = Styles.UserColor,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            b1.Clicked += (object sender, EventArgs e) =>
            {
                if ((bool)App.Current.Properties["subscribes_intro"])
                    Navigation.PushAsync(new SubscribesIntro());
                else
                    Navigation.PushAsync(new Subscribes());
            };
            Button b2 = new Button
            {
                Text = "Родительский контроль",
                BackgroundColor = Styles.BackColor,
                TextColor = Styles.UserColor,
                BorderColor = Styles.UserColor,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            b2.Clicked += (object sender, EventArgs e) =>
            {
                if ((bool)App.Current.Properties["control_intro"])
                    Navigation.PushAsync(new ControlIntro());
                else
                    Navigation.PushAsync(new Password());
            };
            Button size = new Button
            {
                Text = "Сменить",
                BackgroundColor = Styles.BackColor,
                TextColor = Styles.UserColor,
                BorderColor = Styles.UserColor
            };
            size.Clicked += (object sender, EventArgs e) =>
            {
                if ((bool)App.Current.Properties["size_intro"])
                    Navigation.PushAsync(new SizeIntro(main));
                else
                    Navigation.PushAsync(new Size(main));
            };
            Dark.Clicked += MakeDark;
            amount.TextColor = Styles.UserColor;
            OnStart.BackgroundColor = Styles.BackColor;
            amount.BackgroundColor = Styles.BackColor;
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        Elements.LabelAndElement("Количество новостей за раз", amount),
                        Elements.LabelAndElement("Цветовая тема", Dark),
                        Elements.LabelAndElement("Область поиска", time),
                        Elements.LabelAndElement("Размер шрифта", size),
                        Elements.LabelAndElement("При старте показывать", OnStart),
                        Elements.LabelAndElement("Показывать подсказки", Hints),
                        Elements.Button(b1),
                        Elements.Button(b2),
                        Elements.Button(b)
                    }
                }
            };
            
        }

    }
}