using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat2TVP
{
    class Artikal
    {
        string naziv;
        int id_artikal;
        double cena;
        double popust;
        int id_grupa;


        int komada;

        public string Naziv { get{ return naziv; } set{ naziv = value; } }
        public int Id_artikla{ get { return id_artikal; } set { id_artikal = value; } }
        public double Cena { get { return cena; } set { cena = value; } }
        public double Popust { get { return popust; } set { popust = value; } }
        public int Id_grupa { get { return id_grupa; } set { id_grupa = value; } }
        public int Komada { get { return komada; } set { komada = value; } }

        public Artikal(string naziv, double cena, double popust) {
            this.naziv = naziv;
            this.cena = cena;
            this.popust = popust;
        }

        public Artikal() { }
        public Artikal(string naziv, double cena, double popust,int komada)
        {
            this.naziv = naziv;
            this.cena = cena;
            this.popust = popust;
            this.komada = komada;
        }

        public Artikal(string naziv, int id_artikla, double cena, double popust, int id_grupa)
        {
            this.naziv = naziv;
            this.id_artikal = id_artikla;
            this.cena = cena;
            this.popust = popust;
            this.id_grupa = id_grupa;
        }

        public override string ToString()
        {
            return naziv + cena + popust;
        }

        public  string ZaListBoxTrenutni()
        {
            return " Naziv  " + naziv + "      Cena: "+cena + "   Popust: " +popust + " % ";
        }

        public string ZaListBoxRacun()
        {
            return " Naziv  " + naziv + "      Cena: " + cena + "   Popust: " + popust + " % " + "- Komada " + komada;
        }

       

    }
}
