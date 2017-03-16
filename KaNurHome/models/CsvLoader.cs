using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace KaNurHome.models
{
    public class CsvLoader
    {
        public static Dictionary<string, List<string>> GetCsvDictionaly(System.IO.Stream stream)
        {
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            string[] heads = null;
            using (var sr = new System.IO.StreamReader(stream, System.Text.Encoding.Unicode))
            {
                while (sr.Peek() > -1)
                {
                    if (heads == null)
                    {
                        heads = sr.ReadLine().Split('\t').Select(m => m.Replace("\"", "")).ToArray();
                        dic = heads.ToDictionary(m => m.Replace("\"", ""), m => new List<string>());
                        continue;
                    }
                    var line = sr.ReadLine().Split('\t');
                    if (line.Length != heads.Length) continue;
                    for (var i = 0; i < line.Length; ++i)
                    {
                        dic[heads[i]].Add(line[i].Replace("\"", ""));
                    }
                }
            }

            return dic;
        }
    }
}