using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace OGEDestroyer.API
{
    static class AnswerParse
    {
        public static string[] Pattern = new string[] { "правильный ответ указан под номером", "ответ:", "в словосочетании", "главное слово" };

        public static string GetAnswer(int answerId)
        {
            return FormatAnswer(FindAnswer(ParseAnswerText(answerId)));
        }

        private static string ParseAnswerText(int answerId)
        {
            string url = "https://rus-ege.sdamgia.ru/problem?id=" + answerId;
            string htmlText = string.Empty;

            using (var webClient = new WebClient())
            {
                webClient.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:40.0) Gecko/20100101 Firefox/40.1");
                webClient.Encoding = Encoding.UTF8;

                try
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(webClient.DownloadString(url));

                    htmlText = doc.GetElementbyId("sol" + answerId).InnerText;
                    htmlText.Replace("&shy;", "");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return htmlText.ToLower();
        }

        private static string FindAnswer(string answerText)
        {
            string answer = string.Empty;

            if (answerText.Contains(Pattern[0]))
            {
                int indexPattern = answerText.IndexOf(Pattern[0]) + Pattern[0].Length;
                answer = answerText.Substring(indexPattern, answerText.Length);
            }
            else if (answerText.Contains(Pattern[1]))
            {
                int indexPattern = answerText.IndexOf(Pattern[1]) + Pattern[1].Length;

                int dotIndex = answerText.IndexOf('.', indexPattern);
                if (dotIndex == -1)
                {
                    answer = answerText.Substring(indexPattern, answerText.Length - indexPattern);
                }
                else
                {
                    answer = answerText.Substring(indexPattern, dotIndex - indexPattern);
                }
            }
            else if (answerText.Contains(Pattern[2]) && answerText.Contains(Pattern[3]))
            {
                int indexPattern2 = answerText.IndexOf(Pattern[2]) + Pattern[2].Length;
                int indexPattern3 = answerText.IndexOf(Pattern[3]);
                answer = answerText.Substring(indexPattern2, indexPattern3);
            }

            return answer;
        }

        private static string FormatAnswer(string answer)
        {
            var formattedAnswer = Regex.Replace(answer, "[,. ]+", string.Empty);

            if (Regex.Replace(formattedAnswer, "\\D", string.Empty).Length >= 2)
            {
                Regex.Replace(formattedAnswer, "\\D", string.Empty);
            }
            else if(formattedAnswer.Contains("|"))
            {
                var lastIndex = formattedAnswer.LastIndexOf("|") + 1;
                formattedAnswer = formattedAnswer.Substring(lastIndex, formattedAnswer.Length - lastIndex);
            }
            else if(formattedAnswer.Contains("или"))
            {
                formattedAnswer = formattedAnswer.Substring(formattedAnswer.LastIndexOf("или") + 3, formattedAnswer.Length - formattedAnswer.LastIndexOf("или") + 3);
            }

            return formattedAnswer;
        }
    }
}
