using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XBot
{
    static class Formats
    {

        const char parse = '÷';

        public static string FromListIntoString(List<string> l)
        {
            if (l.Count == 0)
                return "";
            string line = "";
            for (int i = 0; i < l.Count; i++)
                line += l[i] + parse;
            return line.Substring(0, line.Length - 1);
        }

        public static List<string> FromStringIntoList(string line)
        {
            if (line == null || line.Length == 0)
                return new List<string>();
            return line.Split(parse).ToList<string>();
        }

    }
}
