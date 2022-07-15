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
    public partial class frmMasaKapat : Form
    {
        public frmMasaKapat()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnMasaKapat_Click(object sender, EventArgs e)
        {
            //string sqlsorgu = " update tblmasalar set durumu ='BOŞ' where MasaID='" + txtMasaID.Text + "'";
            //SqlCommand komut = new SqlCommand();
            //Veritabani.EklemeSilmeGüncelleme(komut, sqlsorgu);
            //string sqlsorgu2 = "delete from tblsepet where  SepetID='"+txtID.Text+"'";
            //SqlCommand komut2 = new SqlCommand();
            //Veritabani.EklemeSilmeGüncelleme(komut2, sqlsorgu2);
            string sorgu = "set dateformat dmy insert into TBLSatis(KullaniciID,MasaID,AcilisTuru,BaslangicSaati,BitisSaati,Sure,Tutar,Aciklama,Tarih)" +
                "values('"+Kullanici.KullaniciID+"','"+int.Parse(txtMasaID.Text)+"','"+txtAcilisTuru.Text+"',@Baslangic,@Bitis,@Sure,@Tutar,'Açıklama yapılmadı',@Tarih)";
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Baslangic", DateTime.Parse(txtBaslamaSaati.Text));
            cmd.Parameters.AddWithValue("@Bitis", DateTime.Parse(txtBitisSaati.Text));
            cmd.Parameters.AddWithValue("@Sure", decimal.Parse(txtSure.Text));
            cmd.Parameters.AddWithValue("@Tutar", decimal.Parse(txtTutar.Text));
            cmd.Parameters.AddWithValue("@Tarih", DateTime.Parse(txtTarih.Text));
            Veritabani.EklemeSilmeGüncelleme(cmd, sorgu);

            this.Close();
            Form1 frm = (Form1)Application.OpenForms["Form1"];
            frm.Yenile();

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMasaKapat_Load(object sender, EventArgs e)
        {

        }
    }
}
