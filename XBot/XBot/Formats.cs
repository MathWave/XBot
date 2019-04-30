using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBot
{
    static class Formats
    {

        public const char parse = '÷';

        public static string FromListIntoString(List<string> l)
        {
            if (l.Count == 0)
                return "";
            string line = "";
            for (int i = 0; i < l.Count; i++)
                line += l[i].Replace(parse.ToString(), parse.ToString() + parse.ToString()) + parse;
            return line.Substring(0, line.Length - 1);
        }

        public static List<string> FromStringIntoList(string line)
        {
            if (line == null || line.Length == 0)
                return new List<string>();
            string word = "";
            List<string> l = new List<string>();
            int i = 0;
            while (i < line.Length)
            {
                if (line[i] != parse)
                    word += line[i];
                else if (line[i + 1] == parse)
                    word += line[i++];
                else
                {
                    l.Add(word);
                    word = "";
                }
                i++;
            }
            l.Add(word);
            return l;
        }

        public static void Add(string what, string where)
        {
            List<string> list = FromStringIntoList((string)App.Current.Properties[where]);
            list.Add(what);
            App.Current.Properties[where] = FromListIntoString(list);
        }

        public static void Remove(string what, string where)
        {
            List<string> list = FromStringIntoList((string)App.Current.Properties[where]);
            list.Remove(what);
            App.Current.Properties[where] = FromListIntoString(list);
        }

    }
}
