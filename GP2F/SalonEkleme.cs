using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GP2F
{
    public partial class SalonEkleme : Form
    {
        string path = "ödevdb.db";
        string cs = @"URI=file:" + Application.StartupPath + "\\ödevdb.db"; //database creat debug folder

        SQLiteConnection con;
        SQLiteCommand cmd;
        SQLiteDataReader dr;
        public SalonEkleme()
        {
            InitializeComponent();
            SQLiteConnection bag = new SQLiteConnection("Data Source=C:\\Users\\serka\\Desktop\\simar\\ödevdb\\ödev.db");
            SQLiteConnection yeni = new SQLiteConnection(bag);
            yeni.Open(); // Bağlantıyı Açtık

            if (yeni.State == ConnectionState.Open)
            {
                MessageBox.Show("Veritabanına Bağlanıldı"); // Bağlantı Açılırsa Uyarı Versin
            }

            else
            {
                MessageBox.Show("Bağlantı Başarısız"); // Başarısız ise uyarı Versin
            }
            yeni.Close(); //Bağlantıyı Sonlandırdık
        }

        private void data_show()
        {
            var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT * FROM test";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                dataGridView1.Rows.Insert(0, dr.GetString(0), dr.GetString(1), dr.GetString(2));
            }
        }
        private void Create_db()
        {
            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source=" + path))
                {
                    sqlite.Open();
                    string sql = "create table test(name varchar(20),id varchar(12))";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                Console.WriteLine("Database cannot create");
                return;
            }
        }


        private void btnEkle_Click(object sender, EventArgs e)
        {
            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);

            try
            {
                cmd.CommandText = "INSERT INTO FilmEkle (SalonId,SalonAd,Kapasite) VALUES(@SalonId,@SalonAd,@Kapasite)";

                string Id = textBox1.Text;
                string Ad = txtSalonAd.Text;
                string Kapasite = txtKapasite.Text;

                cmd.Parameters.AddWithValue("@SalonId", Id);
                cmd.Parameters.AddWithValue("@SalonAd", Ad);
                cmd.Parameters.AddWithValue("@Kapasite", Kapasite);

                dataGridView1.ColumnCount = 3;
                dataGridView1.Columns[0].Name = "Id";
                dataGridView1.Columns[1].Name = "Ad";
                dataGridView1.Columns[2].Name = "Kapasite";
                string[] row = new string[] { Id, Ad, Kapasite };
                dataGridView1.Rows.Add(row);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("veri kaydedilmedi");
                return;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var con = new SQLiteConnection(cs);
            con.Open();

            var cmd = new SQLiteCommand(con);

            try
            {
                cmd.CommandText = "DELETE FROM test where name =@SalonAd";
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@SalonAd", txtSalonAd.Text);

                cmd.ExecuteNonQuery();
                dataGridView1.Rows.Clear();
                data_show();
            }
            catch (Exception)
            {
                Console.WriteLine("veri silinmedi");
                return;
            }
        }

        private void btnyazdir_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Font font = new Font("Arial", 14);
                SolidBrush firca = new SolidBrush(Color.Black);
                Pen kalem = new Pen(Color.Black);
                e.Graphics.DrawString($"Tarih={DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}", font, firca, 50, 25);

                font = new Font("Arial", 20, FontStyle.Bold);
                e.Graphics.DrawString("Salon Ekleme", font, firca, 350, 75);
                e.Graphics.DrawLine(kalem, 50, 70, 780, 70);
                e.Graphics.DrawLine(kalem, 50, 110, 780, 110);
                e.Graphics.DrawLine(kalem, 50, 70, 50, 110);
                e.Graphics.DrawLine(kalem, 780, 70, 780, 110);

                e.Graphics.DrawString("*****************************", font, firca, 280, 115);

                font = new Font("Arial", 15, FontStyle.Bold);
                e.Graphics.DrawString("Salon Id:", font, firca, 60, 150);
                e.Graphics.DrawString("Salon Adı:", font, firca, 60, 200);
                e.Graphics.DrawString("Koltuk Kapasitesi:", font, firca, 60, 250);

                e.Graphics.DrawLine(kalem, 50, 140, 780, 140);
                e.Graphics.DrawLine(kalem, 50, 330, 50, 140);
                e.Graphics.DrawLine(kalem, 50, 330, 780, 330);
                e.Graphics.DrawLine(kalem, 780, 140, 780, 330);

                font = new Font("Arial", 15);
                e.Graphics.DrawString(textBox1.Text, font, firca, 200, 150);
                e.Graphics.DrawString(txtSalonAd.Text, font, firca, 200, 200);
                e.Graphics.DrawString(txtKapasite.Text, font, firca, 250, 250);


            }

            catch (Exception)
            {

            }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void SalonEkleme_Load(object sender, EventArgs e)
        {
            Create_db();
            data_show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["SalonId"].FormattedValue.ToString();
            txtSalonAd.Text = dataGridView1.Rows[e.RowIndex].Cells["SalonAd"].FormattedValue.ToString();
            txtKapasite.Text = dataGridView1.Rows[e.RowIndex].Cells["Kapasite"].FormattedValue.ToString();

        }
    }
}
