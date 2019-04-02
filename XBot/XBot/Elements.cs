using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XBot
{

    public static class Elements
    {
        public static Frame LabelAndElement(string labeltext, View element)
        {
            return new Frame
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        new Label
                        {
                            TextColor = Styles.UserColor,
                            Text = labeltext,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalTextAlignment = TextAlignment.Start
                        },
                        element
                    },
                    Orientation = StackOrientation.Horizontal
                },
                BorderColor = Styles.UserColor,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Styles.BackColor
            };
        }

        public static Frame Button(Button b)
        {
            return new Frame
            {
                Content = b,
                BorderColor = Styles.UserColor,
                BackgroundColor = Styles.BackColor
            };
        }
    }
}
