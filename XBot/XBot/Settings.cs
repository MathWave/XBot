using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XBot
{
    public partial class Settings : ContentPage
    {
        Picker OnStart = new Picker { Items = { "🔝Последние новости", "🤵Мои подписки", "📈Курс валют" }, WidthRequest = 30 };
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

        void MakeContent()
        {
            BackgroundColor = Styles.BackColor;
            Button Dark = Elements.Button("Сменить", false);
            Button Time = Elements.Button("Выбрать", false);
            Button Size = Elements.Button("Выбрать", false);
            Button Type = Elements.Button("Выбрать", false);
            Button Clear = Elements.Button("Очистить диалоговое окно", true);
            Button Subs = Elements.Button("Мои подписки", true);
            Button Control = Elements.Button("Родительский контроль", true);
            Button Support = Elements.Button("Поддержать", true);
            Time.Clicked += TimeClick;
            Clear.Clicked += ClearClick;
            Subs.Clicked += SubsClick;
            Control.Clicked += (object sender, EventArgs e) =>
            {
                if ((bool)App.Current.Properties["control_intro"])
                    Navigation.PushAsync(new ControlIntro());
                else
                    Navigation.PushAsync(new Password());
            };
            Size.Clicked += (object sender, EventArgs e) =>
            {
                if ((bool)App.Current.Properties["size_intro"])
                    Navigation.PushAsync(new SizeIntro(main));
                else
                    Navigation.PushAsync(new Size(main));
            };
            Support.Clicked += (object sender, EventArgs e) => Navigation.PushAsync(new Support());
            Dark.Clicked += MakeDark;
            amount.TextColor = Styles.UserColor;
            OnStart.BackgroundColor = Styles.BackColor;
            amount.BackgroundColor = Styles.BackColor;
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "\nНастройки\n",
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        TextColor = Styles.UserColor,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Styles.BackColor
                    },
                    new ScrollView
                    {
                        Content = new StackLayout
                        {
                            Children =
                            {
                                Elements.LabelAndElement("Количество новостей за раз", amount),
                                Elements.LabelAndElement("Цветовая тема", Dark),
                                Elements.LabelAndElement("Тематика новостей", Type),
                                Elements.LabelAndElement("Область поиска", Time),
                                Elements.LabelAndElement("При старте показывать", OnStart),
                                Elements.LabelAndElement("Размер шрифта", Size),
                                Elements.LabelAndElement("Показывать подсказки", Hints),
                                Elements.ButtonInFrame(Subs),
                                Elements.ButtonInFrame(Control),
                                Elements.ButtonInFrame(Clear),
                                Elements.ButtonInFrame(Support)
                            }
                        }
                    }
                }
            };
            
        }

    }
}