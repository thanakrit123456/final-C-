using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace store
{
    public partial class Formbill : Form
    {
        public Formbill()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Formbill_Load(object products, EventArgs e)
        {


            MySqlConnection con = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=project;");
            con.Open();
            string sql = $"SELECT productname,amount,price,day FROM history WHERE grub = '{Formproduct.lastgrub}'";
            MySqlCommand cmd = new MySqlCommand(sql, con);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            databill.DataSource = ds.Tables[0].DefaultView;
            con.Close();

            MySqlConnection con_1 = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=project;");
            con_1.Open();
            string grub = $"SELECT * FROM history WHERE grub = '{Formproduct.lastgrub}'";
            MySqlCommand cmd_ = new MySqlCommand(grub, con_1);
            con.Open();
            MySqlDataReader dr1 = cmd_.ExecuteReader();
            //List<int> list = new List<int>();
            
            while (dr1.Read())
            {
                
                MySqlConnection con_ = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=project;");
                string grub__ = $"SELECT * FROM product WHERE name = '{dr1.GetString("productname")}'";
                
                MySqlCommand cmd__ = new MySqlCommand(grub__, con_);
                con_.Open();
                MySqlDataReader dr_ = cmd__.ExecuteReader();
                //List<int> list__ = new List<int>();
                if (dr_.Read())
                {
                    MySqlConnection con1 = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=project;");
                    int x = dr_.GetInt32("amount") - dr1.GetInt32("amount");
                    string sql_ = "UPDATE product SET amount = '" + x + "' WHERE name = '" + dr1.GetString("productname") + "' ";
                    con1.Open();
                    MySqlCommand cmd_1 = new MySqlCommand(sql_, con1);
                    int rows = cmd_1.ExecuteNonQuery();
                    con.Close();
                }
            }
            label1.Text = Formproduct.allprice;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
