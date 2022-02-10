using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//dosya yüklemek için sınıfımız
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//namespace ekledik kullanacağımız yardımcı classlar için
using tahsinsoyakhali.Class;
using static tahsinsoyakhali.Class.Cargo;
using static tahsinsoyakhali.Class.YardimciKodlar;

namespace tahsinsoyakhali
{
    public partial class Form1 : Form
    {
        //code first için nesne
        HaliDbContext dBContext = new HaliDbContext();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtID.Enabled = false;
            cbIndirim.Enabled = false;
            ComboBoxDoldurma(); //comboboxları doldurduk
            ShowHali(); //verileri doldurduk
            ButonRenkleri(); //gerekli design işlerimizi yaptık

        }
        #region Database oluşturma
        public void databasecreate()
        {
            //bir kez çağırarak cod first ile database oluşturduk
            using (HaliDbContext context = new HaliDbContext())
            {
                context.Database.Create();
                MessageBox.Show("Veri Tabanı Oluşturuldu.");
            }
        }
        #endregion

        #region HALI Göster,Ekle,Sil,Ara,Güncelle fonksiyonları
        public void HaliClearTextboxs()
        {
            txtAd.Clear();
            txtAra.Clear(); 
            txtBoy.Clear(); 
            txtDesen.Clear(); 
            txtEn.Clear();
            txtFiyat.Clear();
            txtID.Clear();
            txtRenk.Clear();
            txtOzellik.Clear();
            txtMarka.Clear();
            cbSatici.Text = "";
        }
        public void ShowHali()
        {
            //datagridviewi veritabanımızdaki tablomuza bağlıyoruz.
            dataGridView1.DataSource = dBContext.Halis.ToList();

        }
        public void AddHali()
        {
            var tbl = new Hali();
            //tablomuza aktarilacak verilerimizi seçiyoruz textboxlardan
            tbl.Adi = txtAd.Text;
            tbl.Renk = txtRenk.Text;
            tbl.Marka = txtMarka.Text;
            tbl.Desen = txtDesen.Text;
            tbl.Ozellik = txtOzellik.Text;
            //m2sini alıp sayıyı yuvarlayıp ona göre , den sonra sayısı ayarladık.
            double olcu = (Convert.ToDouble(txtEn.Text) / 100) * (Convert.ToDouble(txtBoy.Text) / 100);
            olcu = Math.Round(olcu, 2);
            tbl.Olcu = olcu;
            tbl.Fiyat = Convert.ToDouble(txtFiyat.Text);
            string a = cbSatici.SelectedItem.ToString();
            tbl.Satici = a;
            //tablodaki kayıtları veri tabanına ekliyecek
            dBContext.Halis.Add(tbl);
            //işlemi kaydediyoruz
            dBContext.SaveChanges();
            MessageBox.Show("Kayıt Başarıyla Eklendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //eklendikten sonra temizleme ve verileri gösteriyoruz
            HaliClearTextboxs();
            ShowHali();
        }
        public void UpdateHali()
        {
            int ID = int.Parse(txtID.Text);
            //lamda exppression kullanarak  sadeleştirilmiş anonim metod fonksiyon kullandık (basit işlemler kullanır)
            var tbl = dBContext.Halis.FirstOrDefault(x => x.HaliID == ID);
            tbl.Adi = txtAd.Text;
            tbl.Renk = txtRenk.Text;
            tbl.Marka = txtMarka.Text;
            tbl.Desen = txtDesen.Text;
            tbl.Ozellik = txtOzellik.Text;
            double olcu = (Convert.ToDouble(txtEn.Text) / 100) * (Convert.ToDouble(txtBoy.Text) / 100);
            olcu = Math.Round(olcu, 2);
            tbl.Olcu = olcu;
            tbl.Fiyat = Convert.ToDouble(txtFiyat.Text);
            string a = cbSatici.SelectedItem.ToString();
            tbl.Satici = a;
            dBContext.SaveChanges();
            MessageBox.Show("Kayıt Başarıyla Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            HaliClearTextboxs();
            ShowHali();
        }
        public void DeleteHali()
        {
            int ID = int.Parse(txtID.Text);
            //lamda exppression kullanarak silme yaptık
            var tbl = dBContext.Halis.FirstOrDefault(x => x.HaliID == ID);
            dBContext.Halis.Remove(tbl);
            dBContext.SaveChanges();
            MessageBox.Show("Kayıt Başarıyla Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            HaliClearTextboxs();
            ShowHali();
        }
        public void SearchHali()
        {
            txtID.Enabled = true;
            var ara = from x in dBContext.Halis select x;
            if (txtAra.Text != null)
            {
                //lamda exppression kullanarak arama yaptık
                dataGridView1.DataSource = ara.Where(x => x.Adi.Contains(txtAra.Text)).ToList();
            }
        }
        #endregion

        #region Hali DataGridView
        //datagridview tıkladığımızda içindeki itemler aktarılsın onu ayarladık
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtID.Enabled = true;
            txtID.Text = dataGridView1.CurrentRow.Cells["HaliID"].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells["Adi"].Value.ToString();
            txtRenk.Text = dataGridView1.CurrentRow.Cells["Renk"].Value.ToString();
            txtMarka.Text = dataGridView1.CurrentRow.Cells["Marka"].Value.ToString();
            txtDesen.Text = dataGridView1.CurrentRow.Cells["Desen"].Value.ToString();
            txtOzellik.Text = dataGridView1.CurrentRow.Cells["Ozellik"].Value.ToString();
            cbSatici.Text = dataGridView1.CurrentRow.Cells["Satici"].Value.ToString();
            double a = Convert.ToDouble(dataGridView1.CurrentRow.Cells["Olcu"].Value.ToString());
            a = a * 100 / 2;
            txtEn.Text = Convert.ToString(a);
            txtBoy.Text = Convert.ToString(a);
            txtFiyat.Text = dataGridView1.CurrentRow.Cells["Fiyat"].Value.ToString();
        }
        #endregion

        #region Buton İçleri
        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                AddHali();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateHali();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteHali();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SearchHali();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Buton,Textbox,Gridview Görselleştirmeleri
        private void ButonRenkleri()
        {
            btnEkle.AutoSize = true;
            btnEkle.BackColor = Color.PaleGreen;
            btnGuncelle.AutoSize = true;
            btnGuncelle.BackColor = Color.PaleGreen;
            btnSil.AutoSize = true;
            btnSil.BackColor = Color.MediumVioletRed;
            btnHaliHesap.AutoSize = true;
            btnHaliHesap.BackColor = Color.PaleGreen;
            btnAlisHesap.AutoSize = true;
            btnAlisHesap.BackColor = Color.MediumVioletRed;
            btnPatron.AutoSize = true;
            btnPatron.BackColor = Color.Khaki;
            btnOHaliHesap.AutoSize = true;
            btnOHaliHesap.BackColor = Color.PaleGreen;
            btnGozat.AutoSize = true;
            btnGozat.BackColor = Color.Khaki;
            btnRGozat.AutoSize = true;
            btnRGozat.BackColor = Color.Khaki;
            btnYukle.AutoSize = true;
            btnYukle.BackColor = Color.PaleGreen;
        }
        #endregion

        #region Gerekli Kodlar(Sınıf Ekleme,Enum,List)
        private void ComboBoxDoldurma()
        {
            cbYikamaRenk.DataSource = Enum.GetValues(typeof(Renk));
            cbYikamaMalzeme.Items.AddRange(Ozellik);
            cbYikamaTur.Items.Add("Standart");
            cbYikamaTur.Items.Add("Turk");
            cbYikamaTur.Items.Add("Alman");
            cbYikamaTur.Items.Add("Fransız");
            cbYikamaTur.Items.Add("Iran");
            dtpTeslimEtme.Enabled = false;
            cbiller.DataSource = Enum.GetValues(typeof(Iller));
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cbOtv.Items.AddRange(otv);
            List<string> kdv2 = listeolustur();            
            cbKdv.Items.AddRange(kdv2.ToArray<string>());
            cbSatici.DataSource = Enum.GetValues(typeof(Satici));
            cbSatici2.DataSource = Enum.GetValues(typeof(Satici));
            cbIndirim.Items.AddRange(indirim);
            cbOHaliOzellik.Items.AddRange(Ozellik);
            cbsatinalinan.DataSource = Enum.GetValues(typeof(Satici));
            cbOHRenk.DataSource = Enum.GetValues(typeof(Renk));
            cbOzelHaliUlke.SelectedItem = "Standart";
            cbOHaliOzellik.SelectedItem = "Polyester";
            cbYikamaTur.SelectedItem = "Standart";
            cbYikamaMalzeme.SelectedItem = "Polyester";
            //özellikleri
            cbYikamaTur.DropDownStyle = ComboBoxStyle.DropDownList;
            cbYikamaRenk.DropDownStyle = ComboBoxStyle.DropDownList;
            cbYikamaMalzeme.DropDownStyle = ComboBoxStyle.DropDownList;
            cbiller.DropDownStyle = ComboBoxStyle.DropDownList;
            cbilceler.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOtv.DropDownStyle = ComboBoxStyle.DropDownList;
            cbKdv.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSatici.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSatici2.DropDownStyle = ComboBoxStyle.DropDownList;
            cbIndirim.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOzelHaliUlke.DropDownStyle = ComboBoxStyle.DropDownList;
            cbsatinalinan.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOHaliOzellik.DropDownStyle = ComboBoxStyle.DropDownList;
            cbOHRenk.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void SatisHesap()
        {
            double a = Convert.ToDouble(cbKdv.SelectedItem.ToString());
            double b = Convert.ToDouble(cbOtv.SelectedItem.ToString());
            double fiyat = Convert.ToDouble(txtHFiyat.Text);
            double kar = Convert.ToDouble(txtKar.Text);
            //static yerine nesne türeterek de kullanılabilirdi
            YardimciKodlar y1 = new YardimciKodlar();
            a = y1.fiyathesapla(a);
            fiyat += fiyat * a;
            //override ederek fonksiyonumuzu hesaplıyalım
            fiyat += y1.fiyathesapla(b,fiyat);
            //fiyat += fiyat * (b / 100);
            fiyat += fiyat * (kar / 100);

            MessageBox.Show("Halı Fiyatımız : " + fiyat, "Fiyat", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void AlisHesap()
        {
            double halisayisi = Convert.ToDouble(txtHaliCount.Text);
            double fiyat = Convert.ToDouble(txtToptanF.Text);
            string satici = cbSatici2.SelectedItem.ToString();
            #region indirim oranları
            if (satici == "Suntheo")
            {
                cbIndirim.SelectedItem = "90";
            }
            else if (satici == "CabbarLimited")
            {
                cbIndirim.SelectedItem = "20";
            }
            else if (satici == "Şahinoğlu")
            {
                cbIndirim.SelectedItem = "10";
            }
            else if (satici == "Derebeyler")
            {
                cbIndirim.SelectedItem = "40";
            }
            else if (satici == "Çaycuma")
            {
                cbIndirim.SelectedItem = "30";
            }
            else if (satici == "Derbederler")
            {
                cbIndirim.SelectedItem = "10";
            }
            else if (satici == "Kunduracı")
            {
                cbIndirim.SelectedItem = "20";
            }
            else if (satici == "Türkaylar")
            {
                cbIndirim.SelectedItem = "70";
            }
            #endregion
            double indirim = Convert.ToDouble(cbIndirim.SelectedItem.ToString());

            fiyat = fiyat * halisayisi;
            YardimciKodlar y2 = new YardimciKodlar();
            //yine override edip hesabı yaptık
            fiyat = y2.fiyathesapla(fiyat, indirim, 100);
            MessageBox.Show("Halı Fiyatımız : " + fiyat, "Fiyat", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Hesaplama Butonları
        private void btnHaliHesap_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtHFiyat.Text=="")
                {
                    MessageBox.Show("Lütfen Fiyat Kısmını Doldurunuz", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (cbKdv.Text == "")
                {
                    MessageBox.Show("Lütfen Kdv Seçiniz", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (cbOtv.Text == "")
                {
                    MessageBox.Show("Lütfen Otv Seçiniz", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtKar.Text == "")
                {
                    MessageBox.Show("Lütfen Kar Oranı Giriniz.(Örnek:15)", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                SatisHesap();
            }
            catch(Exception ex )
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAlisHesap_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtHaliCount.Text == "")
                {
                    MessageBox.Show("Lütfen Halı Sayısı Girin.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtToptanF.Text == "")
                {
                    MessageBox.Show("Lütfen Toptan Fiyat Seçiniz", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                AlisHesap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Yükleme Butonları
        private void btnGozat_Click(object sender, EventArgs e)
        {
            try
            {
                GozatResim();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnYukle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtResAdres.Text == "")
                {
                    MessageBox.Show("Lütfen Resim Seçiniz", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    YukleResim();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnRGozat_Click(object sender, EventArgs e)
        {
            try
            {
                GozatResim2();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Resim Yükleme,Gösterme
        private void GozatResim()
        {
            //başlangıç klasorumuzu arama yaparken masaüstü seçiyoruz
            openFileDialog1.InitialDirectory = @"C:\Users\tahsinsoyak\Desktop";
            //birden fazla seçimi açtık
            openFileDialog1.Multiselect = true;
            //sadece resimler gözüksün diye filtreledik
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                //dosya adresini aktarıyoruz
                txtResAdres.Text = openFileDialog1.FileName;
            }
        }
        private void GozatResim2()
        {
            
            //başlangıç klasorumuzu arama yaparken masaüstü seçiyoruz
            openFileDialog1.InitialDirectory = @"C:\Users\tahsinsoyak\source\repos\tahsinsoyakhali\tahsinsoyakhali\Images\";
            //birden fazla seçimi açtık
            openFileDialog1.Multiselect = false;
            //sadece resimler gözüksün diye filtreledik
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            openFileDialog1.ShowDialog();
            string[] FilenameName;
            foreach (string item in openFileDialog1.FileNames)
            {
                FilenameName = item.Split('\\');
                pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                pictureBox1.ImageLocation = @"C:\Users\tahsinsoyak\source\repos\tahsinsoyakhali\tahsinsoyakhali\Images\" + FilenameName[FilenameName.Length - 1];
            }
        }
        private void YukleResim()
        {
            int count = 0;
            string[] FilenameName;
            foreach (string item in openFileDialog1.FileNames)
            {
                FilenameName = item.Split('\\');
                File.Copy(item, @"C:\Users\tahsinsoyak\source\repos\tahsinsoyakhali\tahsinsoyakhali\Images\" + FilenameName[FilenameName.Length - 1]);
                count++;
                DialogResult result = MessageBox.Show("         " + Convert.ToString(count) + " Resim Yüklendi! " + Environment.NewLine + " Görüntülemek İster misiniz?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    pictureBox1.ImageLocation = @"C:\Users\tahsinsoyak\source\repos\tahsinsoyakhali\tahsinsoyakhali\Images\" + FilenameName[FilenameName.Length - 1];
                }
                else if (result == DialogResult.No)
                {
                    pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                    pictureBox1.ImageLocation = @"C:\Users\tahsinsoyak\source\repos\tahsinsoyakhali\tahsinsoyakhali\Images\standart.jpg";
                }
            }
        }
        #endregion

        #region Ozel Hali İşlemleri
        private void OzelHaliHesapla()
        {
            string sad= txtOHaliAd.Text;
            double sfiyat=Convert.ToDouble(txtOHaliFiyat.Text);
            double solcu=Convert.ToDouble(txtOHaliOlcu.Text);
            #region Halı Türü İf
            if (cbOzelHaliUlke.SelectedItem.ToString() =="Türk")
            {
                Turk t1 = new Turk(txtOHaliAd.Text, Convert.ToDouble(txtOHaliOlcu.Text), Convert.ToDouble(txtOHaliFiyat.Text));
                sad = t1.ad;
                sfiyat = t1.fiyat;
                solcu = t1.olcu;

            }
            else if (cbOzelHaliUlke.SelectedItem.ToString() == "İran")
            {
                Iran i1 = new Iran(txtOHaliAd.Text, Convert.ToDouble(txtOHaliOlcu.Text), Convert.ToDouble(txtOHaliFiyat.Text));
                sad = i1.ad;
                sfiyat = i1.fiyat;
                solcu = i1.olcu;

            }
            else if (cbOzelHaliUlke.SelectedItem.ToString() == "Fransız")
            {
                Fransız f1 = new Fransız(txtOHaliAd.Text, Convert.ToDouble(txtOHaliOlcu.Text), Convert.ToDouble(txtOHaliFiyat.Text));
                sad = f1.ad;
                sfiyat = f1.fiyat;
                solcu = f1.olcu;

            }
            else if (cbOzelHaliUlke.SelectedItem.ToString() == "Alman")
            {
                Alman a1 = new Alman(txtOHaliAd.Text, Convert.ToDouble(txtOHaliOlcu.Text), Convert.ToDouble(txtOHaliFiyat.Text));
                sad = a1.ad;
                sfiyat = a1.fiyat;
                solcu = a1.olcu;

            }
            else if (cbOzelHaliUlke.SelectedItem.ToString() == "Standart")
            {
                OzelHali o1 = new OzelHali(txtOHaliAd.Text, Convert.ToDouble(txtOHaliOlcu.Text), Convert.ToDouble(txtOHaliFiyat.Text));
                sad = o1.ad;
                sfiyat = o1.fiyat;
                solcu = o1.olcu;

            }
            #endregion

            #region Ölçü İfleri
            if(solcu<=30)
            {
                sfiyat += sfiyat * 1.2;

            }
            else if (solcu > 30 && solcu <= 100)
            {
                sfiyat += sfiyat * 1.4;

            }
            else if(solcu > 100 && solcu <= 200)
            {
                sfiyat += sfiyat * 1.8;
            }

            #endregion

            #region Şirket İf
            if (cbsatinalinan.SelectedItem.ToString() == "Suntheo")
                sfiyat +=  sfiyat* 0.75;
            else if (cbsatinalinan.SelectedItem.ToString() == "CabbarLimited")
                sfiyat += sfiyat * 1.56;
            else if (cbsatinalinan.SelectedItem.ToString() == "Şahinoğlu")
                sfiyat += sfiyat * 1.24;
            else if (cbsatinalinan.SelectedItem.ToString() == "Derebeyler")
                sfiyat += sfiyat * 0.8;
            else if (cbsatinalinan.SelectedItem.ToString() == "Çaycuma")
                sfiyat += sfiyat * 1.1;
            else if (cbsatinalinan.SelectedItem.ToString() == "Derbederler")
                sfiyat += sfiyat * 1.3;
            else if (cbsatinalinan.SelectedItem.ToString() == "Kunduracı")
                sfiyat += sfiyat * 1.7;
            else if (cbsatinalinan.SelectedItem.ToString() == "Türkaylar")
                sfiyat += sfiyat * 2.7;
            #endregion

            #region CheckBox İfleri
            if (checkboxHaliDokuma.Checked)
                sfiyat = sfiyat * 2;
            string renk = cbOHRenk.SelectedItem.ToString();
            string kaymaztaban="";
            if (checkKaymazTaban.Checked)
            {
                sfiyat +=125;
                kaymaztaban = ",Kaymaz Taban";
            }
            string yikanma="";
            if (checkYikanma.Checked)
            {
                sfiyat += 40;
                yikanma = ",Yıkanabilir";
            }               
            string antialerjik = "";
            if (checkAlerjik.Checked)
            {
                sfiyat += 70;
                antialerjik = ",AntiAlerjik";
            }                
            string antistatik = "";
            if (checkStatik.Checked)
            {
                sfiyat += 35;
                antistatik = ",AntiStatik";
            }
            string bakteri = "";
            if (checkBakteri.Checked)
            {
                sfiyat += 55;
                antistatik = ",AntiBakteriyel";
            }
            string juttaban = "";
            if (checkJTaban.Checked)
            {
                sfiyat += 30;
                juttaban = ",Jüt Taban";
            }
            string pamukzemin = "";
            if (checkPTaban.Checked)
            {
                sfiyat += 20;
                pamukzemin = ",Pamuk Zemin";
            }
            string ciftaraf = "";
            if (checkCiftTaraf.Checked)
            {
                sfiyat += 100;
                ciftaraf = ",Çift Taraflı";
            }
            #endregion

            MessageBox.Show("Son fiyatımız : " + sfiyat, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            string desen = txtOHaliDesen.Text;
            string malzeme = cbOHaliOzellik.Text;
            string dosya_yolu = @"C:\Users\tahsinsoyak\Desktop\yenihali"+DateTime.Now.ToString("yyyy-MM-dd--HHmmss")+ ".txt";
            //İşlem yapacağımız dosyanın yolunu belirtiyoruz.
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosya varsa açılacağını yoksa oluşturulacağını belirtir,
            //3.parametre dosyaya erişimin veri yazmak için olacağını gösterir.
            StreamWriter sw = new StreamWriter(fs);
            if(checkboxHaliDokuma.Checked)
                sw.WriteLine("Halı Adı : " + sad + " Fiyat : " + sfiyat+" Ölçü : "+solcu+" Desen : "+desen+" El İşlemeli "+" Malzeme : "+malzeme+" Renk : "+renk+" Özellikler"+kaymaztaban+yikanma+antialerjik+antistatik+bakteri+juttaban+pamukzemin+ciftaraf);
            else
                sw.WriteLine("Halı Adı : " + sad + " Fiyat : " + sfiyat + " Ölçü : " + solcu + " Desen : " + desen + " Makine " + " Malzeme : " + malzeme + " Renk : " + renk + " Özellikler" + kaymaztaban + yikanma + antialerjik + antistatik + bakteri + juttaban + pamukzemin + ciftaraf);
            sw.Flush();
            sw.Close();
            fs.Close();
            MessageBox.Show("Yeni Halımızın Dosyası Kaydedildi.(Masaüstünü Kontrol Edin!)","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void checkboxHaliDokuma_CheckedChanged(object sender, EventArgs e)
        {
            //groub box ayarlama
            if (groupBox4.Enabled == false)
                groupBox4.Enabled = true;
            else
                groupBox4.Enabled = false;

            #region halı özellikleri checkboxları
            checkAlerjik.Checked = false;
            checkBakteri.Checked = false;
            checkJTaban.Checked = false;
            checkKaymazTaban.Checked = false;
            checkYikanma.Checked = false;
            checkStatik.Checked = false;
            checkCiftTaraf.Checked = false;
            checkPTaban.Checked = false;
            #endregion
        }

        #endregion

        #region Özel Hali Dokuma Butonları
        private void btnOHaliHesap_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtOHaliAd.Text=="")
                {
                    MessageBox.Show("Lütfen Halı Adı Girin", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtOHaliOlcu.Text == "")
                {
                    MessageBox.Show("Lütfen Ölçü Girin (m2)", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (txtOHaliFiyat.Text == "")
                {
                    MessageBox.Show("Lütfen Fiyat Girin", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
                else if (txtOHaliDesen.Text == "")
                {
                    MessageBox.Show("Lütfen Desen Adı Girin", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }               
                else
                OzelHaliHesapla();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPatron_Click(object sender, EventArgs e)
        {            
            try
            {

                TestBossAttribute.Test(cbsatinalinan.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region  Kargo İşlemleri

        private void cbiller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbiller.Text == "Adıyaman")
            {
                cbilceler.DataSource = Enum.GetValues(typeof(AIlce));
            }
            else if (cbiller.Text == "Malatya")
            {
                cbilceler.DataSource = Enum.GetValues(typeof(MIlce));
            }
            else if (cbiller.Text == "Elazığ")
            {
                cbilceler.DataSource = Enum.GetValues(typeof(EIlce));
            }
        }

        private void KargoOlustur()
        {
            double fiyat;
            string ad;
            string tel;
            string il;
            string ilce;
            string adres;
            string not;
            int en = Convert.ToInt32(mtxtEn.Text);
            int boy = Convert.ToInt32(mtxtBoy.Text);
            int yukseklik = Convert.ToInt32(mtxtYukseklik.Text);
            double agirlik = Convert.ToDouble(mtxtAgirlik.Text);

            Cargo c1 = new Cargo(txtTeslimAd.Text, mtxtTeslimTel.Text, cbiller.Text, cbilceler.Text, rtxtAdres.Text,rtxtNot.Text);
            ad = c1.adsoyad;
            tel = c1.telefon;
            il = c1.il;
            ilce = c1.ilce;
            adres = c1.adres;
            not = c1.not;

            if (checkKargo.Checked)
            {
                fiyat = c1.FiyatHesapla();
            }
            else
            fiyat = c1.FiyatHesapla(en, boy, yukseklik, agirlik);


            fiyat = Math.Round(fiyat, 2);

            MessageBox.Show("Fiyat : " +fiyat, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string dosya_yolu = @"C:\Users\tahsinsoyak\Desktop\yenikargo " + DateTime.Now.ToString("yyyy-MM-dd--HHmmss") + ".txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("Kargo Sahibi Ad-Soyad : "+ad);
            sw.WriteLine("Kargo Sahibi Telefon : " + tel);
            sw.WriteLine("Kargo Adresi : " +adres+"  "+ilce+"/"+il);
            sw.WriteLine("Kargo Notu : " + not);
            sw.WriteLine("Kargo En (cm) : " + en);
            sw.WriteLine("Kargo Boy (cm) : " + boy);
            sw.WriteLine("Kargo Yükseklik (cm) : " + yukseklik);
            sw.WriteLine("Kargo Ağırlık (kg) : " + agirlik);
            sw.WriteLine("Kargo Fiyat : " +fiyat);
            sw.Flush();
            sw.Close();
            fs.Close();
            MessageBox.Show("Kargo Teslim Belgemiz Kaydedildi. (Masaüstünü Kontrol Edin!)", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        #endregion

        #region Kargo İşlemleri Butonları
        private void btnBelgeOlustur_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTeslimAd.Text == "")
                {
                    MessageBox.Show("Lütfen Kargo Sahibi Adı-Soyadı Girin", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (mtxtTeslimTel.Text.Length < 11)
                {
                    MessageBox.Show("Lütfen Kargo Sahibi Telefon Numarası Girin", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (rtxtAdres.Text == "")
                {
                    MessageBox.Show("Lütfen Adres Girin. (Mahalle/Sokak/Numara)", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (mtxtEn.Text == "")
                {
                    MessageBox.Show("Lütfen En Girin (cm)", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (mtxtBoy.Text == "")
                {
                    MessageBox.Show("Lütfen Boy Girin (cm)", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (mtxtYukseklik.Text == "")
                {
                    MessageBox.Show("Lütfen Yükseklik Girin (cm)", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (mtxtAgirlik.Text == "")
                {
                    MessageBox.Show("Lütfen Ağırlık Girin (kg)", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    KargoOlustur();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Halı Yılama Kodlaarı

        private void YikamaFiyatHesapla()
        {
            string ad;
            string tel;
            string adres;

            HaliYikama h1 = new HaliYikama(txtYikamaAd.Text,mtxtYikamaTel.Text,rtxtYikamaAdres.Text);
            ad = h1.adsoyad;
            tel = h1.telefon;
            adres = h1.adres;

            dtpTeslimEtme.Value=h1.TeslimHesapla(Convert.ToInt32(nupHaliSayisi.Value), dtpTeslimAlma.Value);
            double fiyat = h1.FiyatHesapla(Convert.ToInt32(nupHaliSayisi.Value), cbYikamaMalzeme.Text, cbYikamaTur.Text, cbYikamaRenk.Text);

            fiyat = Math.Round(fiyat, 2);

            MessageBox.Show("Yıkama Fiyatımız : " + fiyat);

        }
        private void HaliYikamaOluştur()
        {
            string ad;
            string tel;
            string adres;

            HaliYikama h1 = new HaliYikama(txtYikamaAd.Text, mtxtYikamaTel.Text, rtxtYikamaAdres.Text);
            ad = h1.adsoyad;
            tel = h1.telefon;
            adres = h1.adres;

            dtpTeslimEtme.Value = h1.TeslimHesapla(Convert.ToInt32(nupHaliSayisi.Value), dtpTeslimAlma.Value);
            double fiyat = h1.FiyatHesapla(Convert.ToInt32(nupHaliSayisi.Value), cbYikamaMalzeme.Text, cbYikamaTur.Text, cbYikamaRenk.Text);

            string dosya_yolu = @"C:\Users\tahsinsoyak\source\repos\tahsinsoyakhali\tahsinsoyakhali\WashingRecords\yeniyikama " + DateTime.Now.ToString("yyyy-MM-dd--HHmmss") + ".txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("Halı Sahibi Ad-Soyad : " + ad);
            sw.WriteLine("Halı Sahibi Telefon : " + tel);
            sw.WriteLine("Ev Adresi : " + adres);
            sw.WriteLine("Halı Sayısı : " + nupHaliSayisi.Value);
            sw.WriteLine("Halı Malzemesi : " + cbYikamaMalzeme.Text);
            sw.WriteLine("Halı Türü : " + cbYikamaTur.Text);
            sw.WriteLine("Halı Rengi : " + cbYikamaRenk.Text);
            sw.WriteLine("Teslim Alınacak Tarih : " + dtpTeslimAlma.Value);
            sw.WriteLine("Teslim Edilecek Tarih : " + dtpTeslimEtme.Value);
            sw.WriteLine("Yıkama Fiyatı : " + fiyat);
            sw.Flush();
            sw.Close();
            fs.Close();
            DialogResult result = MessageBox.Show("Yıkama Belgemiz Kaydedildi. "+Environment.NewLine+"Kaydı Görüntülemek İster misiniz ?", "Bilgilendirme", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(@"C:\Users\tahsinsoyak\source\repos\tahsinsoyakhali\tahsinsoyakhali\WashingRecords\");
            }

        }

        #endregion

        #region Halı Yıkama Butonları
        private void dtpTeslimAlma_ValueChanged(object sender, EventArgs e)
        {
            if(nupHaliSayisi.Value <= 5)
            {
                dtpTeslimEtme.Value = dtpTeslimAlma.Value.AddDays(2);
            }
            else if (nupHaliSayisi.Value <= 10 && nupHaliSayisi.Value > 5)
            {
                dtpTeslimEtme.Value = dtpTeslimAlma.Value.AddDays(4);
            }
            else if (nupHaliSayisi.Value <= 20 && nupHaliSayisi.Value > 10)
            {
                dtpTeslimEtme.Value = dtpTeslimAlma.Value.AddDays(6);
            }
            else if (nupHaliSayisi.Value >= 25)
            {
                dtpTeslimEtme.Value = dtpTeslimAlma.Value.AddDays(10);
            }
            //halisayisi ifleri
        }

        private void btnHaliYikama_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtYikamaAd.Text == "")
                {
                    MessageBox.Show("Lütfen Halı Sahibi Adı-Soyadı Girin", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (mtxtYikamaTel.Text.Length < 11)
                {
                    MessageBox.Show("Lütfen Halı Sahibi Telefon Numarası Girin", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (rtxtYikamaAdres.Text == "")
                {
                    MessageBox.Show("Lütfen Adres Girin. (Mahalle/Sokak/Numara)", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (nupHaliSayisi.Value == 0)
                {
                    MessageBox.Show("Lütfen En Az Bir Hal Girin", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    HaliYikamaOluştur();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnYikamaFiyat_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtYikamaAd.Text == "")
                {
                    MessageBox.Show("Lütfen Halı Sahibi Adı-Soyadı Girin", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (mtxtYikamaTel.Text.Length < 11)
                {
                    MessageBox.Show("Lütfen Halı Sahibi Telefon Numarası Girin", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (rtxtYikamaAdres.Text == "")
                {
                    MessageBox.Show("Lütfen Adres Girin. (Mahalle/Sokak/Numara)", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (nupHaliSayisi.Value == 0)
                {
                    MessageBox.Show("Lütfen En Az Bir Hal Girin", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    YikamaFiyatHesapla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata Meydana Geldi : " + ex.Message, "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnYikamaGozat_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"C:\Users\tahsinsoyak\source\repos\tahsinsoyakhali\tahsinsoyakhali\WashingRecords\");
        }

        #endregion

        #region Halı Veri Tabanı Yedekleme 
        private void btnVeriYedek_Click(object sender, EventArgs e)
        {
            string dbname = dBContext.Database.Connection.Database;
            string sqlCommand = @"BACKUP DATABASE [{0}] TO  DISK = N'{1}' WITH NOFORMAT, NOINIT,  NAME = N'MyAir-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            dBContext.Database.ExecuteSqlCommand(System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction, string.Format(sqlCommand, dbname, "yedek "+ DateTime.Now.ToString("yyyy-MM-dd--HHmmss") + ""));
            MessageBox.Show("Veri Tabanı Yedeklendi!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(@"C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\Backup");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnVeriYedek_Click(null,null);
        }
        #endregion
    }


}
