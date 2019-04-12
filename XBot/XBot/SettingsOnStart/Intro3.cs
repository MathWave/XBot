using System;

using Xamarin.Forms;

namespace XBot.SettingsOnStart
{
    public class Intro3 : ContentPage
    {
        Button ok;
        Button ignore;
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
        async void Help(object sender, EventArgs e)
        {
            await DisplayAlert("Помощь", "Выбери временной интервал поиска новостей", "Хорошо!");
        }
        async void Pop(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        string t = "\nОбласть поиска\n";
        public Intro3()
        {
            if (Device.RuntimePlatform == "iOS")
            {
                Title = "Область поиска";
                t = "";
            }
            else
                NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Styles.BackColor;
            help.Clicked += Help;
            MakeContent();
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
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            ok = new Button
            {
                Text = "Далее",
                BackgroundColor = Styles.BackColor,
                TextColor = Styles.UserColor,
                BorderColor = Styles.UserColor,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            ok.Clicked += (object sender, EventArgs e) => Navigation.PushAsync(new Intro4());
            ignore.Clicked += Pop;
            StackLayout sl = new StackLayout();
            string[] subs = new string[] { "10 минут", "час", "день", "неделя", "месяц" };
            string[] code = new string[] { "online", "hour", "day", "week", "month" };
            for (int i = 0; i < subs.Length; i++)
            {
                Button button = new Button
                {
                    FontSize = 20,
                    ClassId = i.ToString(),
                    FontAttributes = FontAttributes.Bold,
                    BorderColor = Styles.UserColor,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Styles.BackColor,
                    Text = subs[i],
                    TextColor = Styles.UserColor,
                    CornerRadius = 30
                };
                button.Clicked += (object sender, EventArgs e) =>
                {
                    App.Current.Properties["frequency"] = code[int.Parse(((Button)sender).ClassId)];
                    MakeContent();
                };
                Frame newf = new Frame
                {
                    BorderColor = Styles.UserColor,
                    BackgroundColor = Styles.BackColor,
                    CornerRadius = 30,
                    Content = button
                };
                if ((string)App.Current.Properties["frequency"] == code[i])
                {
                    button.TextColor = Color.White;
                    button.BackgroundColor = Styles.UserColor;
                    newf.BackgroundColor = Styles.UserColor;
                }
                sl.Children.Add(newf);
            }
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
                    new ScrollView { Content = sl, VerticalOptions = LayoutOptions.FillAndExpand },
                    new StackLayout
                    {
                        Children = {ignore, help, ok },
                        Orientation = StackOrientation.Horizontal
                    }
                }
            };
        }

    }
}

