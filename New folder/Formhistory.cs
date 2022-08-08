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
    public partial class Formhistory : Form
    {
      
        public Formhistory()
        {
            InitializeComponent();
        }

        private void Formhistory_Load(object sender, EventArgs e)
        {
            showdataGridView1();
            
            timer1.Start();
            DateTime.Now.ToLongDateString();
            DateTime.Now.ToLongTimeString();
        }
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }

        private void showdataGridView1()
        {
            MySqlConnection conn = databaseConnection(); //เเสดงข้อมูลลูกค้าที่ซื้อทั้งหมด
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM history";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "") //พิมพ์ค้นหาชื่อในDB
            {
                MySqlConnection conn = databaseConnection();
                DataSet ds = new DataSet();
                conn.Open();
                MySqlCommand cmd;
                cmd = conn.CreateCommand();
                cmd.CommandText = ($"SELECT*FROM history WHERE username like\"%{textBox1.Text}\"");
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(ds);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MySqlConnection conn2 = databaseConnection();
                    conn2.Open();
                    MySqlCommand cmd2;
                    cmd2 = conn2.CreateCommand(); // เอาราคาของสิ้นค้าที่เลือกซื้อ ใน From history มาบวกกัน ให้เป็นราคาทั้งหมดของคนนั้น
                    cmd2.CommandText = ($"SELECT SUM(price)  FROM history WHERE username like\"%{textBox1.Text}\"");
                    MySqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        textBox2.Text = Convert.ToString(dr2[0]); //  ราคารวมทั้งหมดที่ textBox2
                    }
                    conn2.Close();
                }
                conn.Close();
                dataGridView1.DataSource = ds.Tables[0].DefaultView; // โชว์ข้อมูลลูกค้าใน dataGridView 
            }
            else
            {
                showdataGridView1(); //ถ้าไม่เสิร์ชชื่อ ก็จะแสดงข้อมูลลูกค้าทั้งหมด
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "0"; 
            MySqlConnection conn = databaseConnection();
            DataSet ds = new DataSet();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = ($"SELECT*FROM history WHERE day BETWEEN \"{dateTimePicker1.Text}\" AND \"{dateTimePicker2.Text}\""); //เลือกระหว่างวันนี้-วันนั้น
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            adapter.Fill(ds);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                MySqlConnection conn2 = databaseConnection();
                conn2.Open();
                MySqlCommand cmd2;
                cmd2 = conn2.CreateCommand(); // เอาราคาของสิ้นค้าที่เลือกซื้อ ใน From history มาบวกกัน  ในระหว่างวันที่เราเลือกในปฏิทิน
                cmd2.CommandText = ($"SELECT SUM(price) FROM history WHERE day BETWEEN \"{dateTimePicker1.Text}\" AND \"{dateTimePicker2.Text}\"");
                MySqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {

                    textBox2.Text = Convert.ToString(dr2[0]); // จะขึ้นโชว์ ราคารวมยอดขายทั้งหมดที่ textBox2
                }
                conn2.Close();
            }
            conn.Close();
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form2 Formdelete = new Form2();
            this.Hide();
            Formdelete.Show();
        }
    }
}
