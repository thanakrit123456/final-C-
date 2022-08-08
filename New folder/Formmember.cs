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
    public partial class Formmember : Form
    {
        int x = 0; 
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        public Formmember()
        {
            InitializeComponent();
        }
        
        
        

        private void register_btn_Click(object sender, EventArgs e)
        {
            x = 0;
            MySqlConnection conn = databaseConnection();
            conn.Open();
            MySqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = $"SELECT*FROM member WHERE username =\"{textBoxuesr.Text}\"";
            MySqlDataReader row = cmd.ExecuteReader();
            if (row.HasRows) //เช็คเเถวนั้นว่ามีไอดีนี้หรือยัง
            {
                MessageBox.Show("มีชื่อผู้ใช้นี้อยู่ในระบบอยู่แล้ว");
                x = 2;
            }
            if (x == 0)  //ถ้าไม่มีก็บันทึกลงไป
            {
                
                MySqlConnection conn2 = databaseConnection();
                String sql = "INSERT INTO member (name,username,password) VALUES('" + textBoxname.Text + "' , '" + textBoxuesr.Text + "','" + textBoxpass.Text + "')";
                MySqlCommand cmd2 = new MySqlCommand(sql, conn2);
                conn2.Open();
                int rows = cmd2.ExecuteNonQuery();
                
                conn2.Close();
                if (rows > 0)

                {
                    
                    MessageBox.Show("Add data complete");
                }
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 65 || e.KeyChar > 90)&& (e.KeyChar < 97 || e.KeyChar > 122) && (e.KeyChar != 8) && (e.KeyChar != 32))
            {
                
                e.Handled = true;  //ดักให้กรอกชื่อได้เเค่ตัวengใส่ตัวเลขไม่ได้
            }
        }

        private void Formmember_Load(object sender, EventArgs e)
        {

        }

        private void textBoxpass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
