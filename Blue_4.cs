using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_7;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;

            public string Name => _name;

            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    return _scores.ToArray();
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_scores == null)
                    {
                        return 0;
                    }

                    return _scores.Sum();
                }
            }

            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }

            public void Print()
            {
                Console.WriteLine($"{Name}: {TotalScore}");
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }

        public class Group
        {
            private string _name;
            private Team[] _manTeams;
            private Team[] _womanTeams;
            private int _manIndex;
            private int _womanIndex;

            public string Name => _name;
            public Team[] ManTeams => _manTeams;
            public Team[] WomanTeams => _womanTeams;

            public Group(string name)
            {
                _name = name;
                _manTeams = new Team[12];
                _womanTeams = new Team[12];
                _manIndex = 0;
                _womanIndex = 0;
            }

            public void Add(Team team)
            {
                if (team is ManTeam && _manIndex < _manTeams.Length)
                {
                    _manTeams[_manIndex++] = team;
                }
                else if (team is WomanTeam && _womanIndex < _womanTeams.Length)
                {
                    _womanTeams[_womanIndex++] = team;
                }
            }

            public void Add(Team[] teams)
            {
                foreach (var team in teams)
                {
                    Add(team);
                }
            }

            public void Sort()
            {
                Array.Sort(_manTeams, CompareTeams);
                Array.Sort(_womanTeams, CompareTeams);
            }

            private int CompareTeams(Team team1, Team team2)
            {
                if (team1 == null) return 1;
                if (team2 == null) return -1;
                return team2.TotalScore.CompareTo(team1.TotalScore);
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                if (group1._manTeams.Length == 0 || group1._womanTeams.Length == 0 ||
                    group2._manTeams.Length == 0 || group2._womanTeams.Length == 0)
                {
                    return null;
                }

                else if (group1._manTeams == null || group1._womanTeams == null ||
                        group2._manTeams == null || group2._womanTeams == null)
                {
                    return null;
                }

                Group result = new Group("Финалисты");
                result.MergeTeams(result._manTeams, group1._manTeams, group2._manTeams, size / 2);
                result.MergeTeams(result._womanTeams, group1._womanTeams, group2._womanTeams, size / 2);
                return result;
            }
            private void MergeTeams(Team[] result, Team[] teams1, Team[] teams2, int size)
            {
                Team[] allTeams = new Team[teams1.Length + teams2.Length];
                int index = 0;
                for (int i = 0; i < teams1.Length; i++)
                {
                    if (teams1[i] != null)
                    {
                        allTeams[index++] = teams1[i];
                    }
                }
                for (int i = 0; i < teams2.Length; i++)
                {
                    if (teams2[i] != null)
                    {
                        allTeams[index++] = teams2[i];
                    }
                }

                Array.Sort(allTeams, (team1, team2) => team2?.TotalScore.CompareTo(team1?.TotalScore) ?? 0);

                for (int i = 0; i < size && i < allTeams.Length; i++)
                {
                    result[i] = allTeams[i];
                }
            }

            public void Print()
            {
                Console.WriteLine($"Группа: {Name}");
                Console.WriteLine("Мужские команды:");
                foreach (var team in _manTeams)
                {
                    if (team != null)
                    {
                        team.Print();
                    }
                }
                Console.WriteLine("Женские команды:");
                foreach (var team in _womanTeams)
                {
                    if (team != null)
                    {
                        team.Print();
                    }
                }
            }
        }
    }
}
