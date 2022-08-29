using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Data.OleDb;

namespace Personel_Takip
{
    public partial class Form1 : Form
    {

        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Personel-Veri.accdb");
        OleDbCommand cmd = new OleDbCommand();

        public static string portismi, banthizi;
        string[] ports = SerialPort.GetPortNames();


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
            comboBox2.Items.Add("300");
            comboBox2.Items.Add("1200");
            comboBox2.Items.Add("2400");
            comboBox2.Items.Add("4800");
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("19200");
            comboBox2.Items.Add("38400");
            comboBox2.Items.Add("57600");
            comboBox2.Items.Add("74880");
            comboBox2.Items.Add("115200");
            comboBox2.Items.Add("230400");
            comboBox2.Items.Add("250000");
            comboBox2.Items.Add("500000");
            comboBox2.Items.Add("1000000");
            comboBox2.Items.Add("2000000");

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 4;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            timer1.Start();
            portismi = comboBox1.Text;
            banthizi = comboBox2.Text;

            try
            {
                serialPort1.PortName = portismi;
                serialPort1.BaudRate = Convert.ToInt16(banthizi);

                serialPort1.Open();
                label1.Text = "Bağlantı Sağlandı";
                label1.ForeColor = Color.Green;
            }
            catch
            {
                serialPort1.Close();
                serialPort1.Open();

                MessageBox.Show("Bağlantı Zaten Açık");
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if(serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                label1.Text = "Bağlantı Kesildi";
                label1.ForeColor = Color.Red;
            }

        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string sonuc;
            sonuc = serialPort1.ReadExisting();

            if (sonuc != "")
            {
                label2.Text = sonuc;
                bag.Open();

                cmd.Connection = bag;
                cmd.CommandText="Select * FROM tablo WHERE Kid='" + sonuc + "'";

                OleDbDataReader reader = cmd.ExecuteReader();   

                if(reader.Read())
                {
                    DateTime bugun = DateTime.Now;
                    pictureBox1.Image = Image.FromFile("foto\\"+reader["resim"].ToString());
                    label8.Text = reader["isim"].ToString();
                    label9.Text = reader["sinif"].ToString() + "/" + reader["sube"].ToString();
                    label10.Text=bugun.ToShortDateString();
                    label11.Text = bugun.ToLongTimeString();
                    bag.Close();

                    bag.Open();
                    cmd.CommandText = "INSERT INTO zaman (isim,tarih,saat)VALUES ('" + label8.Text + "','" + label10.Text + "','" + label11.Text + "')";
                    cmd.ExecuteNonQuery();
                    bag.Close();

                }
                else
                {
                    pictureBox1.Image = Image.FromFile("foto\\images.png" );
                    label2.Text = "Kart Kayıtlı Değil";
                    label8.Text = "_____________";
                    label9.Text = "_____________";
                    label10.Text = "_____________";
                    label11.Text = "_____________";
                }

                bag.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (portismi == null || banthizi == null)
            {
                MessageBox.Show("Bağlantı Kontrol Et");
            }
            else
            {
                timer1.Stop();
                serialPort1.Close();
                label1.Text = "Bağlantı Kapalı";
                label1.ForeColor= Color.Red;
                
                kayit kyt = new kayit();
                kyt.ShowDialog();
            }   
        }


    }
}
