using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tahsinsoyakhali.Class
{
    public class OzelHali
    {
        public double olcu;
        public double fiyat;
        public string ad;
        public OzelHali(string ad2,double olcu2,double fiyat2)
        {
            ad = ad2;
            fiyat = fiyat2;
            olcu = olcu2;
        }

    }
    public class Iran:OzelHali
    {
        public Iran(string ad2, double olcu2,double fiyat2): base(ad2, olcu2,fiyat2)
        {
            fiyat = 2 * fiyat2;
            ad = ad2;
            olcu = olcu2;
        }
    }
    public class Turk:OzelHali
    {
        public Turk(string ad3,double olcu3,double fiyat3):base(ad3,olcu3,fiyat3)
        {
            fiyat = 1.5 * fiyat3;
            ad = ad3;
            olcu = olcu3;
        }
    }
    public class Alman : OzelHali
    {
        public Alman(string ad4, double olcu4, double fiyat4):base(ad4, olcu4, fiyat4)
        {
            fiyat = 2.5 * fiyat4;
            ad = ad4;
            olcu = olcu4;
        }
    }
    public class Fransız : OzelHali
    {
        public Fransız(string ad5, double olcu5, double fiyat5) : base(ad5, olcu5, fiyat5)
        {
            fiyat = 1.25 * fiyat5;
            ad = ad5;
            olcu = olcu5;
        }
    }
    
    #region Interface
    interface Desen
    {
        string desenadi { get; set; }
    }
    public class IOzelIstek:Desen
    {
        public string desenimiz = "Desenimiz : ";
        public string desenadi
        {
            get
            {
               return desenadi;
            }
            set
            {
                desenadi = desenadi +" ";
            }
        }
        public IOzelIstek(string ad)
        {
            desenadi = ad;

        }
        public string desenyaz()
        {
            return desenimiz + desenadi;
        }
    }

    #endregion

    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class Patron : System.Attribute
    {
        public string ad;
        public string adres;
        public string tel;
        public Patron(string ad,string adres,string tel)
        {
            ad = "Tahsin Soyak";
            adres = "Malatya";
            tel = "543223";
        }
    }
    [Patron("Tahsin Soyak", "Malatya", "543223")]
    public struct Suntheo
    {

    }
    [Patron("Hasan Şaş", "Elazığ", "213124")]
    public struct CabbarLimited
    {
        
    }
    [Patron("Pascal Nouma", "Fransa", "21324124")]
    public struct Şahinoğlu
    {

    }
    [Patron("Servet Sahin", "İstanbul", "1231231")]
    public struct Derebeyler
    {

    }
    [Patron("İsmail Kar", "Samsun", "2124515")]
    public struct Çaycuma
    {

    }
    [Patron("İdris Elba", "Diyarbakır", "21312414")]
    public struct Derbederler
    {

    }
    [Patron("Brad Pitt", "Almanya", "12412151")]
    public struct Kunduracı
    {

    }
    [Patron("Bengi Türkay", "Ankara", "41241")]
    public struct Türkaylar
    {

    }
    public class TestBossAttribute
    {
        public static void Test(string a)
        {
            if(a =="Suntheo")
                MessageBox.Show("Patron İsmi : " + " Tahsin Soyak" + " Patron Adres : " + " Malatya "+ "Patron Tel : " + " 543223", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if(a == "CabbarLimited")
                MessageBox.Show("Patron İsmi : " + " Hasan Şaş" + " Patron Adres : " + " Elzığ " + "Patron Tel : " + " 213124", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (a == "Şahinoğlu")
                MessageBox.Show("Patron İsmi : " + " Pascal Nouma" + " Patron Adres : " + " Fransa " + "Patron Tel : " + " 21324124", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (a == "Derebeyler")
                MessageBox.Show("Patron İsmi : " + " Servet Sahin" + " Patron Adres : " + " İstanbul " + "Patron Tel : " + " 1231231", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (a == "Çaycuma")
                MessageBox.Show("Patron İsmi : " + " İsmail Kar" + " Patron Adres : " + " Samsun " + "Patron Tel : " + " 2124515", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (a == "Derbederler")
                MessageBox.Show("Patron İsmi : " + " İdris Elba" + " Patron Adres : " + " Diyarbakır " + "Patron Tel : " + " 21312414", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (a == "Kunduracı")
                MessageBox.Show("Patron İsmi : " + " Brad Pitt" + " Patron Adres : " + " Almanya " + "Patron Tel : " + " 12412151", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (a == "Türkaylar")
                MessageBox.Show("Patron İsmi : " + " Bengi Türkay" + " Patron Adres : " + " Ankara " + "Patron Tel : " + " 41241", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public static void YazdırBilgiyi(System.Type t)
        {
            MessageBox.Show("Company Boss Info : " + t);
            System.Attribute[] attributes = System.Attribute.GetCustomAttributes(t);
            foreach (Attribute att in attributes)
            {
                if (att is Patron)
                {                    
                    Patron b = (Patron)att;
                    MessageBox.Show("Patron İsmi : "+b.ad+" Patron Adres : "+b.adres+"Patron Tel : "+b.tel,"Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
    }
}
