using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XBot
{
    static class Styles
    {

        public static Xamarin.Forms.Color BackColor
        {
            get 
            {
                string[] c = ((string)App.Current.Properties["back"]).Split(' ');
                return System.Drawing.Color.FromArgb(int.Parse(c[0]), int.Parse(c[1]), int.Parse(c[2]));
            }
        }

        public static Xamarin.Forms.Color UserColor
        {
            get 
            {
                string[] c = ((string)App.Current.Properties["user"]).Split(' ');
                return System.Drawing.Color.FromArgb(int.Parse(c[0]), int.Parse(c[1]), int.Parse(c[2]));
            }
        }

        public static Xamarin.Forms.Color BotColor
        {
            get 
            {
                string[] c = ((string)App.Current.Properties["bot"]).Split(' ');
                return System.Drawing.Color.FromArgb(int.Parse(c[0]), int.Parse(c[1]), int.Parse(c[2]));
            }
        }

        public static Xamarin.Forms.Color UserBackColor
        {
            get
            {
                Color c = UserColor;
                return Color.FromRgb(c.R * 1.2, c.G * 1.2, c.B * 1.2);
            }
        }

        public static int Size
        {
            get 
            {
                return (int)App.Current.Properties["size"];
            }
        }

        /*
        public static string Title(string title, ref NavigationPage p)
        {
            if (Device.RuntimePlatform == "iOS")
            {
                p.Title = title;
                return "";
            }
            else
            {
                NavigationPage.SetHasNavigationBar(p, false);
                return $"\n{title}\n";
            }
        }
        */

    }
}
