using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat2TVP
{
    class Racun
    {
        private int id_Racuna;
        private double cena;
        private DateTime datum;
        private DateTime vreme;
        


        public Racun()
        {
        }

        public Racun(int id_Racuna, double Cena, DateTime Datum, DateTime Vreme)
        {
            this.id_Racuna = id_Racuna;
            this.cena = Cena;
            this.datum = Datum;
            this.vreme = Vreme;
        }
        public Racun(double Cena, DateTime Datum, DateTime Vreme)
        {
            this.cena = Cena;
            this.datum = Datum;
            this.vreme = Vreme;
        }

       
        public DateTime Vreme{ get { return vreme; } set { vreme = value; } }
        public DateTime Datum { get { return datum; } set { datum = value; } }
        public double Cena { get { return cena; } set { cena = value; } }

        public override string ToString()
        {
            return "Vreme kupovine:  " + vreme.ToShortTimeString() + "      Datum  kupovine:   " + datum.ToShortDateString() + "        Ukupna cena: " + cena+ " din.";
        }

    }
}
