using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_7;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            private string _name;
            protected int _votes;

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public string Name => _name;
            public int Votes => _votes;

            public virtual int CountVotes(Response[] responses)
            {
                if (_name == null || responses == null)
                {
                    _votes = 0;
                    return 0;
                }
                int k = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    if (responses[i].Name == _name)
                    {
                        k++;
                    }
                }
                _votes = k;
                return _votes;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{_name} {_votes}");
            }
        }

        public class HumanResponse : Response
        {
            private string _surname;

            public HumanResponse(string name, string surname) : base(name)
            {
                _surname = surname;
            }

            public string Surname => _surname;

            public override int CountVotes(Response[] responses)
            {
                if (_surname == null || responses == null)
                {
                    _votes = 0;
                    return 0;
                }
                int k = 0;
                for (int i = 0; i < responses.Length; i++)
                {
                    HumanResponse hr = responses[i] as HumanResponse;
                    if (hr != null && hr.Name == Name && hr.Surname == _surname)
                    {
                        k++;
                    }
                }
                _votes = k;
                return _votes;
            }

            public override void Print()
            {
                Console.WriteLine($"{Name} {_surname} {_votes}");
            }
        }
    }
}
