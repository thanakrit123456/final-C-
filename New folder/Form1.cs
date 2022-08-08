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
    public partial class Form1 : Form
    {
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        /*private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=product;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }*/
        public Form1()
        {
            InitializeComponent();
        }
        public static string globaluser;
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormAdmin Form1 = new FormAdmin();
            this.Hide();
            Form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Formmember Form1 = new Formmember();
            this.Hide();
            Form1.Show();
        }
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            //ตรวจสอบข้อมูลว่ามีในDBหรือไม่
            MySqlConnection conn = databaseConnection();
            MySqlDataAdapter sql = new MySqlDataAdapter("SELECT COUNT(*) FROM member WHERE username = '" + textBox1.Text + "' AND password = '" + textBox2.Text + "'", conn);
            DataTable login = new DataTable();
            sql.Fill(login);
            if (login.Rows[0][0].ToString() == "1")
            {
                globaluser = textBox1.Text;
                MessageBox.Show("เข้าสู่ระบบสำเร็จ");
                Formproduct Login = new Formproduct();
                Hide();
                Login.ShowDialog();
                this.Close();

                MySqlConnection connn = databaseConnection();
                connn.Open();

                MySqlCommand cmd;

                cmd = connn.CreateCommand();
                cmd.CommandText = $"SELECT*FROM account WHERE  username =\"{textBox1.Text}\" AND password=\"{ textBox2.Text}\"";

                MySqlDataReader Row = cmd.ExecuteReader();
                /*if (Row.HasRows)
                {
                    //Formproduct f3 = new Formproduct();
                    MySqlConnection conn3 = databaseConnection();
                    conn3.Open(); // สร้างพารามิเตอร์ ID เก็บค่า usernameText 
                    MySqlCommand cmd2 = new MySqlCommand("SELECT firstname from account where  username = @ID", conn3);
                    cmd2.Parameters.AddWithValue("@ID", (textBox1.Text));
                    MySqlDataReader da = cmd2.ExecuteReader();
                    while (da.Read())

                    MessageBox.Show("เข้าสู่ระบบสำเร็จ");
                    /*Formproduct f1 = new Formproduct();
                    this.Hide();
                    f1.Show();
                    
                    {
                        f1.namez = da.GetValue(0).ToString();
                    }
                }*/
            }
            else
            {
                MessageBox.Show("กรอกข้อมูลให้ถูกต้อง");
                Form1 login1 = new Form1();
                Hide();
                login1.ShowDialog();
                this.Close();

            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           /* if ((e.KeyChar < 65 || e.KeyChar > 90) && (e.KeyChar < 97 || e.KeyChar > 122) && (e.KeyChar != 8) && (e.KeyChar != 32))
            {

                e.Handled = true;  //
            }*/
        }
    }
}
