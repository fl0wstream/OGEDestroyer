using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OGEDestroyer.API;

namespace OGEDestroyer
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            var questionNumber = 0;
            Int32.TryParse(Console.ReadLine(), out questionNumber);
            Console.WriteLine(AnswerParse.GetAnswer(questionNumber));

            Console.ReadKey();
        }
    }
}
