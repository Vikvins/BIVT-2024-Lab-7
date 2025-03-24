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
                    if (_scores == null) return 0;
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
            private ManTeam[] _manTeams;
            private WomanTeam[] _womanTeams;
            private int _countMan;
            private int _countWoman;

            public string Name => _name;
            public ManTeam[] ManTeams => _manTeams;
            public WomanTeam[] WomanTeams => _womanTeams;

            public Group(string name)
            {
                _name = name;
                _manTeams = new ManTeam[12];
                _womanTeams = new WomanTeam[12];
                _countMan = 0;
                _countWoman = 0;
            }

            public void Add(Team team)
            {
                if (team == null) return;

                if (team is ManTeam manTeam && _countMan < _manTeams.Length)
                {
                    _manTeams[_countMan++] = manTeam;
                }
                else if (team is WomanTeam womanTeam && _countWoman < _womanTeams.Length)
                {
                    _womanTeams[_countWoman++] = womanTeam;
                }
            }

            public void Add(Team[] teams)
            {
                if (teams == null) return;
                foreach (var team in teams)
                {
                    Add(team);
                }
            }

            private void TeamSort(Team[] teams, int count)
            {
                if (teams == null || count == 0) return;

                for (int i = 1; i < count; i++)
                {
                    for (int j = i; j > 0 && teams[j - 1].TotalScore < teams[j].TotalScore; j--)
                    {
                        var temp = teams[j];
                        teams[j] = teams[j - 1];
                        teams[j - 1] = temp;
                    }
                }
            }

            public void Sort()
            {
                TeamSort(_manTeams, _countMan);
                TeamSort(_womanTeams, _countWoman);
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                if (group1 == null || group2 == null) return null;

                Group result = new Group("Финалисты");
                Team[] manTeams = MergeTeams(group1._manTeams, group2._manTeams, size);
                Team[] womanTeams = MergeTeams(group1._womanTeams, group2._womanTeams, size);

                result.Add(manTeams);
                result.Add(womanTeams);

                return result;
            }

            private static Team[] MergeTeams(Team[] team1, Team[] team2, int size)
            {
                if (team1 == null || team2 == null) return null;

                Team[] result = new Team[size];
                int n = size / 2;
                int i = 0, j = 0, k = 0;

                while (k < size && i < n && j < n && team1[i] != null && team2[j] != null)
                {
                    if (team1[i].TotalScore >= team2[j].TotalScore)
                    {
                        result[k++] = team1[i++];
                    }
                    else
                    {
                        result[k++] = team2[j++];
                    }
                }

                while (k < size && i < n && team1[i] != null)
                {
                    result[k++] = team1[i++];
                }

                while (k < size && j < n && team2[j] != null)
                {
                    result[k++] = team2[j++];
                }

                return result;
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
