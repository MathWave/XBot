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
                line = await client.GetStringAsync($"http://mediametrics.ru/rating/{(string)App.Current.Properties["type"]}ru/{(string)App.Current.Properties["frequency"]}.tsv?page=1&update=1401216280");
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
            catch (Exception ex) 
            { 
                string a = ex.ToString();
                line = "";
            }
            mess += '\b';
            Chat.Remove();
            mess = $"Топ-{count} новостей на {Now} за {Period}\n\n֍֍" + mess;
            if (line == null || line.Length == 0)
                Chat.Add("Отсутствует подключение к интернету", true);
            else if (mess.Length == 0 || count == 0)
                Chat.Add("Поиск не дал результатов", true);
            else
                Chat.Add(mess.Replace("&quot;", "\"").Replace("&amp;", "\""), true);
            m.Display();
            m.Active(true);
        }

        async public static void Search(MainPage m, IEnumerable<string> requests, bool type)
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
                    line = await client.GetStringAsync($"http://mediametrics.ru/rating/{(string)App.Current.Properties["type"]}ru/{(string)App.Current.Properties["frequency"]}.tsv?page=" + count.ToString() + "&update=1401216280");
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
            string name = "";
            if (type)
                name = "подписок";
            else
                name = $"новостей по запросу \"{FromIEnumerable(requests)}\"";
            if (line == null || line.Length == 0)
                Chat.Add("Отсутствует подключение к интернету", true);
            else if (mess.Length == 0)
                Chat.Add("Поиск не дал результатов", true);
            else
                Chat.Add(($"Топ-{amount} {name} на {Now} за {Period}\n\n֍֍" + mess + "\b\b\b").Replace("&quot;", "\"").Replace("&amp;", "\""), true);
            m.Display();
            m.Active(true);
        }

        async public static void Currency(MainPage m)
        {
            m.Active(false);
            string line = "";
            string dollar = "";
            string euro = "";
            try
            {
                byte[] s = await client.GetByteArrayAsync("https://www.cbr-xml-daily.ru/daily_json.js");
                line = Encoding.UTF8.GetString(s, 0, s.Length);
                string[] vl = line.Split('\n');
                dollar = vl[102].Split('"')[2].Substring(2, 5);
                euro = vl[111].Split('"')[2].Substring(2, 5);
            }
            catch { }
            Chat.Remove();
            if (line == "")
                Chat.Add("Отсутствует подключение к интернету", true);
            else
                Chat.Add($"CКурс валют на {Now}\n$ {dollar}\n€ {euro}", true);
            m.Display();
            m.Active(true);
        }

        static string FromIEnumerable(IEnumerable<string> arr)
        {
            string line = "";
            foreach (string s in arr)
                line += s + " ";
            return line.Substring(0, line.Length - 1);
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

        static string Period
        {
            get 
            {
                switch ((string)App.Current.Properties["frequency"])
                {
                    case "online":
                        return "последние 10 минут";
                    case "hour":
                        return "последний час";
                    case "day":
                        return "последние сутки";
                    case "week":
                        return "последнюю неделю";
                    default:
                        return "последний месяц";
                }
            }
        }

        static string Now
        {
            get {
                string datetime = DateTime.Now.ToString();
                string line = "";
                if (datetime[0] == '0')
                    datetime = datetime.Substring(1);
                string[] d1 = datetime.Split(' ');
                string[] date;
                if (!DateTime.Now.ToString().Contains("/"))
                {
                    date = d1[0].Split('.');
                    line += $"{date[0]} {Month(date[1])} ";
                }
                else
                {
                    date = d1[0].Split('/');
                    line += $"{date[1]} {Month(date[0])} ";
                }
                string[] time = d1[1].Split(':');
                line += time[0] + ":" + time[1];
                return line;
            }
        }

        static string Month(string num)
        {
            if (num[0] == '0')
                num = num.Substring(1);
            switch (int.Parse(num))
            {
                case 1:
                    return "января";
                case 2:
                    return "февраля";
                case 3:
                    return "марта";
                case 4:
                    return "апреля";
                case 5:
                    return "мая";
                case 6:
                    return "июня";
                case 7:
                    return "июля";
                case 8:
                    return "августа";
                case 9:
                    return "сентбря";
                case 10:
                    return "октября";
                case 11:
                    return "ноября";
                default:
                    return "декабря";
            }
        }

    }
}
