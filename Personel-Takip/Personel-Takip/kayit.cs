using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;  // acces e bağlanmak için

namespace Personel_Takip
{
    public partial class kayit : Form
    {
        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Personel-Veri.accdb");
        OleDbCommand cmd = new OleDbCommand();
        
        public kayit()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort1.ReadExisting();

            if (sonuc != "")
            {
                label6.Text = sonuc;
            }
        }

         private void groupBox1_Enter(object sender, EventArgs e) // kayit ekranı
         {
            serialPort1.PortName = Form1.portismi;
            serialPort1.BaudRate= Convert.ToInt16(Form1.banthizi);

            if(serialPort1.IsOpen == false)
            {
               try
                { 
                    serialPort1.Open();
                    label7.Text = "Bağlantı Sağlandı";
                    label7.ForeColor = Color.Green;
                }
                catch
                {
                label7.Text= "Bağlantı Sağlanamadı";
                }
            }
            else
            {
                label7.Text = "Bağlantı Sağlanamadı";
                label7.ForeColor = Color.Red;
            }
         }

         private void button3_Click(object sender, EventArgs e)
         {
            timer1.Start();
            label6.Text = "__________";
            textBox1.Text = "";
            comboBox1.Text = "Seçiniz";
            comboBox2.Text = "Seçiniz";
            textBox2.Text = "";
            label8.Text = "";
         }
         private void button1_Click(object sender, EventArgs e)
         {
            OpenFileDialog dosya = new OpenFileDialog();
            //dosya.Filter = "Resim Dosyaları (jpg) |'.jpg|Tüm Dosyalar |'.'";
            dosya.Filter = "JPEG (*.png; *.jpg; *jpeg; *jpe)|*.png; *.jpg; *jpeg; *jpe|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\foto";
            dosya.RestoreDirectory = true;

            if(dosya.ShowDialog() == DialogResult.OK)
            {
                string di = dosya.SafeFileName;
                textBox2.Text= di;
            }




         }
        private void button2_Click(object sender, EventArgs e)
        {
            if (label6.Text == "___________" || textBox1.Text == "" || comboBox1.Text == "seçiniz" || comboBox2.Text == "seçiniz" || textBox2.Text == "")
            {
                label8.Text = "Bilgileriniz Eksik";
                label8.ForeColor = Color.Red;
            }
            else
            {
                try
                {
                    bag.Open();
                    cmd.Connection = bag;
                    //cmd.CommandText = "INSERT INTO Tablo (kid,isim,sinif,sube,resim)VALUES ('" + label6.Text + "','" + textBox1.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox2.Text + "')";
                    cmd.CommandText = "INSERT INTO Tablo (Kid,isim,sinif,sube,resim)VALUES " +
                    "('" + label6.Text + "','" + textBox1.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + textBox2.Text + "')";
                    cmd.ExecuteNonQuery();

                    label8.Text = "KAYIT YAPILDI";
                    label8.ForeColor = Color.Green;

                    bag.Close();
                }
                catch
                {
                    bag.Close();
                    MessageBox.Show("Bu kişi zaten kayıtlı");
                }

            }


        }

        private void kayit_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            serialPort1.Close();
        }













        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
