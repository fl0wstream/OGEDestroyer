using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGEDestroyer.API
{
    public class Test
    {
        public int Id { get; private set; }
        public int StatId { get; private set; }
        public int[] ProblemIds { get; private set; }
        public Dictionary<int, string> Answers { get; private set; }
        public int Timer { get; set; }

        public Test(int id, int statId, int timer)
        {
            Id = id;
            StatId = statId;
            Timer = timer;

            Answers = new Dictionary<int, string>();

            Console.WriteLine($"Test #{Id} w/ stat_id #{StatId} -> Created!");

            ProblemIds = TestParse.GetProblemNumbers(Id, StatId);
            
            for (int i = 0; i < ProblemIds.Count(); i++)
            {
                Answers.Add(i + 1, AnswerParse.GetAnswer(ProblemIds[i]));
                Console.WriteLine($"Test #{Id} -> {i + 1} = {AnswerParse.GetAnswer(ProblemIds[i])}");
            }
        }
    }
}
