using System;

using Xamarin.Forms;

namespace XBot
{
    public partial class MainPage : ContentPage
    {
        async void Command()
        {
            Chat.Add(message.Text, false);
            switch (message.Text.Split(' ')[0])
            {
                case "/light":
                    App.Current.Properties["back"] = "255 255 255";
                    App.Current.Properties["user"] = "0 0 255";
                    App.Current.Properties["bot"] = "128 0 128";
                    Chat.Add("Тема сменена на светлую", true);
                    break;
                case "/dark":
                    App.Current.Properties["back"] = "30 30 30";
                    App.Current.Properties["user"] = "86 156 214";
                    App.Current.Properties["bot"] = "80 80 80";
                    Chat.Add("Тема сменена на темную", true);
                    break;
                case "/news":
                    NewsClick(new object(), new EventArgs());
                    break;
                default:
                    Chat.Add("Неизвестная команда!", true);
                    break;

            }
            Display();
        }

    }
}

