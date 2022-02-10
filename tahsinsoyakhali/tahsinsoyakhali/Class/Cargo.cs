using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tahsinsoyakhali.Class
{
    public  class Cargo
    {
        public enum Iller { Elazığ, Malatya, Adıyaman};
        public enum EIlce { Merkez,Baskil,Keban};
        public enum MIlce { Merkez,Battalgazi,Yeşilyurt,Doğanyol,Akçadağ,Hekiman};
        public enum AIlce { Merkez,Kahta};

        public string adsoyad;
        public string telefon;
        public string il;
        public string ilce;
        public string adres;
        public string not;
        public double fiyat=0;
        public  Cargo(string ad,string tel,string ilx,string ilcex,string adress,string notx)
        {
            adsoyad = ad;
            telefon = tel;
            il = ilx;
            ilce = ilcex;
            adres = adress;
            not = notx;
        }
        public double FiyatHesapla(double en,double boy,double yukseklik,double kilogram)
        {
            fiyat =((en * boy) / yukseklik) /3;
            fiyat += kilogram * 2;

            return fiyat;
        }
        public double FiyatHesapla()
        {
            return fiyat;
        }

    }
}
