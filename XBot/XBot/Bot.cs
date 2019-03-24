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
            m.Active(false);
            string line = "";
            string mess = "";
            int count = 0;
            try
            {
                line = await client.GetStringAsync("http://mediametrics.ru/rating/ru/online.tsv?page=1&update=1401216280");
                string[] elems = line.Split('\n');
                for (int i = 1; count < (int)App.Current.Properties["count"] && i < elems.Length; i++)
                {
                    string[] first = elems[i].Split('\t');
                    string info = await client.GetStringAsync("http://mediametrics.ru/rating/index.tsv?titles=" + first[5]);
                    string[] elems1 = info.Split('\t');
                    if (!Contains(elems1[1]))
                    {
                        mess += elems1[1] + "\n֍" + elems1[0] + "֍";
                        count++;
                    }
                }
            }
            catch (IndexOutOfRangeException) { }
            catch { line = ""; }
            mess += '\b';
            Chat.Remove();
            mess = $"Топ-{count} новостей на {DateTime.Now.ToString()}\n\n֍֍" + mess;
            if (line == null || line.Length == 0)
                Chat.Add("Отсутствует подключение к интернету", true);
            else if (mess.Length == 0 || count == 0)
                Chat.Add("Поиск не дал результатов", true);
            else
                Chat.Add(mess.Replace("&quot;", "\"").Replace("&amp;", "\""), true);
            m.Display();
            m.Active(true);
        }

        async public static void Search(MainPage m, IEnumerable<string> requests)
        {
            m.Active(false);
            int count = 1;
            int amount = 0;
            string mess = "";
            string line = "";
            try
            {
                while (count != 4 && amount < (int)App.Current.Properties["count"])
                {
                    line = await client.GetStringAsync("http://mediametrics.ru/rating/ru/online.tsv?page=" + count.ToString() + "&update=1401216280");
                    string[] elems = line.Split('\n');
                    for (int i = 1; i < elems.Length; i++)
                    {
                        string[] first = elems[i].Split('\t');
                        string info = await client.GetStringAsync("http://mediametrics.ru/rating/index.tsv?titles=" + first[5]);
                        string[] elems1 = info.Split('\t');
                        foreach (string str in requests)
                        {
                            if (elems1[1].ToLower().Contains(str.ToLower()) && !Contains(elems1[1]))
                            {
                                mess += elems1[1] + "\n֍" + elems1[0] + "֍";
                                amount++;
                            }
                            if (amount == (int)App.Current.Properties["count"])
                                goto go;
                        }
                        count++;
                    }
                }
            }
            catch { }
            go:
            Chat.Remove();
            if (line == null || line.Length == 0)
                Chat.Add("Отсутствует подключение к интернету", true);
            else if (mess.Length == 0)
                Chat.Add("Поиск не дал результатов", true);
            else
                Chat.Add(($"Топ-{amount} подписок на {DateTime.Now.ToString()}\n\n֍֍" + mess + "\b\b\b").Replace("&quot;", "\"").Replace("&amp;", "\""), true);
            m.Display();
            m.Active(true);
        }

        static bool Contains(string news)
        {
            if (!(bool)App.Current.Properties["blocked"])
                return false;
            List<string> subs = Formats.FromStringIntoList((string)App.Current.Properties["control"]);
            foreach (string str in subs)
                if (news.ToLower().Contains(str.ToLower()))
                    return true;
            return false;
        }

    }
}
