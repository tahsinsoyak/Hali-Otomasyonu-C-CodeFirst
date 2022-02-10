using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tahsinsoyakhali.Class
{
    public class YardimciKodlar
    {
        //bir tane enum kullanıyoruz satıcıları belirlemek için 
        public enum Satici { Suntheo,CabbarLimited,Şahinoğlu,Derebeyler,Çaycuma,Derbederler,Kunduracı,Türkaylar };

        public enum Renk { Gri,Bej,Mavi,Siyah,Kahverengi,Beyaz,Pembe,Kırmızı,Yeşil,Sarı,Mor,Turuncu,Kahve };

        //kdv için liste oluşturduk
        public static List<string> listeolustur()
        {
            List<string> kdv = new List<string>();
            kdv.Add("8");
            kdv.Add("18");

            return kdv;
        }

        //otv için oluşturduk
        public static string[] otv = new string[3] {"40","80","120"};
        //indirim oranları için oluşturduk
        public static string[] indirim = new string[9] {"10","20","30","40","50","60","70","80","90"};

        public static string[] Ozellik = new string[12] { "Proplen","Polipropilen,Polyester", "Polyester", "Polyester,Kaydırmaz Taban", "Pamuk İplik", "Polipropilen", "Sönil,Akrilik,Pamuk", "Pamuk", "Akrilik", "Sönil", "Suni Deri","Deri" };

        public double fiyathesapla(double kdv)
        {
            kdv = kdv / 100;
            return kdv;
        }
        //override edelim
        public double fiyathesapla(double otv,double fiyat)
        {
            otv = otv / 100;
            fiyat = fiyat* otv;
            return fiyat;
        }
        public double fiyathesapla(double fiyat, double indirim,double _x)
        {
            double x = _x;
            double ind = indirim;
            double fiya = fiyat;
            fiya -= fiya * (ind / x);
            return fiya;
        }

    }
}
