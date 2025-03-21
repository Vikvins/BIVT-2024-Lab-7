using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_7;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;
            private bool _setted_place;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
                _setted_place = false;
            }

            public void SetPlace(int place)
            {
                if (!_setted_place)
                {
                    _place = place;
                    _setted_place = true;
                }
                else
                {
                    Console.WriteLine($"Место уже установлено");
                }
            }

            public void Print()
            {
                Console.WriteLine($"{_name} {_surname} {_place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _added_sportsmen;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    Sportsman[] result = new Sportsman[_added_sportsmen];
                    for (int i = 0; i < _added_sportsmen; i++)
                    {
                        result[i] = _sportsmen[i];
                    }
                    return result;
                }
            }

            public int SummaryScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _added_sportsmen; i++)
                    {
                        int points = 5 - _sportsmen[i].Place + 1;
                        if (points > 0 && _sportsmen[i].Place != 0)
                        {
                            sum += points;
                        }
                    }
                    return sum;
                }
            }

            public int TopPlace
            {
                get
                {
                    int bestPlace = int.MaxValue;
                    for (int i = 0; i < _added_sportsmen; i++)
                    {
                        if (_sportsmen[i].Place != 0 && _sportsmen[i].Place < bestPlace)
                        {
                            bestPlace = _sportsmen[i].Place;
                        }
                    }
                    return bestPlace;
                }
            }

            protected Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _added_sportsmen = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_added_sportsmen < 6)
                {
                    _sportsmen[_added_sportsmen++] = sportsman;
                }
                else
                {
                    Console.WriteLine("Команда уже заполнена");
                }
            }

            public void Add(Sportsman[] newSportsmen)
            {
                foreach (var sportsman in newSportsmen)
                {
                    if (_added_sportsmen < 6)
                    {
                        _sportsmen[_added_sportsmen++] = sportsman;
                    }
                    else
                    {
                        Console.WriteLine("Команда уже заполнена");
                        break;
                    }
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0)
                {
                    return;
                }

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore)
                        {
                            if (teams[j].TopPlace > teams[j + 1].TopPlace)
                            {
                                Team temp = teams[j];
                                teams[j] = teams[j + 1];
                                teams[j + 1] = temp;
                            }
                        }
                    }
                }

                Console.WriteLine("Команды отсортированы");
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team champion = teams[0];
                double maxStrength = champion.GetTeamStrength();

                for (int i = 1; i < teams.Length; i++)
                {
                    double strength = teams[i].GetTeamStrength();
                    if (strength > maxStrength)
                    {
                        champion = teams[i];
                        maxStrength = strength;
                    }
                }

                Console.WriteLine($"Победитель: {champion.Name}");
                return champion;
            }

            public void Print()
            {
                Console.WriteLine($"{_name}: Лучшее место - {TopPlace}, Суммарный счёт - {SummaryScore}");
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double totalPlaces = 0;
                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    totalPlaces += Sportsmen[i].Place;
                }
                double averagePlace = totalPlaces / Sportsmen.Length;
                return 100 / averagePlace;
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double sumOfPlaces = 0;
                double productOfPlaces = 1;
                int numSportsmen = Sportsmen.Length;

                for (int i = 0; i < Sportsmen.Length; i++)
                {
                    sumOfPlaces += Sportsmen[i].Place;
                    productOfPlaces *= Sportsmen[i].Place;
                }

                return 100 * sumOfPlaces * numSportsmen / productOfPlaces;
            }
        }
    }
}