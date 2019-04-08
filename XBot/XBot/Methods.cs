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
                App.Current.Properties["bot"] = "80 80 80";
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

        void CurrencyClick(object sender, EventArgs e)
        {
            if ((bool)App.Current.Properties["currency_intro"])
                Navigation.PushAsync(new CurrencyIntro());
            else
                Navigation.PushAsync(new Currency());
        }

        void TypeClick(object sender, EventArgs e)
        {
            if ((bool)App.Current.Properties["type_intro"])
                Navigation.PushAsync(new TypeIntro());
            else
                Navigation.PushAsync(new Type());
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

        void ControlClick(object sender, EventArgs e)
        {
            if ((bool)App.Current.Properties["control_intro"])
                Navigation.PushAsync(new ControlIntro());
            else
                Navigation.PushAsync(new Password());
        }

        void SizeClick(object sender, EventArgs e)
        {
            if ((bool)App.Current.Properties["size_intro"])
                Navigation.PushAsync(new SizeIntro(main));
            else
                Navigation.PushAsync(new Size(main));
        }
    }
}
