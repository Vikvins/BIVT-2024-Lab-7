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

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }

            public void SetPlace(int place)
            {
                if (_place == 0)
                {
                    _place = place;
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
            private int _count;

            public string Name => _name;

            public Sportsman[] Sportsmen => _sportsmen;

            public int SummaryScore
            {
                get
                {
                    int sum = 0;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i] == null) continue;
                        switch (_sportsmen[i].Place)
                        {
                            case 1: sum += 5; break;
                            case 2: sum += 4; break;
                            case 3: sum += 3; break;
                            case 4: sum += 2; break;
                            case 5: sum += 1; break;
                        }
                    }
                    return sum;
                }
            }

            public int TopPlace
            {
                get
                {
                    int maxPlace = 18;
                    for (int i = 0; i < _sportsmen.Length; i++)
                    {
                        if (_sportsmen[i] != null && _sportsmen[i].Place > 0 && _sportsmen[i].Place < maxPlace)
                        {
                            maxPlace = _sportsmen[i].Place;
                        }
                    }
                    return maxPlace;
                }
            }

            protected Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _count = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_sportsmen == null || sportsman == null || _sportsmen.Length == 0 || _count >= _sportsmen.Length) return;
                _sportsmen[_count++] = sportsman;
            }

            public void Add(Sportsman[] sportsman)
            {
                if (_sportsmen == null || sportsman == null || sportsman.Length == 0 || _sportsmen.Length == 0 || _count >= _sportsmen.Length) return;
                int i = 0;
                while (_count < _sportsmen.Length && i < sportsman.Length)
                {
                    if (sportsman[i] == null)
                    {
                        i++;
                        continue;
                    }
                    _sportsmen[_count++] = sportsman[i++];
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return;
                for (int i = 1; i < teams.Length; i++)
                {
                    for (int j = 0; j < teams.Length - i; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore ||
                            (teams[j].SummaryScore == teams[j + 1].SummaryScore && teams[j].TopPlace > teams[j + 1].TopPlace))
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                    }
                }
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;

                Team champion = null;
                double maxStrength = 0;

                foreach (var team in teams)
                {
                    if (team == null) continue;
                    double strength = team.GetTeamStrength();
                    if (strength > maxStrength)
                    {
                        champion = team;
                        maxStrength = strength;
                    }
                }

                return champion;
            }

            public void Print()
            {
                Console.WriteLine($"{_name}: Лучшее место - {TopPlace}, Общий счёт - {SummaryScore}");
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double places = 0;
                int count = 0;
                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null)
                    {
                        places += sportsman.Place;
                        count++;
                    }
                }
                return count == 0 ? 0 : 100 / (places / count);
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                double sumOfPlaces = 0;
                double productOfPlaces = 1;
                int count = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null)
                    {
                        sumOfPlaces += sportsman.Place;
                        productOfPlaces *= sportsman.Place;
                        count++;
                    }
                }

                return productOfPlaces == 0 ? 0 : 100 * sumOfPlaces * count / productOfPlaces;
            }
        }
    }
}