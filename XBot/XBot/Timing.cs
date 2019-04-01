using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class Timing : ContentPage
    {

        Entry entry = new Entry();

        public Timing()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Colors.BackColor;
            MakeContent();
        }

        void MakeContent()
        {
            StackLayout sl = new StackLayout();
            string[] subs = new string[] { "10 минут", "час", "день", "неделя", "месяц" };
            string[] code = new string[] { "online", "hour", "day", "week", "month" };
            for (int i = 0; i < subs.Length; i++)
            {
                Button button = new Button
                {
                    Text = "✔",
                    FontSize = 20,
                    ClassId = i.ToString(),
                    FontAttributes = FontAttributes.Bold,
                    BorderColor = Colors.UserColor,
                    HorizontalOptions = LayoutOptions.Start,
                    BackgroundColor = Colors.BackColor
                };
                button.Clicked += (object sender, EventArgs e) =>
                {
                    App.Current.Properties["frequency"] = code[int.Parse(((Button)sender).ClassId)];
                    MakeContent();
                };
                button.TextColor = (string)App.Current.Properties["frequency"] == code[i] ? Color.Green : Colors.UserColor;
                Frame newf = new Frame
                {
                    BorderColor = Colors.UserColor,
                    BackgroundColor = Colors.BackColor,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            button,
                            new Label
                            {
                                Text = subs[i],
                                TextColor = Colors.UserColor,
                                HorizontalTextAlignment = TextAlignment.Start,
                                VerticalTextAlignment = TextAlignment.Center
                            }
                        },
                        Orientation = StackOrientation.Horizontal
                    }
                };
                sl.Children.Add(newf);
            }
            Content = new StackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = "\nОбласть поиска\n",
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontSize = 20,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        TextColor = Colors.UserColor,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Colors.BackColor
                    },
                    new ScrollView { Content = sl }
                }
            };
        }

    }
}