using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_1
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            private double[] _coefs;
            private int[,] _marks;
            private int _number;
            private double _sum;
            public string Name => _name;
            public string Surname => _surname;
            public double[] Coefs
            {
                get
                {
                    if (_coefs == null) return null;
                    double[] coefs = new double[_coefs.Length];
                    Array.Copy(_coefs, coefs, _coefs.Length);
                    return coefs;
                }
            }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] marks = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++) marks[i, j] = _marks[i, j];
                    }
                    return marks;
                }
            }
            public double TotalScore
            {
                get
                {
                    return _sum;
                }

            }
            private void Sum()
            {
                if(_coefs==null||_marks==null) return;

                for (int i = 0; i < _marks.GetLength(0); i++)
                {
                    int min = int.MaxValue;
                    int max = int.MinValue;
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        _sum += _marks[i, j] * _coefs[i];
                        if (min > _marks[i, j]) min = _marks[i, j];
                        if (max < _marks[i, j]) max = _marks[i, j];
                    }
                    _sum -= (min + max) * _coefs[i];

                }

            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _coefs = new double[4] { 2.5, 2.5, 2.5, 2.5 };
                _marks = new int[4, 7];
                _sum = 0;
                _number = 0;
            }
            public void SetCriterias(double[] coefs)
            {
                if (coefs.Length!=4||coefs == null || _coefs == null) return; 
                Array.Copy(coefs, _coefs, coefs.Length);
            }
            public void Jump(int[] marks)
            {

                if (_number >= 4 || marks == null || _marks == null || _marks.GetLength(1) != 7||_coefs==null) return;

                
                    for (int i = 0; i < marks.Length; i++)
                    {
                        _marks[_number, i] = marks[i];
                    }

                    _number++;
                    if (_number == 4)
                    {
                        Sum();
                    }
                
               
            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;
                for (int i = 1; i < array.Length;)
                {
                    if (i == 0 || array[i].TotalScore <= array[i - 1].TotalScore)
                    {
                        i++;
                    }
                    else
                    {
                        Participant temp1 = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp1;
                        i--;
                    }
                }

            }

            public void Print()
            {
                Console.Write(_name + " ");
                Console.Write(_surname + " ");
                Console.WriteLine(_sum);
            }
        }
        public class Judge
        {
            private string _name;
            private int[] _marks;
            private int _nomber;
            public string Name => _name;
            public Judge(string name, int[] marks)
            {

                _name = name;
                _nomber = 0;
                if (marks == null) 
                {
                    _marks = null;
                    return;
                }
                
                Array.Copy(marks,_marks,marks.Length);
                
            }
            public int CreateMark()
            {
                if(_marks==null || _marks.Length == 0) return 0;
                if(_nomber>=_marks.Length) _nomber = 0;
                int a = _marks[_nomber];
                _nomber++;
                return a;
            }
            public void Print()
            {
                Console.WriteLine(_name);
                for (int i = 0; i < _marks.Length; i++)
                {
                    Console.Write(_marks[i] + " ");
                }
            }
        }
        public class Competition
        {
            private Judge[] _judges;
            private Participant[] _participants;
            public Judge[] Judges => _judges;
            public Participant[] Participants => _participants;
            public Competition(Judge[] judges)
            {
                _participants = new Participant[0];
                if(judges == null)
                {
                    _judges = null;
                    return;
                }
                Array.Copy(judges, _judges, judges.Length);
                
            }
            public void Evaluate(Participant jumper)
            {
                if (jumper == null || _participants == null||_judges==null|| _judges.Length!=7) return;
                int[] marks1 = new int[7];
                for (int i = 0; i < _judges.Length; i++)
                {
                   
                    marks1[i] = _judges[i].CreateMark();
                }
                jumper.Jump(marks1);
            }
            public void Add(Participant jumper)
            {
                if (jumper == null) return;
                if (_participants == null) _participants= new Participant[0];
                Array.Resize(ref _participants, _participants.Length + 1);
                Evaluate(jumper);
                _participants[_participants.Length - 1] = jumper;

            }
            public void Add(Participant[] jumper)
            {
                if (jumper == null || jumper.Length == 0) return;
                if (_participants == null) _participants = new Participant[0];
                for (int i = 0; i < jumper.Length; i++)
                {
                    Evaluate(jumper[i]);
                }
                int n = _participants.Length;
                Array.Resize(ref _participants, _participants.Length + jumper.Length);
                for (int i = 0; i < jumper.Length; i++)
                {
                    _participants[i + n] = jumper[i];
                }
            }
            public void Sort()
            {
                Participant.Sort(_participants);
            }
        }
    }
}
