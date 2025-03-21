using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_7;

namespace Lab_7
{
    public class Blue_3
    {

        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _penaltyTimes;

            public string Name => _name;
            public string Surname => _surname;

            public int[] Penalties
            {
                get
                {
                    if (_penaltyTimes == null) return null;
                    int[] copy = new int[_penaltyTimes.Length];
                    Array.Copy(_penaltyTimes, copy, _penaltyTimes.Length);
                    return copy;
                }
            }

            public int Total => _penaltyTimes?.Sum() ?? 0;

            public virtual bool IsExpelled => _penaltyTimes != null && _penaltyTimes.Any(time => time == 10);

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0];
            }

            public virtual void PlayMatch(int time)
            {
                if (time < 0 || time > 10 || (time != 0 && time != 2 && time != 5 && time != 10))
                {
                    return;
                }

                int[] newPenaltyTimes = new int[_penaltyTimes.Length + 1];
                _penaltyTimes.CopyTo(newPenaltyTimes, 0);
                newPenaltyTimes[_penaltyTimes.Length] = time;
                _penaltyTimes = newPenaltyTimes;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;

                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"Участник: {Name} {Surname}");
                Console.WriteLine("Штрафные минуты:");

                if (_penaltyTimes != null && _penaltyTimes.Length > 0)
                {
                    foreach (var time in _penaltyTimes)
                    {
                        Console.Write($"{time} ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    return;
                }

                Console.WriteLine($"Общее время: {Total}");

                Console.WriteLine(IsExpelled);

            }
        }

        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
            }

            public override bool IsExpelled
            {
                get
                {
                    int matches = _penaltyTimes.Length;
                    int fouls = _penaltyTimes.Sum();

                    return matches > 0 && (_penaltyTimes.Count(time => time == 5) > matches * 0.1 || fouls > matches * 2);
                }
            }

            public override void PlayMatch(int fouls)
            {
                if (fouls < 0 || fouls > 5)
                {
                    return;
                }

                int[] newPenaltyTimes = new int[_penaltyTimes.Length + 1];
                _penaltyTimes.CopyTo(newPenaltyTimes, 0);
                newPenaltyTimes[_penaltyTimes.Length] = fouls;
                _penaltyTimes = newPenaltyTimes;
            }
        }

        public class HockeyPlayer : Participant
        {
            private static int totalPenaltyTime;
            private static int totalPlayers;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
            }

            public override bool IsExpelled
            {
                get
                {
                    double averagePenalty = totalPenaltyTime / (double)totalPlayers;
                    return _penaltyTimes.Any(time => time == 10) || Total > averagePenalty * 0.1;
                }
            }

            public override void PlayMatch(int time)
            {
                base.PlayMatch(time);
                totalPenaltyTime += time;
                totalPlayers = totalPlayers == 0 ? 1 : totalPlayers;
            }
        }
    }
}