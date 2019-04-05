using System;
namespace XBot
{
    public partial class Settings
    {

        void MakeDark(object sender, EventArgs e)
        {
            if ((string)App.Current.Properties["back"] == "30 30 30")
            {
                App.Current.Properties["back"] = "255 255 255";
                App.Current.Properties["user"] = "0 0 255";
                App.Current.Properties["bot"] = "128 0 128";
            }
            else
            {
                App.Current.Properties["back"] = "30 30 30";
                App.Current.Properties["user"] = "86 156 214";
                App.Current.Properties["bot"] = "255 255 255";
            }
            main.Display();
            MakeContent();
        }

        void TimeClick(object sender, EventArgs e)
        {
            if ((bool)App.Current.Properties["frequency_intro"])
                Navigation.PushAsync(new TimingIntro());
            else
                Navigation.PushAsync(new Timing());
        }

        void ClearClick(object sender, EventArgs e)
        {
            App.Current.Properties["messages"] = "";
            main.Display();
        }

        void SubsClick(object sender, EventArgs e)
        {
            if ((bool)App.Current.Properties["subscribes_intro"])
                Navigation.PushAsync(new SubscribesIntro());
            else
                Navigation.PushAsync(new Subscribes());
        }
    }
}
