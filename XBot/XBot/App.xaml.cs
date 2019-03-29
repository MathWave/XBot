using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XBot
{
    public partial class App : Application
    {
        public App()
        {
            object obj;
            if (!App.Current.Properties.TryGetValue("messages", out obj))
            {
                App.Current.Properties["messages"] = "";
                App.Current.Properties["subscribes"] = "";
                App.Current.Properties["count"] = 5;
                App.Current.Properties["onstart"] = "news";
                App.Current.Properties["back"] = "255 255 255";
                App.Current.Properties["user"] = "0 0 255";
                App.Current.Properties["bot"] = "128 0 128";
                App.Current.Properties["control"] = "";
                App.Current.Properties["blocked"] = false;
                App.Current.Properties["welcome"] = true;
                App.Current.Properties["subscribes_intro"] = true;
                App.Current.Properties["control_intro"] = true;
                App.Current.Properties["hint"] = true;
            }
            MainPage = (bool)App.Current.Properties["welcome"] ? new NavigationPage(new Welcome()) : (bool)App.Current.Properties["hint"] ? new NavigationPage(new Hint()) : new NavigationPage(new MainPage());
        }

    }
}
