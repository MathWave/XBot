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
            ok.Clicked += (object sender, EventArgs e) => Navigation.PushAsync(new Intro5());
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
            MakeContent();
        }

        void MakeContent()
        {
            StackLayout sl = new StackLayout();
            string[] subs = new string[] { "очень мелкий", "мелкий", "средний", "большой", "очень большой" };
            int[] code = new int[] { 8, 10, 14, 18, 24 };
            for (int i = 0; i < subs.Length; i++)
            {
                Button button = new Button
                {
                    FontSize = code[i],
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
                    App.Current.Properties["size"] = code[int.Parse(((Button)sender).ClassId)];
                    MakeContent();
                };
                Frame newf = new Frame
                {
                    BorderColor = Styles.UserColor,
                    BackgroundColor = Styles.BackColor,
                    CornerRadius = 30,
                    Content = button
                };
                if ((int)App.Current.Properties["size"] == code[i])
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

