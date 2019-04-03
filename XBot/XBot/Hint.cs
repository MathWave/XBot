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
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
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
            "В настройках можно очистить диалоговое окно",
            "В настройках можно указать период времени, по которому будет проводиться поиск",
            "В настройках можно выбрать размер шрифта в чате"
        };

        public Hint()
        {
            if (App.device)
                ok.Text += "\n\n";
            ok.Clicked += OkClick;
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Styles.BackColor;
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
                                TextColor = Styles.UserColor,
                                FontSize = 24
                            },
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = Styles.BackColor,
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