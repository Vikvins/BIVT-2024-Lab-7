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
                    int[] copy = new int[_penaltyTimes.Length];
                    Array.Copy(_penaltyTimes, copy, _penaltyTimes.Length);
                    return copy;
                }
            }

            public int Total => _penaltyTimes.Sum();

            public virtual bool IsExpelled => _penaltyTimes.Any(time => time == 10);

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltyTimes = new int[0];
            }

            public virtual void PlayMatch(int time)
            {
                if (time != 0 && time != 2 && time != 5 && time != 10)
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
                Array.Sort(array, (a, b) => a.Total.CompareTo(b.Total));
            }

            public void Print()
            {
                Console.WriteLine($"Игрок: {Name} {Surname}");
                Console.WriteLine("Штрафные минуты:");

                foreach (var time in _penaltyTimes)
                {
                    Console.Write($"{time} ");
                }
                Console.WriteLine();

                Console.WriteLine($"Общее штрафное время: {Total}");
                Console.WriteLine($"Исключен из списка: {IsExpelled}");
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
                totalPlayers++;
            }

            public override bool IsExpelled
            {
                get
                {
                    double averagePenalty = totalPlayers > 0 ? totalPenaltyTime / (double)totalPlayers : 0;
                    return _penaltyTimes.Any(time => time == 10) || Total > averagePenalty * 0.1;
                }
            }

            public override void PlayMatch(int time)
            {
                if (time != 0 && time != 2 && time != 5 && time != 10)
                {
                    return;
                }

                base.PlayMatch(time);
                totalPenaltyTime += time;
            }
        }
    }
}