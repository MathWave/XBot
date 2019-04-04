using System;

using Xamarin.Forms;

namespace XBot
{
    public class Support : ContentPage
    {
        Button ok = new Button
        {
            Text = "Поддержать",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.CenterAndExpand
        };
        public Support()
        {
            ok.Clicked += (object sender, EventArgs e) => Device.OpenUri(new Uri("https://money.yandex.ru/to/410014676758208"));
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
                                Text =
                                "Большое спасибо за скачивание и использование моего приложения!\n\n" +
                                "Я верю в то, что оно тебе нравится, и ты поставил ему оценку в магазине приложений.\n\n" +
                                "Если хочешь поддержать меня материально, а также сделать вклад в будущие проекты, нажми на кнопку ниже😉",
                                TextColor = Styles.UserColor,
                                FontSize = 24
                            },
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = Styles.BackColor,
                            HasShadow = false
                        },
                        VerticalOptions = LayoutOptions.FillAndExpand
                    },
                    Elements.Button(ok)
                }
            };
        }

    }
}

