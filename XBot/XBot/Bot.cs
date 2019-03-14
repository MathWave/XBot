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
            Chat.Remove();
            if (line == null || line.Length == 0 || mess == "")
                Chat.Add("Отсутствует подключение к интернету", true);
            else
                Chat.Add(mess.Replace("&quot;", "\"").Replace("&amp;", "\""), true);
            m.Display();
        }

        async public static void Search(MainPage m, IEnumerable<string> requests)
        {
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
                            if (elems1[1].ToLower().Contains(str.ToLower()))
                            {
                                mess += elems1[1] + "\n֍" + elems1[0] + "֍";
                                amount++;
                                break;
                            }
                            if (amount == (int)App.Current.Properties["count"])
                                break;
                        }
                        count++;
                    }
                }
            }
            catch { }
            Chat.Remove();
            if (line == null || line.Length == 0)
                Chat.Add("Отсутствует подключение к интернету", true);
            else if (mess.Length == 0)
                Chat.Add("Поиск не дал результатов", true);
            else
                Chat.Add(($"Топ-{amount} подписок на {DateTime.Now.ToString()}\n\n֍֍" + mess + "\b\b\b").Replace("&quot;", "\"").Replace("&amp;", "\""), true);
            m.Display();
        }

    }
}
