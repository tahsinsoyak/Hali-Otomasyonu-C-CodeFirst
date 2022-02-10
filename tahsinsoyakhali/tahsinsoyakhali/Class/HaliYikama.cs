using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tahsinsoyakhali.Class
{
    public class HaliYikama
    {
        public string adsoyad;
        public string telefon;
        public string adres;
        public int halisayisi;
        public string malzeme;
        public string halitur;
        public string renk;
        public DateTime teslimtarihi;
        
        public HaliYikama(string ad,string tel,string adress)
        {
            adsoyad = ad;
            telefon = tel;
            adres = adress;
        }
        public DateTime TeslimHesapla(int hs,DateTime dt)
        {
            if(hs <= 5)
            {

                teslimtarihi = dt.AddDays(2);
            }
            else if (hs <= 10 && hs > 5)
            {

                teslimtarihi = dt.AddDays(4);
            }
            else if (hs <= 20 && hs > 10)
            {

                teslimtarihi = dt.AddDays(6);
            }
            else if (hs >= 25)
            {

                teslimtarihi = dt.AddDays(10);
            }
            return teslimtarihi;
        }

        public double FiyatHesapla(int hali,string malzem,string haltur,string re)
        {
            halisayisi = hali;
            malzeme = malzem;
            halitur = haltur;
            renk = re;
            double fiyat;
            fiyat = 30;
            fiyat = halisayisi * fiyat;
            if(malzeme == "Polyester")
            {
                fiyat += 10;
            }
            else if(malzeme == "Proplen")
            {
                fiyat += 15;
            }
            else if(malzeme =="Şönil")
            {
                fiyat += 42;
            }
            else if (malzeme == "Pamuk")
            {
                fiyat += 30;
            }
            else if (malzeme == "Deri")
            {
                fiyat += 9;
            }
            else if (malzeme == "Suni Deri")
            {
                fiyat += 23;
            }
            else if (malzeme == "Akrilik")
            {
                fiyat += 12;
            }
            else if (malzeme == "Polipropilen,Polyester")
            {
                fiyat += 30;
            }
            else if (malzeme == "Polyester,Kaydırmaz Taban")
            {
                fiyat += 30;
            }
            else if (malzeme == "Pamuk İplik")
            {
                fiyat += 12;
            }
            else if (malzeme == "Polipropilen")
            {
                fiyat += 16;
            }
            else if (malzeme == "Sönil,Akrilik,Pamuk")
            {
                fiyat += 27;
            }
            if(halitur == "Turk")
            {
                fiyat += 0;
            }
            else if (halitur == "Alman")
            {
                fiyat += 50;
            }
            else if (halitur == "Iran")
            {
                fiyat += 42;
            }
            else if (halitur == "Fransız")
            {
                fiyat += 100;
            }
            return fiyat;


        }
    }
}
