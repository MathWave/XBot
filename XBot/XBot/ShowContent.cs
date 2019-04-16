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
            Text = "Сохранить",
            BackgroundColor = Styles.BackColor,
            TextColor = Styles.UserColor,
            BorderColor = Styles.UserColor,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.FillAndExpand
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
            save.Clicked += SaveClick;
            back.Clicked += BackClick;
            internet.Clicked += (object sender, EventArgs e) => Device.OpenUri(new Uri(url));
            NavigationPage.SetHasNavigationBar(this, false);
            List<string> favs = Formats.FromStringIntoList((string)App.Current.Properties["save"]);
            if (PositionInList(favs, $"{title}֍{url}") != -1)
                save.Text = "Удалить";
            Content = new StackLayout { Children = { new WebView { Source = "http://" + url, VerticalOptions = LayoutOptions.FillAndExpand }, new StackLayout { Children = { back, internet, save }, Orientation = StackOrientation.Horizontal } } };
        }

        async void SaveClick(object sender, EventArgs e)
        {
            List<string> favs = Formats.FromStringIntoList((string)App.Current.Properties["save"]);
            if (save.Text == "Сохранить")
            {
                favs.Add($"{title}֍{url}");
                save.Text = "Удалить";
            }
            else
            {
                favs.Remove($"{title}֍{url}");
                save.Text = "Сохранить";
                await Navigation.PopAsync();
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

