using System;
using System.Collections.Generic;
using System.Text;

namespace XBot
{
    static class Chat
    {
        public static void Add(string message, bool sender)
        {
            string mess = "";
            if (sender)
                mess += "B";
            else
                mess += "U";
            mess += message;
            Formats.Add(mess, "messages");
        }

        public static void Remove()
        {
            List<string> mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
            mes.RemoveAt(mes.Count - 1);
            App.Current.Properties["messages"] = Formats.FromListIntoString(mes);
        }
    }
}
