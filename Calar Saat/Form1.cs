using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;
using System.Windows.Forms;


namespace Calar_Saat
{



    public partial class Form1 : Form
    {
        int sure = 60; // Geri sayım için başlangıç süresi (60 dakika)
        public Form1()
        {
            InitializeComponent();
            OtoBaslat();
        }
        private void OtoBaslat()
        {
            string baslangicKlasoru = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string kisaYolAdi = "Calar_Saat.lnk";
            string kisaYolYolu = Path.Combine(baslangicKlasoru, kisaYolAdi);

            if (!System.IO.File.Exists(kisaYolYolu))
            {
                string uygulamaYolu = Assembly.GetExecutingAssembly().Location;
                WshShell shell = new WshShell();
                IWshShortcut kisaYol = (IWshShortcut)shell.CreateShortcut(kisaYolYolu);
                kisaYol.TargetPath = uygulamaYolu;
                kisaYol.Save();
            }
        }

    


        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start(); // Timer başlatılır
            label1.Text = "Saymaya başladı!";

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure--; // Zamanı azalt

            if (sure >= 0)
            {
                label1.Text = sure.ToString() + " dk"; // Kalan süreyi göster
            }
            else
            {
                timer1.Stop(); // Zaman dolunca sayaç durdurulur
                DialogResult result = MessageBox.Show("Zaman doldu! Kalk! Yeniden başlatmak ister misiniz?", "Kalk!", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    sure = 60; // Süreyi tekrar 60 dakika olarak ayarla
                    label1.Text = sure.ToString() + " dk"; // Etiketi güncelle
                    timer1.Start(); // Timer'ı tekrar başlat
                }
                else
                {
                    // Kullanıcı "İptal" düğmesine bastı, geri sayımı sıfırla
                    sure = 60;
                    label1.Text = sure.ToString() + " dk";
                }
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {


        }
    }
}
