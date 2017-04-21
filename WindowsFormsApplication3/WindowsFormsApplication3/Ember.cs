using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication3
{
    class Ember
    {
        public bool Bejarva = false;
        public Ember parja;
        public List<Ember> gyermekek = new List<Ember>();
        public String Nev;
        public Color Szín = Color.Black;

        public Ember(String Nev, Color Szín)
        {
            this.Nev = Nev;
            this.Szín = Szín;
        }

        public Ember(String Nev)
        {
            this.Nev = Nev;
        }
    }
}
