using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Purple_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;
            private int _target;
            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[] marks = new int[_marks.Length];
                    Array.Copy(_marks, marks, _marks.Length);
                    return marks;
                }
            }
            public int Result
            {
                get
                {
                    return rezult();
                }
            }
            private int rezult()
            {
                if (_marks == null||_marks.Length==0) return 0;
                int sum = 0;
                int min = int.MaxValue;
                int max = int.MinValue;
                for (int i = 0; i < _marks.Length; i++)
                {
                    sum += _marks[i];
                    if (min > _marks[i]) min = _marks[i];
                    if (max < _marks[i]) max = _marks[i];
                }
                sum = sum - min - max;
                if (_distance > 120) 
                {
                    if (_distance>=_target) return (sum+60 + 60 + 2 * (_distance - 120));
                    return (sum + 60 + 2 * (_distance - 120));
                }
               
                else
                {
                    if (_distance >= _target)
                    {
                        int n = 60 - 2 * (120 - _distance);
                        if (n <= 0) return sum+60;
                        else return (sum + n+60);
                    }
                    int m = 60 - 2 * (120 - _distance);
                    if (m <= 0) return sum;
                    else return (sum + m);
                }


            }
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[5];
                _distance = 0;
                _target = 0;
            }
            public void Jump(int distance, int[] marks, int target)
            {
                if (marks == null || marks.Length != 5 || distance < 0 || _marks == null||target<0) return;
                _distance = distance;
                _target=target;
                Array.Copy(marks, _marks, marks.Length);

            }
            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;
                for (int i = 1; i < array.Length;)
                {
                    if (i == 0 || array[i].Result <= array[i - 1].Result)
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
                Console.WriteLine(Result);
            }
        }
        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants; 
            public string Name =>_name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;
            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
            }
            public void Add(Participant participant)
            {
                if (_participants == null) _participants = new Participant[0];
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;

            }
            public void Add(Participant[] participant)
            {
                if ( participant==null||participant.Length==0) return;
                if (_participants == null) _participants = new Participant[0];
                int n = _participants.Length;
                Array.Resize(ref _participants, _participants.Length + participant.Length);
                for (int i = 0; i < participant.Length; i++)
                {
                    _participants[i + n] = participant[i];
                }
            }
            public void Jump(int distance, int[] marks)
            {
                if (marks == null || marks.Length == 0||_participants==null||_participants.Length==0) return;
                int k = -1;
                for(int i=0; i < _participants.Length; i++)
                {
                    if (_participants[i].Result==0&& _participants[i].Marks != null)
                    {
                        k = i;
                        break;
                    }
                }
                if (k == -1) return;
                _participants[k].Jump(distance, marks, _standard);
            }
            public void Print()
            {
                Console.Write(_name);
                Console.WriteLine(_standard);
                foreach (Participant x in _participants) x.Print();
            }
        }
        public class JuniorSkiJumping: SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) { }
        }
        public class ProSkiJumping: SkiJumping
        {
            public ProSkiJumping() : base("150m", 150) { }
        }
    }
}
