using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rezerwacja
{
    public class RezerwacjaClass
    {
        private long id=-1;
        public long Id
        {
            get { return id;}
            set {SetProperty(ref id,value);}
        }




        internal Rezerwacja(long id, string imie, string nazwisko, int ilosc, string obiekt, string data){
            this.id = id;
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.ilosc = ilosc;
            this.obiekt = obiekt;
            this.data = data;
        }
    }
}
