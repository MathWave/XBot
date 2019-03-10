using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace XBot
{
    static class Bot
    {
     
        private static readonly HttpClient client = new HttpClient();

        async public static void GetNews(MainPage m)
        {            
            List<string> mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
            string line = "";
            string mess = "";
            mess += $"Топ-{(int)App.Current.Properties["count"]} новостей на {DateTime.Now.ToString()}\n\n֍֍";
            try
            {
                line = await client.GetStringAsync("http://mediametrics.ru/rating/ru/online.tsv?page=1&update=1401216280");
                string[] elems = line.Split('\n');
                for (int i = 1; i <= (int)App.Current.Properties["count"]; i++)
                {
                    string[] first = elems[i].Split('\t');
                    string info = await client.GetStringAsync("http://mediametrics.ru/rating/index.tsv?titles=" + first[5]);
                    string[] elems1 = info.Split('\t');
                    mess += elems1[1] + "\n֍" + elems1[0] + "֍";
                }
            }
            catch { line = ""; }
            mess += '\b';
            mes.RemoveAt(mes.Count - 1);
            if (line == null || line.Length == 0 || mess == "")
                mes.Add("BОтсутствует подключение к интернету");
            else
                mes.Add("B" + mess.Replace("&quot;", "\"").Replace("&amp;", "\""));
            App.Current.Properties["messages"] = Formats.FromListIntoString(mes);
            m.Display();
        }

        async public static void Search(MainPage m, IEnumerable<string> requests)
        {
            List<string> mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
            int count = 1;
            int amount = 0;
            string mess = "";
            string line = "";
            try
            {
                while (count != 3 && amount < (int)App.Current.Properties["count"])
                {
                    line = await client.GetStringAsync("http://mediametrics.ru/rating/ru/online.tsv?page=" + count.ToString() + "&update=1401216280");
                    string[] elems = line.Split('\n');
                    for (int i = 1; i < elems.Length; i++)
                    {
                        string[] first = elems[i].Split('\t');
                        string info = await client.GetStringAsync("http://mediametrics.ru/rating/index.tsv?titles=" + first[5]);
                        string[] elems1 = info.Split('\t');
                        string article = elems1[1];
                        string link = elems1[0];
                        foreach (string str in requests)
                            if (article.ToLower().Contains(str.ToLower()))
                            {
                                mess += elems1[1] + "\n֍" + elems1[0] + "֍";
                                amount++;
                                break;
                            }
                        if (amount == 5)
                            break;
                        count++;
                    }
                }
            }
            catch { }
            mes.RemoveAt(mes.Count - 1);
            if (line == null || line.Length == 0)
                mes.Add("BОтсутствует подключение к интернету");
            else if (mess.Length == 0)
                mes.Add("BПоиск не дал результатов");
            else
                mes.Add(($"BТоп-{amount} подписок на {DateTime.Now.ToString()}\n\n֍֍" + mess + "\b\b\b").Replace("&quot;", "\"").Replace("&amp;", "\""));
            App.Current.Properties["messages"] = Formats.FromListIntoString(mes);
            m.Display();
        }

        async public static void FastSearch(MainPage m, string str)
        {
            List<string> mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
            int count = 1;
            List<string> strings = new List<string>();
            string[] infos = new string[1];
            while (infos.Length != 9)
            {
                string line = await client.GetStringAsync("http://mediametrics.ru/rating/ru/online.tsv?page=" + count.ToString() + "&update=1401216280");
                infos = line.Split('\t');

            }
            List<string> news = new List<string>();
            try
            {
                while (count != 3 && news.Count < 5)
                {
                    string line = await client.GetStringAsync("http://mediametrics.ru/rating/ru/online.tsv?page=" + count.ToString() + "&update=1401216280");
                    string[] elems = line.Split('\n');
                    for (int i = 1; i < elems.Length; i++)
                    {
                        string[] first = elems[i].Split('\t');
                        string info = await client.GetStringAsync("http://mediametrics.ru/rating/index.tsv?titles=" + first[5]);
                        string[] elems1 = info.Split('\t');
                        string article = elems1[1];
                        if (article.ToLower().Contains(str.ToLower()))
                            news.Add(article);
                        if (news.Count == 5)
                            break;
                        count++;
                    }
                }
            }
            catch { }
            string mess = "";
            mes.RemoveAt(mes.Count - 1);
            foreach (string art in news)
                mess += art.Replace("&quot;", "\"") + "\n\n";
            if (mess.Length == 0)
                mes.Add("BПоиск не дал результатов");
            else
                mes.Add(("B" + mess + "\b\b\b").Replace("&quot;", "\""));
            App.Current.Properties["messages"] = Formats.FromListIntoString(mes);
            m.MakeFrame();
            m.MakeContent();
        }



        /*
        //public static string news;
        //static string sender = "https://api.telegram.org/bot710143249:AAHCpMfVfSKgoRdwAnfVJ1j3N8LAEVUyLG4/";
        //static string receiver = "https://api.telegram.org/bot716832978:AAFENDh7xeOZot_wVAWJ3XMBROUmmY6NLhU/";
        public static void Activate(MainPage m)
        {
            GetUrl();
            List<string> mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
            mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
            mes.Add("BБот готов к работе");
            App.Current.Properties["messages"] = Formats.FromListIntoString(mes);
            m.MakeFrame();
            m.MakeContent();
        }

        async static void GetUrl()
        {
            news = await client.GetStringAsync("https://api.telegram.org/bot716832978:AAFENDh7xeOZot_wVAWJ3XMBROUmmY6NLhU/getUpdates");
        }

        async public static void send_mes(string mes)
        {
            news = await client.GetStringAsync(sender + "sendMessage?chat_id=-1001411015706&text=" + mes);
        }

        async public static void check(MainPage m)
        {
            try
            {
                while (true)
                {
                    bool flag = true;
                    string new_news = "";
                    try { new_news = await client.GetStringAsync("https://api.telegram.org/bot716832978:AAFENDh7xeOZot_wVAWJ3XMBROUmmY6NLhU/getUpdates"); }
                    catch
                    {
                        flag = false;
                    }
                    List<string> mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
                    if ((new_news.Length != news.Length) && flag)
                    {
                        mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
                        string line = await client.GetStringAsync("http://mediametrics.ru/rating/ru/online.tsv?page=1&update=1401216280");
                        string[] elems = line.Split('\n');
                        string mess = "";
                        for (int i = 1; i < 6; i++)
                        {
                            string[] first = elems[i].Split('\t');
                            string info = await client.GetStringAsync("http://mediametrics.ru/rating/index.tsv?titles=" + first[5]);
                            string[] elems1 = info.Split('\t');
                            mess += elems1[1] + '\n';
                        }
                        mes.Add(mess + '\b');
                        mes.Add("BСервер принял новое сообщение");
                        App.Current.Properties["messages"] = Formats.FromListIntoString(mes);
                        m.MakeFrame();
                        m.MakeContent();
                        news = new_news;
                    }
                    break;
                }
            }
            catch
            {
                List<string> mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
                mes = Formats.FromStringIntoList((string)App.Current.Properties["messages"]);
                mes.Add("BБот перегружен, подождите, пока он не восстановится");
                App.Current.Properties["messages"] = Formats.FromListIntoString(mes);
                m.MakeFrame();
                m.MakeContent();
            }
        }
    */
    }
}
