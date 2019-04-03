using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XBot
{
    public class Timing : ContentPage
    {

        public Timing()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BackgroundColor = Styles.BackColor;
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
                    BorderColor = Styles.UserColor,
                    HorizontalOptions = LayoutOptions.Start,
                    BackgroundColor = Styles.BackColor
                };
                button.Clicked += (object sender, EventArgs e) =>
                {
                    App.Current.Properties["frequency"] = code[int.Parse(((Button)sender).ClassId)];
                    MakeContent();
                };
                button.TextColor = (string)App.Current.Properties["frequency"] == code[i] ? Color.Green : Styles.UserColor;
                Frame newf = new Frame
                {
                    BorderColor = Styles.UserColor,
                    BackgroundColor = Styles.BackColor,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            button,
                            new Label
                            {
                                Text = subs[i],
                                TextColor = Styles.UserColor,
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
                        TextColor = Styles.UserColor,
                        FontAttributes = FontAttributes.Bold,
                        BackgroundColor = Styles.BackColor
                    },
                    new ScrollView { Content = sl }
                }
            };
        }

    }
}