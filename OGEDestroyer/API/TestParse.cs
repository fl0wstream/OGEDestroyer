using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;

namespace OGEDestroyer.API
{
    public static class TestParse
    {
        public static int[] GetProblemNumbers(int testNumber, int statId)
        {
            var url = $"https://rus-ege.sdamgia.ru/test?print=true&continue={statId}&id={testNumber}&svg=0&num=true";
            var htmlPage = string.Empty;
            var numbers = new List<int>();

            using (var webClient = new WebClient())
            {
                webClient.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1");
                webClient.Encoding = Encoding.UTF8;

                try
                {
                    htmlPage = webClient.DownloadString(url);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                foreach (var number in htmlPage.Split('"').Where(x => x.Contains("maindiv") && !x.Contains("prob")))
                {
                    int numberId = Convert.ToInt32(number.Replace("maindiv", string.Empty));

                    if (!numbers.Contains(numberId))
                        numbers.Add(numberId);
                }
            }

            return numbers.ToArray();
        }
    }
}
