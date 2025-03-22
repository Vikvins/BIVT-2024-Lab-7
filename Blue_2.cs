using System;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks => _marks;

            public int TotalScore
            {
                get
                {
                    int total = 0;
                    foreach (int mark in _marks)
                    {
                        total += mark;
                    }
                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
            }

            public void Jump(int[] result)
            {
                if (_marks[0, 0] == 0)
                {
                    for (int i = 0; i < 5; i++)
                        _marks[0, i] = result[i];
                }
                else if (_marks[1, 0] == 0)
                {
                    for (int i = 0; i < 5; i++)
                        _marks[1, i] = result[i];
                }
            }

            public void Print()
            {
                Console.WriteLine($"Name: {Name}, Surname: {Surname}, Total Score: {TotalScore}");
            }

            public static void Sort(Participant[] participants)
            {
                Array.Sort(participants, (a, b) => b.TotalScore.CompareTo(a.TotalScore));
            }
        }

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => (Participant[])_participants.Clone();

            public abstract double[] Prize { get; }

            protected WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }

            public void Add(params Participant[] participants)
            {
                int oldLength = _participants.Length;
                Array.Resize(ref _participants, oldLength + participants.Length);
                Array.Copy(participants, 0, _participants, oldLength, participants.Length);
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return new double[0];

                    Participant.Sort(Participants);

                    return new double[]
                    {
                        Bank * 0.5,
                        Bank * 0.3,
                        Bank * 0.2
                    };
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants.Length < 3) return new double[0];

                    Participant.Sort(Participants);

                    int topCount = Math.Min(10, Math.Max(3, Participants.Length / 2));
                    double[] prizes = new double[topCount];

                    double nPercent = 20.0 / topCount;

                    for (int i = 0; i < topCount; i++)
                    {
                        prizes[i] = Bank * (nPercent / 100.0);
                    }

                    prizes[0] += Bank * 0.4;
                    prizes[1] += Bank * 0.25;
                    prizes[2] += Bank * 0.15;

                    return prizes;
                }
            }
        }
    }
}