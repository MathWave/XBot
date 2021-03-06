﻿using System;
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

        public static int Size
        {
            get => (int)App.Current.Properties["size"];
        }

    }
}
