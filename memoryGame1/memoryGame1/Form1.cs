using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace memoryGame1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        byte islem = 0; //başa dönmek için
        byte kalan = 8; // kalan eşlenmemiş kart
        int sure=0; //timer ayarları
        PictureBox oncekiResim;
        void resimleriSifirla()
        {
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    (x as PictureBox).Image = Properties.Resources._0;
                }
            }
        }
        void tagDagit() //kartlara tag numarası vermek için fonk
        {
            int[] sayilar = new int[16];
            Random rastgele = new Random();
            byte i = 0;
            while(i<16)
            {
                int rast = rastgele.Next(1, 17);
                if(Array.IndexOf(sayilar, rast)==-1)
                {
                    sayilar[i] = rast;
                    i++;
                }
            }
            for(byte a=0; a<16; a++) //16 kart var ama 8 resim 2 tane gerekiyor eşlemej için o yüzden 8 çıkıyoruz
            {
                if (sayilar[a] > 8) sayilar[a] -= 8;
            }
            byte b = 0;
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    x.Tag = sayilar[b].ToString();
                    b++;
                }
            }
        }
        void taglariSifirla() //yeni oyuna geçince hepsinin 0'lanması için
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    (x as PictureBox).Tag = "0";
                }
            }
        }
        void karsilastir(PictureBox onceki, PictureBox sonraki)
        {
            if(onceki.Tag.ToString()==sonraki.Tag.ToString())
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                onceki.Visible = false;
                sonraki.Visible = false;
                kalan--;
                if (kalan == 0)
                {
                    left.Text = "Tebrikler Kazandınız!";
                }
                else
                {
                    left.Text = "Kalan = " + kalan;  /*left labelin adı*/
                }

                
            }
            else
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                onceki.Image = Image.FromFile("0.png");
                sonraki.Image = Image.FromFile("0.png");
               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            resimleriSifirla();
            taglariSifirla();
            tagDagit();
            lblTime.Text = Convert.ToString(sure);
            timer1.Start();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox simdikiResim = (sender as PictureBox);
            simdikiResim.Image = Image.FromFile((sender as PictureBox).Tag.ToString() + ".png");
            if(islem==0)
            {
                oncekiResim = simdikiResim;
                islem++;
            }
            else
            {
                if(oncekiResim==simdikiResim)
                {
                    MessageBox.Show("Bu kartı zaten seçmiştiniz.");
                    islem = 0;
                    oncekiResim.Image = Image.FromFile("0.png");
                }
                else
                {
                    karsilastir(oncekiResim, simdikiResim);
                    islem = 0;
                }
            }
        }
        void goster()
        {
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    (x as PictureBox).Image = Image.FromFile(x.Tag.ToString() + ".png");
                }
            }
        }
        void gizle()
        {
            foreach (Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    (x as PictureBox).Image = Image.FromFile("0.png");
                }
            }
        }
        private void btnGoster_Click(object sender, EventArgs e)
        {
            goster();
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
            gizle();
            islem = 0;
        }
        void visibleAc()
        {
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox)
                {
                    (x as PictureBox).Visible = true; 
                }
            }
        }

        private void btnYeniOyun_Click(object sender, EventArgs e)
        {
            resimleriSifirla();
            taglariSifirla();
            tagDagit();
            visibleAc();
            kalan = 8;
            islem = 0;
            if (islem == 0)
            {
                timer1.Stop();
                sure = 0;
                timer1.Start();
            }
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure = sure + 1;
            lblTime.Text = Convert.ToString(sure);
            if (kalan == 0)
            {
                timer1.Stop();
            }
        }
    }
}
