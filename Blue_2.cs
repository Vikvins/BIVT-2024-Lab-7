using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_7;

namespace Lab_7
{
    public abstract class WaterJump
    {
        private string _name;
        private int _bank;
        private Blue_2.Participant[] _participants;

        public string Name => _name;
        public int Bank => _bank;
        public Blue_2.Participant[] Participants
        {
            get
            {
                if (_participants == null) { return null; }
                Blue_2.Participant[] copy = new Blue_2.Participant[_participants.Length];
                Array.Copy(_participants, copy, _participants.Length);
                return copy;
            }
        }

        public abstract double[] Prize { get; }

        protected WaterJump(string name, int bank)
        {
            _name = name;
            _bank = bank;
            _participants = new Blue_2.Participant[0];
        }

        public void Add(Blue_2.Participant participant)
        {
            Blue_2.Participant[] newParticipants = new Blue_2.Participant[_participants.Length + 1];
            for (int i = 0; i < _participants.Length; i++)
            {
                newParticipants[i] = _participants[i];
            }
            newParticipants[_participants.Length] = participant;
            _participants = newParticipants;
        }

        public void Add(Blue_2.Participant[] participants)
        {
            Blue_2.Participant[] newParticipants = new Blue_2.Participant[_participants.Length + participants.Length];
            for (int i = 0; i < _participants.Length; i++)
            {
                newParticipants[i] = _participants[i];
            }
            for (int j = 0; j < participants.Length; j++)
            {
                newParticipants[_participants.Length + j] = participants[j];
            }
            _participants = newParticipants;
        }
    }

    public class WaterJump3m : WaterJump
    {
        public WaterJump3m(string name, int bank) : base(name, bank) { }

        public override double[] Prize
        {
            get
            {
                if (Participants == null || Participants.Length < 3) return null;

                Blue_2.Participant.Sort(Participants);

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
                if (Participants == null || Participants.Length < 3) return null;

                Blue_2.Participant.Sort(Participants);

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