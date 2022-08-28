using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Takip
{
    public partial class kayit : Form
    {
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






















        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      
    }
}
