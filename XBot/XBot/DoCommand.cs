using System;
namespace XBot
{
    public partial class MainPage
    {
        void DoCommand(string request)
        {
            string[] parse = request.ToLower().Split(' ');
            Chat.Add(request, false);
            MakeFrame();
            switch(parse[0])
            {
                case "/news":
                    NewsClick(new object(), new EventArgs());
                    break;
                case "/favs":
                    FavsClick(new object(), new EventArgs());
                    break;
                case "/clear":
                    App.Current.Properties["messages"] = "";
                    Display();
                    break;
                case "/dark":
                    App.Current.Properties["back"] = "30 30 30";
                    App.Current.Properties["user"] = "86 156 214";
                    App.Current.Properties["bot"] = "80 80 80";
                    Chat.Add("Установлена темная тема", true);
                    Display();
                    break;
                case "/light":
                    App.Current.Properties["back"] = "255 255 255";
                    App.Current.Properties["user"] = "0 0 255";
                    App.Current.Properties["bot"] = "128 0 128";
                    Chat.Add("Установлена светлая тема", true);
                    Display();
                    break;
                    
                case "/currency":
                    if (parse.Length > 1)
                        switch(parse[1])
                        {
                            case "add":

                        }
                    else
                        CurrencyClick(new object(), new EventArgs());
                    break;
                default:
                    Chat.Add("Неизвестная команда!", true);
                    Display();
                    break;
            }
        }
    }
}
