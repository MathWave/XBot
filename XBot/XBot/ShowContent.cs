using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XBot
{
    public class ShowContent : ContentPage
    {

        string title;
        string url;

        Button save = new Button
        {
            Text = "☆",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            FontSize = 20
        };

        Button back = new Button
        {
            Text = "Назад",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        Button internet = new Button
        {
            Text = "🌎",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.FillAndExpand
        };

        public ShowContent(string url, string title)
        {
            this.title = title;
            this.url = url;
            BackgroundColor = Styles.BackColor;
            back.Clicked += BackClick;
            internet.Clicked += (object sender, EventArgs e) => Device.OpenUri(new Uri("http://" + url));
            NavigationPage.SetHasNavigationBar(this, false);
            List<string> favs = Formats.FromStringIntoList((string)App.Current.Properties["save"]);
            if (PositionInList(favs, $"{title}֍{url}") != -1)
                save = new Button
                {
                    Text = "★",
                    BackgroundColor = Styles.BackColor,
                    TextColor = Color.Yellow,
                    BorderColor = Color.Yellow,
                    VerticalOptions = LayoutOptions.End,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    FontSize = 20
                };
            save.Clicked += SaveClick;
            Content = new StackLayout { Children = { new WebView { Source = "http://" + url, VerticalOptions = LayoutOptions.FillAndExpand }, new StackLayout { Children = { back, internet, save }, Orientation = StackOrientation.Horizontal } } };
        }

        void SaveClick(object sender, EventArgs e)
        {
            List<string> favs = Formats.FromStringIntoList((string)App.Current.Properties["save"]);
            if (save.Text == "☆")
            {
                favs.Add($"{title}֍{url}");
                save.Text = "★";
                save.TextColor = Color.Yellow;
            }
            else
            {
                favs.Remove($"{title}֍{url}");
                save.Text = "☆";
                save.TextColor = Styles.UserColor;
            }
            App.Current.Properties["save"] = Formats.FromListIntoString(favs);
        }

        async void BackClick(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        static int PositionInList(List<string> list, string line)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i] == line)
                    return i;
            return -1;
        }

    }
}