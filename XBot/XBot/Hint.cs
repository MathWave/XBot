using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class Hint : ContentPage
    {
        Button ok = new Button
        {
            Text = "Спасибо",
            BackgroundColor = Colors.BackColor,
            TextColor = Colors.UserColor,
            BorderColor = Colors.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        string[] hints =
        {
            "При нажатии на кнопку 📈 я расскажу тебе о курсе валют",
            "При нажатии на кнопку 🤵 я расскажу тебе о последних новостях из твоих подписок",
            "При нажатии на кнопку 🔝 я расскажу тебе о последних новостях",
            "В настройках можно указать количество новостей выводимых за раз",
            "В настройках можно сменить цветовую схему",
            "В настройках можно сменить контент, показываемый при запуске",
            "В настройках можно сохранить ключевые слова для поиска в разделе \"Мои подписки\"",
            "В настройках можно запретить показывать нежелательный контент в разделе \"Родительский контроль\"",
            "В настройках можно очистить диалоговое окно"
        };

        public Hint()
        {
            ok.Clicked += OkClick;
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Colors.BackColor;
            Content = new StackLayout
            {
                Children =
                {
                    new ScrollView
                    {
                        Content = new Frame
                        {
                            Content = new Label
                            {
                                Text = hints[(new Random()).Next(hints.Length)],
                                TextColor = Colors.UserColor,
                                FontSize = 24
                            },
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = Colors.BackColor,
                            HasShadow = false
                        },
                        VerticalOptions = LayoutOptions.FillAndExpand
                    },
                    ok
                }
            };
        }

        async void OkClick(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(Navigation.NavigationStack[0]);
        }
    }
}