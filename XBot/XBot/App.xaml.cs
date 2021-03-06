﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XBot
{
    public partial class App : Application
    {
    
        public App()
        {
            object obj;
            if (!App.Current.Properties.TryGetValue("messages", out obj))
                App.Current.Properties["messages"] = "";
            if (!App.Current.Properties.TryGetValue("subscribes", out obj))
                App.Current.Properties["subscribes"] = "";
            if (!App.Current.Properties.TryGetValue("count", out obj))
                App.Current.Properties["count"] = 5;
            if (!App.Current.Properties.TryGetValue("onstart", out obj))
                App.Current.Properties["onstart"] = "news";
            if (!App.Current.Properties.TryGetValue("back", out obj))
                App.Current.Properties["back"] = "255 255 255";
            if (!App.Current.Properties.TryGetValue("user", out obj))
                App.Current.Properties["user"] = "0 0 255";
            if (!App.Current.Properties.TryGetValue("bot", out obj))
                App.Current.Properties["bot"] = "128 0 128";
            if (!App.Current.Properties.TryGetValue("control", out obj))
                App.Current.Properties["control"] = "";
            if (!App.Current.Properties.TryGetValue("blocked", out obj))
                App.Current.Properties["blocked"] = false;
            if (!App.Current.Properties.TryGetValue("welcome", out obj))
                App.Current.Properties["welcome"] = true;
            if (!App.Current.Properties.TryGetValue("subscribes_intro", out obj))
                App.Current.Properties["subscribes_intro"] = true;
            if (!App.Current.Properties.TryGetValue("control_intro", out obj))
                App.Current.Properties["control_intro"] = true;
            if (!App.Current.Properties.TryGetValue("hint", out obj))
                App.Current.Properties["hint"] = true;
            if (!App.Current.Properties.TryGetValue("frequency", out obj))
                App.Current.Properties["frequency"] = "online";
            if (!App.Current.Properties.TryGetValue("frequency_intro", out obj))
                App.Current.Properties["frequency_intro"] = true;
            if (!App.Current.Properties.TryGetValue("size", out obj))
                App.Current.Properties["size"] = 14;
            if (!App.Current.Properties.TryGetValue("size_intro", out obj))
                App.Current.Properties["size_intro"] = true;
            if (!App.Current.Properties.TryGetValue("type", out obj))
                App.Current.Properties["type"] = "";
            if (!App.Current.Properties.TryGetValue("type_intro", out obj))
                App.Current.Properties["type_intro"] = true;
            if (!App.Current.Properties.TryGetValue("currency", out obj))
                App.Current.Properties["currency"] = "";
            if (!App.Current.Properties.TryGetValue("currency_intro", out obj))
                App.Current.Properties["currency_intro"] = true;
            if (!App.Current.Properties.TryGetValue("save", out obj))
                App.Current.Properties["save"] = "";
            if ((string)App.Current.Properties["back"] == "30 30 30")
            {
                App.Current.Properties["back"] = "30 30 30";
                App.Current.Properties["user"] = "86 156 214";
                App.Current.Properties["bot"] = "80 80 80";
            }
            else
            {
                App.Current.Properties["back"] = "255 255 255";
                App.Current.Properties["user"] = "0 0 255";
                App.Current.Properties["bot"] = "128 0 128";
            }
            MainPage = (bool)App.Current.Properties["welcome"] ? new NavigationPage(new Welcome()) : (bool)App.Current.Properties["hint"] ? new NavigationPage(new Hint()) : new NavigationPage(new MainPage());
        }

    }
}
