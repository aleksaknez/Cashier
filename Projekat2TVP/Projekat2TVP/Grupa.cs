using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat2TVP
{
    class Grupa
    {
        int id_grupa;
        string nazivGrupe;

        public Grupa(int id_grupa, string nazivGrupe)
        {
            this.id_grupa = id_grupa;
            this.nazivGrupe = nazivGrupe;
        }
        public Grupa() { }

        public int Id_grupa { get { return id_grupa; } set { id_grupa = value; } }
        public string NazivGrupe { get { return nazivGrupe; } set { nazivGrupe = value; } }
    }
}
