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
    public partial class Formdelete : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        public Formdelete()
        {
            InitializeComponent();
        }

        private void Formdelete_Load(object sender, EventArgs e)
        {
            //ดึงข้อมูลจากDBลงdataGridView
            MySqlConnection conn = databaseConnection();
            conn.Open();
            string sql = "SELECT * FROM product";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            conn.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;
            textBoxid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();  //กรอกข้อมูลลงในดาต้าเบส
            textBoxname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxprice.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBoxamount.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();  
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBoxid.Text); //รับข้อมูลเป็นตัวเลขกับตัวหนังสื่อ
            string name = textBoxname.Text;
            int price = Convert.ToInt32(textBoxprice.Text);
            int amount = Convert.ToInt32(textBoxamount.Text);
            MySqlConnection conn = databaseConnection();
            conn.Open();
            string sql = "UPDATE product SET name = '"+name+"', price = '"+ price +"', amount = '"+ amount +"' WHERE id = '"+id+"'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int rows = cmd.ExecuteNonQuery(); 
            conn.Close();
            if (MessageBox.Show("Are you sure", "Title_here", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (rows > 0)
                {
                    MessageBox.Show("แก้ไขข้อมูลแล้ว");
                    conn.Open();
                    sql = "SELECT * FROM product";
                    cmd = new MySqlCommand(sql, conn);
                    DataSet ds = new DataSet();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0].DefaultView; //
                    conn.Close();
                }
            }
                
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = databaseConnection();
            String sql = "INSERT INTO product (name,price,amount) VALUES('" + textBoxname.Text + "' , '" + textBoxprice.Text + "','" + textBoxamount.Text + "')";
            MySqlCommand cmd2 = new MySqlCommand(sql, conn);
            conn.Open();
            int rows = cmd2.ExecuteNonQuery();
            conn.Close();
            if (MessageBox.Show("Are you sure", "Title_here", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (rows > 0)
                {
                    MessageBox.Show("เพิ่มข้อมูลสำเร็จ");
                    conn.Open();
                    sql = "SELECT * FROM product";
                    cmd2 = new MySqlCommand(sql, conn);
                    DataSet ds = new DataSet();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd2);
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    conn.Close();
                }
            }    
                
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.CurrentCell.RowIndex;
            int deleteId = Convert.ToInt32(dataGridView1.Rows[selectedRow].Cells["id"].Value);
            MySqlConnection conn = databaseConnection();
            String sql = "DELETE FROM product WHERE id = '" + deleteId + "'";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            conn.Open();

            int rows = cmd.ExecuteNonQuery();

            conn.Close();
            if (MessageBox.Show("Are you sure","Title_here", MessageBoxButtons.YesNo ) == DialogResult.Yes)
                {
                if (rows > 0)
                {

                    MessageBox.Show("ลบข้อมูลสำเร็จ");

                    conn.Open();
                    sql = "SELECT * FROM product";
                    cmd = new MySqlCommand(sql, conn);
                    DataSet ds = new DataSet();
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0].DefaultView;
                    conn.Close();
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 Formdelete = new Form2();
            this.Hide();
            Formdelete.Show();
        }
    }
}
