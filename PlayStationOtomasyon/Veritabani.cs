using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;
using ListView = System.Windows.Forms.ListView;
using ListViewItem = System.Windows.Forms.ListViewItem;

namespace PlayStationOtomasyon
{
    internal class Veritabani
    {
        public static SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-70CN17D;Initial Catalog=PlayStationOtomasyon;Integrated Security=True;Pooling=False");
        public static DataTable SepetListele(DataGridView gridview)
        {
            SqlDataAdapter adtr = new SqlDataAdapter(" select *from tblsepet", baglanti);
            DataTable tbl = new DataTable();
            adtr.Fill(tbl);
            gridview.DataSource = tbl;
            return tbl;
            
        }
        public static DataTable ComboyaBosMasaGetir(ComboBox combo)
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter(" select *from tblmasalar where durumu='BOŞ'", baglanti);
            DataTable tbl = new DataTable();
            adtr.Fill(tbl);
            combo.DataSource = tbl;
            combo.DisplayMember = "Masalar";
            combo.ValueMember = "MasaID";
            baglanti.Close();
            return tbl;
        }
        public static void EklemeSilmeGüncelleme(SqlCommand command, string sorgu)
        {
            baglanti.Open();
            command.Connection = baglanti;
            command.CommandText = sorgu;
            command.ExecuteNonQuery();
            baglanti.Close();
            

        }
        public static SqlDataReader ListviewdeKayitlarıGoster(ListView list)
        {
            list.Items.Clear();
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select *from tblhareketler where tarih>=@Tarih", baglanti);
            cmd.Parameters.AddWithValue("@Tarih",DateTime.Parse(DateTime.Now.ToShortDateString()));
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr[0].ToString();
                ekle.SubItems.Add(dr[1].ToString());
                ekle.SubItems.Add(dr[2].ToString());
                ekle.SubItems.Add(dr[3].ToString());
                ekle.SubItems.Add(dr[4].ToString());
                ekle.SubItems.Add(dr[5].ToString());
                ekle.SubItems.Add(dr[6].ToString());
                list.Items.Add(ekle);

               
            } 
            baglanti.Close();
            return dr;
        }
    }
}
