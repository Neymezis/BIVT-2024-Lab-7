using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Purple_4;
using static Lab_7.Purple_5;

namespace Lab_7
{
    public class Purple_5
    {
        public struct Response
        {
            private string _animal;
            private string _characterTrait;
            private string _concept;
            public string Animal => _animal;
            public string CharacterTrait => _characterTrait;
            public string Concept => _concept;
            public Response(string animal, string charactertrait, string concept)
            {
                _animal = animal;
                _characterTrait = charactertrait;
                _concept = concept;
            }
            public int CountVotes(Response[] responses, int questionNumber)
            {
                if (responses == null || questionNumber < 1 || questionNumber > 3) return 0;

                if (questionNumber == 1)
                {
                    string answer = _animal;
                    return responses.Count(one => one.Animal == answer && one.Animal != null);
                }

                else if (questionNumber == 2)
                {
                    string answer2 = _characterTrait;
                    return responses.Count(one => one.CharacterTrait == answer2 && one.CharacterTrait != null);
                }
                else if (questionNumber == 3)
                {
                    string answer3 = _concept;
                    return responses.Count(one => one.Concept == answer3 && one.Concept != null);
                }
                else return 0;

            }
            public void Print()
            {
                Console.WriteLine(_animal + " ");
                Console.WriteLine(_characterTrait + " ");
                Console.WriteLine(_concept + " ");
            }


        }
        public struct Research
        {
            private string _name;
            private Response[] _responses;
            public string Name => _name;
            public Response[] Responses
            {
                get
                {
                    if (_responses == null) return null;
                    else return _responses;
                }

            }
            public Research(string name)
            {
                _name = name;
                _responses = new Response[0];

            }
            public void Add(string[] answers)
            {
                if (answers == null || answers.Length != 3 || _responses == null) return;
                var Abb = new Response(answers[0], answers[1], answers[2]);
                Array.Resize(ref _responses, _responses.Length + 1);
                _responses[_responses.Length - 1] = Abb;
            }
            public string[] GetTopResponses(int question)
            {
                if (_responses == null || question > 3 || question < 1) return null;
                string[] kan = new string[_responses.Length];

                for (int i = 0, k = 0; i < _responses.Length; i++)
                {
                    string a1 = Geta1(_responses[i], question);
                    if (a1 == "" || a1 == null) Array.Resize(ref kan, kan.Length - 1);
                    else
                    {

                        kan[k] = a1;
                        k++;

                    }

                }

                string[] kan1 = new string[kan.Length];
                kan1[0] = kan[0];

                for (int i = 1, k = 1; i < kan.Length; i++)
                {

                    for (int j = 0; j < k; j++)
                    {

                        if (kan[i] == kan1[j])
                        {
                            Array.Resize(ref kan1, kan1.Length - 1);
                            j = k;
                        }
                        else if (j == k - 1)
                        {
                            kan1[k] = kan[i];
                            k++;
                            break;
                        }

                    }
                }


                int[] mas = new int[kan1.Length];
                for (int i = 0; i < kan1.Length; i++)
                {
                    mas[i] = kan.Count(x => x == kan1[i]);
                }

                for (int i = 1; i < mas.Length;)
                {
                    if (i == 0 || mas[i] <= mas[i - 1])
                    {
                        i++;
                    }
                    else
                    {
                        int temp1 = mas[i];
                        mas[i] = mas[i - 1];
                        mas[i - 1] = temp1;
                        string temp = kan1[i];
                        kan1[i] = kan1[i - 1];
                        kan1[i - 1] = temp;
                        i--;
                    }
                }
                Array.Resize(ref kan1, 5);
                Array.Resize(ref mas, 5);
                Console.WriteLine(112);
                foreach (string x in kan1) Console.WriteLine(x);
                foreach (int x in mas) Console.WriteLine(x);



                return kan1;
            }

            private string Geta1(Response a, int question)
            {
                string a1;
                switch (question)
                {
                    case 1:
                        a1 = a.Animal;
                        return a1;
                    case 2:
                        a1 = a.CharacterTrait;
                        return a1;
                    case 3:
                        a1 = a.Concept;
                        return a1;
                    default:
                        return null;
                }
            }
            public void Print()
            {
                for (int i = 1; i < 4; i++)
                {
                    string[] answer = GetTopResponses(i);
                }

            }
        }
        public class Report
        {
            private Research[] _research;
            private static int _number;
            public Research[] Researches => _research;
            static Report()
            {
                _number = 1;
            }
            public Report()
            {
                _research = new Research[0];
            }
            public Research MakeResearch()
            {
                
                string name = $"No_{_number}_{DateTime.Now.Month:d2}/{DateTime.Now.Year:d2}";
                Research research = new Research(name);
                if (_research == null) return research;
                Array.Resize(ref _research, _research.Length + 1);
                _research[_research.Length - 1] = research;
                _number++;
                return research;
            }
            public (string, double)[] GetGeneralReport(int question)
            {
                if (question < 1 || question > 3 || _research == null) return null;
                string [] correct = new string[0];
                int k = 0;
                for(int i=0;i < _research.Length;i++)
                {
                    for(int j = 0; j < _research[i].Responses.Length; j++)
                    {
                        string a1 = Geta1(_research[i].Responses[j],question);
                        if (a1 != "" && a1 != null)                         
                        {
                            correct[k] = a1;
                            k++;
                        }
                    }
                }

                string[] one =new string[correct.Length];
                one[0] = correct[0];

                for (int i = 1, n = 1; i < correct.Length; i++)
                {

                    for (int j = 0; j < k; j++)
                    {

                        if (correct[i] == one[j])
                        {
                            Array.Resize(ref one, one.Length - 1);
                            j = n;
                        }
                        else if (j == n - 1)
                        {
                            one[n] = correct[i];
                            n++;
                            break;
                        }

                    }
                }
                int[] mas = new int[one.Length];
                for (int i = 0; i < one.Length; i++)
                {
                    mas[i] = correct.Count(x => x == one[i]);
                }
                int sum = correct.Length;
                (string, double)[] answer = new (string, double)[one.Length];
                for(int i=0;i < answer.Length; i++)
                {
                    answer[i] = (one[i], (mas[i]/sum)*100);
                }
                return answer;
            }
            private string Geta1(Response a, int question)
            {
                string a1;
                switch (question)
                {
                    case 1:
                        a1 = a.Animal;
                        return a1;
                    case 2:
                        a1 = a.CharacterTrait;
                        return a1;
                    case 3:
                        a1 = a.Concept;
                        return a1;
                    default:
                        return null;
                }
            }

        }

    }
}
