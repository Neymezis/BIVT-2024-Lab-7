using System;
using System.Linq;

namespace Lab_7
{
    public class Purple_3
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private double[] _marks;
            private int[] _places;
            private int _nomber;

            public string Name => _name;
            public string Surname => _surname;

            public double[] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    double[] marks = new double[_marks.Length];
                    Array.Copy(_marks, marks, _marks.Length);
                    return marks;
                }
            }
            public int[] Places
            {
                get
                {
                    if (_places == null) return null;
                    int[] places = new int[_places.Length];
                    Array.Copy(_places, places, _places.Length);
                    return places;
                }
            }
            public int Score
            {
                get
                {
                    if (_places == null || _places.Length == 0) return 0;
                    int score = 0;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        score += _places[i];
                    }
                    return score;
                }
            }
            private int Max
            {
                get
                {
                    if (_places == null || _places.Length == 0) return 0;
                    int Max = int.MaxValue;
                    for (int i = 0; i < _places.Length; i++)
                    {
                        if (_places[i] < Max) Max = _places[i];
                    }
                    return Max;
                }
            }
            private double TotalMark
            {
                get
                {
                    if (_marks == null || _marks.Length != 7) return 0;
                    double Totalmark = 0;
                    for (int i = 0; i < _marks.Length; i++)
                    {
                        Totalmark += _marks[i];

                    }

                    return Totalmark;
                }
            }


            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new double[7];
                _places = new int[7];
                _nomber = 0;

            }
            public void Evaluate(double result)
            {

                if (_nomber >= _marks.Length) return;
                if (_marks == null || result < 0 || result > 6) return;
                _marks[_nomber] = result;
                _nomber++;
            }
            public static void SetPlaces(Participant[] participants)
            {
                if (participants == null) return;
                Participant[] participants1 = new Participant[participants.Length];
                int k = 0;
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i]._places == null || participants[i]._marks == null)
                    {
                        Array.Resize(ref participants1, participants1.Length - 1);
                    }
                    else
                    {
                        participants1[k] = participants[i];
                        k++;
                    }
                }
                for (int j = 0; j < 7; j++)
                {


                    for (int i = 1; i < participants1.Length;)
                    {
                        if (i == 0 || participants1[i]._marks[j] <= participants1[i - 1]._marks[j])
                        {
                            i++;
                        }
                        else
                        {

                            Participant temp1 = participants1[i];
                            participants1[i] = participants1[i - 1];
                            participants1[i - 1] = temp1;
                            i--;
                        }
                    }
                    for (int i = 0; i < participants1.Length; i++)
                    {
                        participants1[i]._places[j] = 1 + i;
                    }
                }
                Participant[] participants2 = new Participant[participants.Length];


                int m = participants1.Length;
                for (int i = 0; i < participants1.Length; i++)
                {
                    participants2[i] = participants1[i];

                }
                for (int i = 0; i < participants.Length; i++)
                {
                    if (participants[i]._places == null || participants[i]._marks == null)
                    {
                        participants2[m] = participants[i];
                        m++;
                    }
                }
                Array.Copy(participants2, participants, participants2.Length);
            }
            public static void Sort(Participant[] array)
            {
                if (array == null ) return;

                Participant[] array1 = new Participant[array.Length];
                int k = 0;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i]._places == null || array[i]._marks == null)
                    {
                        Array.Resize(ref array1, array1.Length - 1);
                    }
                    else
                    {
                        array1[k] = array[i];
                        k++;
                    }
                }

                for (int i = 1; i < array1.Length;)
                {

                    if (i == 0 || array1[i].Score > array1[i - 1].Score)
                    {
                        i++;

                    }
                    else if (array1[i].Score == array1[i - 1].Score)
                    {
                        int _1 = array1[i].Max;

                        int _2 = array1[i - 1].Max;
                        if (_1 > _2)
                        {
                            i++;
                        }
                        else if (_2 > _1)
                        {
                            Participant temp1 = array1[i];
                            array1[i] = array1[i - 1];
                            array1[i - 1] = temp1;
                            i--;
                        }
                        else
                        {
                            if (array1[i].TotalMark < array1[i - 1].TotalMark)
                            {
                                i++;
                            }
                            else
                            {
                                Participant temp1 = array1[i];
                                array1[i] = array1[i - 1];
                                array1[i - 1] = temp1;
                                i--;
                            }
                        }
                    }

                    else
                    {
                        Participant temp1 = array1[i];
                        array1[i] = array1[i - 1];
                        array1[i - 1] = temp1;
                        i--;

                    }

                }
                Participant[] array2 = new Participant[array.Length];


                int m = array1.Length;
                for (int i = 0; i < array1.Length; i++)
                {
                    array2[i] = array1[i];

                }
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i]._places == null || array[i]._marks == null)
                    {
                        array2[m] = array[i];
                        m++;
                    }
                }
                Array.Copy(array2, array, array2.Length);
            }
            public void Print()
            {
                Console.Write(_name + " ");
                Console.Write(_surname + " ");
                Console.Write(Score + " ");
                Console.Write(Max + " ");
                Console.WriteLine(TotalMark);
            }
        }
        public abstract class Skating
        {
            protected Participant[] _participants;
            protected double[] _moods;
            public Participant[] Participants => _participants;
            public double[] Moods
            {
                get
                {
                    if (_moods == null) return null;
                    var moods = new double[_moods.Length];
                    Array.Copy(_moods, moods, _moods.Length);
                    return moods;
                }

            }
            protected abstract void ModificateMood();
            public Skating(double[] moods)
            {
                if (moods == null) return;
                _moods = new double[moods.Length];
                Array.Copy(moods, _moods, moods.Length);
                ModificateMood();
            }
            public void Evaluate(double[] marks)
            {
                if (marks == null||_participants==null) return;
                int k = -1;
                for (int i = 0; i < _participants.Length; i++)
                {
                    if (_participants[i].Marks.All(x => x==0) && (_participants[i].Places != null && _participants[i].Places.Length != 0))
                    {
                        k = i;
                        break;
                    }
                }
                if (k == -1) return;
                for (int i = 0; i < _participants[k].Marks.Length; i++)
                {
                    _participants[k].Evaluate(marks[i] * _moods[i]);
                }
            }
            public void Add(Participant participant)
            {
                if (Participants==null) return;
                if (_participants == null) _participants = new Participant[0];
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
              

            }
            public void Add(Participant[] participant)
            {
                if (participant == null || participant.Length == 0) return;
                if (_participants == null) _participants = new Participant[0];
                int n = _participants.Length;
                Array.Resize(ref _participants, _participants.Length + participant.Length);
                for (int i = 0; i < participant.Length; i++)
                {
                    _participants[i + n] = participant[i];
                }
            }
        }
        public class FigureSkating : Skating
        {
            public FigureSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                for (int i = 0; i < _moods.Length; i++)
                {
                    _moods[i] += (i + 1.0) / 10;
                }
            }
        }
        public class IceSkating: Skating
        {
            public IceSkating(double[] moods) : base(moods) { }
            protected override void ModificateMood()
            {
                for(int i=0; i< _moods.Length; i++)
                {
                    _moods[i] *= (1 + (1.0 + i) / 100);
                }
            }
        }
    }
}
