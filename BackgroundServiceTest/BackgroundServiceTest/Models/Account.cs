using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundServiceTest.Models
{
    public class Account
    {
        private int id;
        private string name;
        private int surplus;

        public Account(int id, string name, int surplus)
        {
            this.id = id;
            this.name = name;
            this.surplus = surplus;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Surplus { get => surplus; set => surplus = value; }
    }
}
