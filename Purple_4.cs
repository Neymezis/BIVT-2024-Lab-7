using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Purple_4;

namespace Lab_7
{
    public class Purple_4
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private double _time;
            public string Name => _name;
            public string Surname => _surname;
            public double Time => _time;
            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _time = 0;
            }
            public void Run(double time)
            {
                if (time > 0 && _time == 0)
                {
                    _time = time;
                }

            }
            public void Print()
            {
                Console.Write(_name + " ");
                Console.Write(_surname + " ");
                Console.WriteLine(_time);
            }
            public static void Sort(Sportsman[] array)
            {
                if (array == null || array.Length == 0) return;
                Sportsman[] correct = new Sportsman[0];
                foreach (Sportsman x in array)
                {
                    if (x != null)
                    {
                        Array.Resize(ref correct, correct.Length + 1);
                        correct[correct.Length - 1] = x;
                    }
                }

                for (int i = 1; i < correct.Length;)
                {
                    if (i == 0 || correct[i].Time >= correct[i - 1].Time)
                    {
                        i++;
                    }
                    else
                    {
                        Sportsman temp = correct[i];
                        correct[i] = correct[i - 1];
                        correct[i - 1] = temp;
                        i--;
                    }
                }
                foreach (Sportsman x in array)
                {
                    if (x == null)
                    {
                        Array.Resize(ref correct, correct.Length + 1);
                        correct[correct.Length - 1] = x;
                    }
                }
                Array.Copy(correct, array, correct.Length);
            }
        }
        public class Group
        {
            private string _name;
            private Sportsman[] _sportsmen;
            public string Name => _name;
            public Sportsman[] Sportsmen => _sportsmen;
            public Group(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[0];
            }
            public Group(Group group)
            {
                _name = group.Name;
                if (group.Sportsmen == null) _sportsmen = null;
                else
                {
                    _sportsmen = new Sportsman[group.Sportsmen.Length];
                    Array.Copy(group.Sportsmen, _sportsmen, group.Sportsmen.Length);
                }
            }
            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null||sportsman==null) return;
                Array.Resize(ref _sportsmen, _sportsmen.Length + 1);
                _sportsmen[_sportsmen.Length - 1] = sportsman;

            }
            public void Add(Sportsman[] sportsman)
            {
                if (_sportsmen == null || sportsman.Length == 0 || sportsman == null) return;
                for (int i = 0; i < sportsman.Length; i++)
                {
                    if (sportsman[i] != null) 
                    {
                        Array.Resize(ref _sportsmen,_sportsmen.Length + 1);
                        _sportsmen[_sportsmen.Length-1] = sportsman[i];
                    }
                }

            }
            public void Add(Group group)
            {
                Add(group.Sportsmen);
            }
            public void Sort()
            {
                if (_sportsmen == null||_sportsmen.Length==0) return;
                Sportsman[] correct = new Sportsman[0];
                foreach(Sportsman x in _sportsmen)
                {
                    if(x!=null)
                    {
                        Array.Resize(ref correct,correct.Length+1);
                        correct[correct.Length - 1] = x;
                    }
                }
                for (int i = 1; i < correct.Length;)
                {
                    if (i == 0 || correct[i].Time >= correct[i - 1].Time)
                    {
                        i++;
                    }
                    else
                    {
                        Sportsman temp1 = correct[i];
                        correct[i] = correct[i - 1];
                        correct[i - 1] = temp1;
                        i--;
                    }
                }
                foreach (Sportsman x in _sportsmen)
                {
                    if (x == null)
                    {
                        Array.Resize(ref correct, correct.Length + 1);
                        correct[correct.Length - 1] = x;
                    }
                }
                Array.Copy(correct,_sportsmen,correct.Length);
            }
            public static Group Merge(Group group1, Group group2)
            {
                Group Finalists = new Group("Финалисты");
                Finalists._sportsmen = new Sportsman[group1._sportsmen.Length + group2._sportsmen.Length];
                for (int i = 0; i < group1._sportsmen.Length; i++)
                {
                    Finalists._sportsmen[i] = group1._sportsmen[i];
                }
                for (int i = group1._sportsmen.Length; i < group2._sportsmen.Length + group1._sportsmen.Length; i++)
                {
                    Finalists._sportsmen[i] = group2._sportsmen[i - group1._sportsmen.Length];
                }

                Finalists.Sort();
                return Finalists;
            }
            public void Print()
            {
               
            }
            
            public void Split(out Sportsman[] men, out Sportsman[] women)
            {
                men = null;
                women = null;
                if (_sportsmen == null) return;
                men = new Sportsman[0];
                women = new Sportsman[0];
                
                for (int i=0;i<_sportsmen.Length;i++)
                {
                    if (_sportsmen[i] is SkiMan)
                    {
                        Array.Resize(ref men, men.Length + 1);
                        men[men.Length-1]= _sportsmen[i];
                    }
                    if (_sportsmen[i] is SkiWoman)
                    {
                        Array.Resize(ref women, women.Length + 1);
                        women[women.Length - 1] = _sportsmen[i];
                    }
                }
            }
            public void Shuffle()
            {
                if(_sportsmen == null||_sportsmen.Length==0) return;
                Sort();
                Split(out Sportsman[] men, out Sportsman[] women);
                if (men == null || women == null) return;
                Sportsman [] sportsman = new Sportsman[_sportsmen.Length];
                int menIndex = 0;
                int womenIndex = 0;
                if (men.Length == 0)
                {
                    Array.Copy(women, _sportsmen, women.Length);
                    return;
                }
                if (women.Length == 0)
                {
                    Array.Copy(men, _sportsmen, men.Length);
                    return;
                }
                if (men[0].Time <= women[0].Time)
                {
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (menIndex < men.Length && (i % 2 == 0 || womenIndex >= women.Length))
                        {
                            _sportsmen[i] = men[menIndex++];
                        }
                        else if (womenIndex < women.Length)
                        {
                            _sportsmen[i] = women[womenIndex++];
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (womenIndex < women.Length && (i % 2 == 0 || menIndex >= men.Length))
                        {
                            _sportsmen[i] = women[womenIndex++];
                        }
                        else if (menIndex < men.Length)
                        {
                            _sportsmen[i] = men[menIndex++];
                        }
                    }
                }
                
            }
        }
        public class SkiMan : Sportsman
        {
            public SkiMan(string name, string surname) : base(name, surname) { }

            public SkiMan(string name, string surname, double time) : base(name, surname) { Run(time); }
        }
        public class SkiWoman : Sportsman
        {
            public SkiWoman(string name, string surname) : base(name, surname) { }

            public SkiWoman(string name, string surname, double time) : base(name, surname) { Run(time); }
        }
    }
}
