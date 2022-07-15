using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayStationOtomasyon
{
    public partial class Form1 : Form
    {

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-70CN17D;Initial Catalog=PlayStationOtomasyon;Integrated Security=True;Pooling=False");
        public Form1()
        {
            InitializeComponent();
        }
        Button btn;
        RadioButton radio;
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'playStationOtomasyonDataSet.TBLSaatUcreti' table. You can move, or remove it, as needed.
            this.tBLSaatUcretiTableAdapter.Fill(this.playStationOtomasyonDataSet.TBLSaatUcreti);
            radioButtonSuresiz.Checked = true;  
            Yenile();
            comboBosMasalar.Text = "";
            timer1.Start();
           

        }
        public void Yenile()
        {

            Veritabani.SepetListele(dataGridView1);
            Veritabani.ListviewdeKayitlarıGoster(listView1);
            Veritabani.ComboyaBosMasaGetir(comboBosMasalar);
            foreach(Control item in Controls)
            {
               if(item is Button)
                {
                    if (item.Name != btnMasaAC.Name)
                    {
                       Veritabani.baglanti.Open();
                        SqlCommand komut = new SqlCommand("select *from tblmasalar", Veritabani.baglanti);
                        SqlDataReader dr = komut.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["Durumu"].ToString() == "BOŞ" && dr["Masalar"].ToString() == item.Text)
                            {
                                item.BackColor = Color.LightGreen;
                            }
                            if (dr["Durumu"].ToString() == "DOLU" && dr["Masalar"].ToString() == item.Text)
                            {
                                item.BackColor = Color.Red;
                               
                                
                            }
                        }
                        Veritabani.baglanti.Close();
                    }
                }
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

       

       
        private void SecileneGore(object sender, MouseEventArgs e)
        {
            btn=sender as Button;
            if (btn.BackColor == Color.Red)
            {
                süreliMasaİsteğiGöderToolStripMenuItem.Visible = false;
                süresizMasaİsteğiGönderToolStripMenuItem.Visible = false;
            }
            if (btn.BackColor == Color.LightGreen)
            {
                süreliMasaİsteğiGöderToolStripMenuItem.Visible = true;
                süresizMasaİsteğiGönderToolStripMenuItem.Visible = true;
            }
            
        }

        private void RadioButtonSeciliyeGore(object sender, EventArgs e)
        {
            radio = sender as RadioButton;
        }

        private void comboBosMasalar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnMasaAC_Click(object sender, EventArgs e)
        {
            if (Kullanici.KullaniciID==1)
            {
                string sqlsorgu = "set dateformat dmy insert into tblsepet (masaID,masa,acilisturu,baslangic,tarih) values('" + comboBosMasalar.Text.Substring(5) + "','" + comboBosMasalar.Text + "'" +
                       ",'" + radio.Text + "',@Baslangic ,@Tarih)";
                SqlCommand komut = new SqlCommand();
                komut.Parameters.AddWithValue("@Baslangic", DateTime.Parse(DateTime.Now.ToString()));
                komut.Parameters.AddWithValue("@Tarih", DateTime.Parse(DateTime.Now.ToString()));
                Veritabani.EklemeSilmeGüncelleme(komut, sqlsorgu);
                MessageBox.Show(comboBosMasalar.Text.Substring(0) + " açıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Yenile();
                radioButtonSuresiz.Checked = true;
               
            }
            else
            {
                MessageBox.Show("Böyle bir yetkiniz bulunmuyor", "Uyarı",MessageBoxButtons.OK ,MessageBoxIcon.Warning);
            }


        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Hesapla"].Index)
            {
                if (comboSaatUcreti.Text != "")
                {
                    DateTime BitisTarihi = DateTime.Now;
                    DateTime BaslangicTarihi = DateTime.Parse(dataGridView1.CurrentRow.Cells["BaslamaSaati"].Value.ToString());
                    TimeSpan saatfarki = BitisTarihi - BaslangicTarihi;
                    double saatfarki2 = saatfarki.TotalHours;
                    dataGridView1.CurrentRow.Cells["Sure"].Value = saatfarki2.ToString("0.00");
                    double toplamtutar = saatfarki2 * double.Parse(comboSaatUcreti.Text);
                    dataGridView1.CurrentRow.Cells["Tutar"].Value = toplamtutar.ToString("0.00");
                    dataGridView1.CurrentRow.Cells["BitisSaati"].Value = BitisTarihi;
                }
                if (comboSaatUcreti.Text == "")
                {
                    MessageBox.Show("Önce saat ücretini giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (e.ColumnIndex == dataGridView1.Columns["MasaKapat"].Index)
            {
                if (dataGridView1.CurrentRow.Cells["BitisSaati"].Value!=null)
                {
                    frmMasaKapat frm = new frmMasaKapat();
                    frm.txtID.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                    frm.txtMasaID.Text = dataGridView1.CurrentRow.Cells["Masa_ID"].Value.ToString();
                    frm.txtMasa.Text = dataGridView1.CurrentRow.Cells["_Masa"].Value.ToString();
                    frm.txtAcilisTuru.Text = dataGridView1.CurrentRow.Cells["AcilisTuru"].Value.ToString();
                    frm.txtBaslamaSaati.Text = dataGridView1.CurrentRow.Cells["BaslamaSaati"].Value.ToString();
                    frm.txtBitisSaati.Text = dataGridView1.CurrentRow.Cells["BitisSaati"].Value.ToString();
                    frm.txtSaatUcreti.Text = comboSaatUcreti.Text;
                    frm.txtSure.Text = dataGridView1.CurrentRow.Cells["Sure"].Value.ToString();
                    frm.txtTutar.Text = dataGridView1.CurrentRow.Cells["Tutar"].Value.ToString();
                    frm.txtTarih.Text = dataGridView1.CurrentRow.Cells["_Tarih"].Value.ToString();
                    frm.ShowDialog(); 
                }
               else if(dataGridView1.CurrentRow.Cells["BitisSaati"].Value == null)
                {
                    MessageBox.Show("Lütfen Önce Hesaplama Yapın", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }
       

        private void Istekler()
        {
            string sqlsorgu = "set dateformat dmy insert into tblhareketler(KullaniciID,MasaID,Masa,IstekTuru,Aciklama,Tarih) values('" +
                            Kullanici.KullaniciID + "','" + btn.Text.Substring(5)+ "','" + btn.Text + "','" + istek + "','Yapılmadı','" + DateTime.Now + "')";
            SqlCommand komut = new SqlCommand();
            Veritabani.EklemeSilmeGüncelleme(komut, sqlsorgu);
            Veritabani.ListviewdeKayitlarıGoster(listView1);
        }
        string istek = "";
        private void yöneticiÇağırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istek = "Yönetici çağırılıyor";
            Istekler();
        }
        private void süresizMasaİsteğiGönderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istek = "Süresiz masa açma isteği gönderildi";
            Istekler();

        }
        private void masaDeğiştirmeİsteğiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            istek = "Masa değiştirme isteği gönderildi";
            Istekler();
        }
        ToolStripMenuItem item;
        private void SureliIstekSecilirse(object sender, EventArgs e)
        {
            item = sender as ToolStripMenuItem;
            istek = item.Text + " dk süre ile masa açma isteği gönderildi";
            Istekler();
        }

        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if (sayac > 29)
            {
                if (comboSaatUcreti.Text != "")
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        DateTime BitisTarihi = DateTime.Now;
                        DateTime BaslangicTarihi = DateTime.Parse(dataGridView1.Rows[i].Cells["BaslamaSaati"].Value.ToString());
                        TimeSpan saatfarki = BitisTarihi - BaslangicTarihi;
                        double saatfarki2 = saatfarki.TotalHours;
                        dataGridView1.Rows[i].Cells["Sure"].Value = saatfarki2.ToString("0.00");
                        double toplamtutar = saatfarki2 * double.Parse(comboSaatUcreti.Text);
                        dataGridView1.Rows[i].Cells["Tutar"].Value = toplamtutar.ToString("0.00");
                        dataGridView1.Rows[i].Cells["BitisSaati"].Value = BitisTarihi; 
                    }
                }
                if (comboSaatUcreti.Text == "")
                {
                    MessageBox.Show("Önce saat ücretini giriniz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnMasaDegistir_Click(object sender, EventArgs e)
        {
            int SepetID =int.Parse(dataGridView1.CurrentRow.Cells["ID"].Value.ToString());
            int MasaID = int.Parse(dataGridView1.CurrentRow.Cells["Masa_ID"].Value.ToString());
            string sql1 = "update tblsepet set  MasaID='"+int.Parse(comboBosMasalar.Text.Substring(5))+"',Masa='"+comboBosMasalar.Text+"' where SepetID='"+SepetID+"'";
            SqlCommand cmd1 = new SqlCommand();
            Veritabani.EklemeSilmeGüncelleme(cmd1, sql1);

            string sql2 = "update tblmasalar set durumu='BOŞ' where MasaID='"+MasaID+"'";
            SqlCommand cmd2= new SqlCommand();
            Veritabani.EklemeSilmeGüncelleme(cmd2, sql2);

            string sql3 = "update tblmasalar set  durumu='DOLU' where MasaID='"+int.Parse(comboBosMasalar.Text.Substring(5))+"'";
            SqlCommand cmd3 = new SqlCommand();
            Veritabani.EklemeSilmeGüncelleme(cmd3, sql3);
            Yenile();
            MessageBox.Show("Masa değiştirme isteği başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
