﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_7;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _filledJumps;

            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (_marks == null)
                        return null;

                    int[,] copy = new int[2, 5];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            copy[i, j] = _marks[i, j];
                        }
                    }
                    return copy;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null || _marks.Length == 0) return 0;

                    int total = 0;
                    
                    for (int jump = 0; jump < 2; jump++)
                    {
                        for (int judge = 0; judge < 5; judge++)
                        {
                            total += _marks[jump, judge];
                        }
                    }
                    return total;
                }
            }


            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _filledJumps = 0;
            }

            public void Jump(int[] result)
            {
                if (result == null || _marks == null || result.Length == 0)
                    return;

                if (_filledJumps >= 2)
                    return;

                for (int j = 0; j < 5; j++)
                {
                    _marks[_filledJumps, j] = result[j];
                }
                _filledJumps++;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length <= 1) return;

                
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
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
                Console.WriteLine($"{_name} {_surname} {TotalScore}");
            }
        }

        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;


            public abstract double[] Prize { get; }

            protected WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                if (_participants == null)
                    return;

                Participant[] newParticipants = new Participant[_participants.Length + 1];
                for (int i = 0; i < _participants.Length; i++)
                {
                    newParticipants[i] = _participants[i];
                }
                newParticipants[_participants.Length] = participant;
                _participants = newParticipants;
            }

            public void Add(Participant[] participants)
            {
                if (participants == null || participants.Length == 0)
                    return;

                foreach (Participant participant in participants)
                {
                    Add(participant);
                }
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3)
                        return null;

                    double[] prize = new double[3];
                    prize[0] = 0.5 * Bank;
                    prize[1] = 0.3 * Bank;
                    prize[2] = 0.2 * Bank;
                    return prize;
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
                    if (Participants == null || Participants.Length < 3)
                        return null;

                    int n = Participants.Length / 2;
                    if (n > 10)
                    {
                        n = 10;
                    }
                    else if (n < 3)
                    {
                        return null;
                    }

                    double[] prize = new double[n];
                    double N = (0.2 * Bank) / n;
                     
                    prize[0] = 0.4 * Bank + N;  
                    prize[1] = 0.25 * Bank + N;
                    prize[2] = 0.15 * Bank + N;

                    
                    for (int i = 3; i < n; i++)
                    {
                        prize[i] = N;
                    }

                    return prize;
                }
            }
        }
    }
}