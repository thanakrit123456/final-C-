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
    public partial class Formproduct : Form
    {
        
        
        //int na = 0;
        //String mv;
        //int ml = 0;
        //int sumbt = 0;
        //int total = 0;
       // int sub = 0;
        //String nametrue;
        //String pricee;
        //String menu;

        string img;
        private List<cart> shoppingCart = new List<cart>(); //ประกาศเพื่อเียกใช้ cart
        float Sumprice_product;
        public Formproduct()

        {
            InitializeComponent();
        }
        private MySqlConnection databaseConnection()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=project;charset=utf8;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 Formproduct = new Form1();
            this.Hide();
            Formproduct.Show();
        }
        public static int lastgrub = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = databaseConnection();
            
            string grub = "SELECT grub FROM history"; 
            MySqlCommand cmd = new MySqlCommand(grub, conn);
            conn.Open();

            MySqlDataReader dr = cmd.ExecuteReader();
            List<int> list = new List<int>(); //เก็บลิสไว้ในลิส

            while (dr.Read())
            {
                list.Add(dr.GetInt32("grub"));
            }

            
            lastgrub = list[list.Count - 1] + 1; //ให้+เพิ่มเข้าไป1จากค่าเดิม
            
            DataSet ds = new DataSet(); 

            conn.Close();

            for (int i = 0; i < shoppingCart.Count; i++) //ถ้าiมีค่า=0 ถ้า iมีค่าน้อยกว่าของในตะกร้าให้เพิ่มiเข้าไป
            {

                string history = @"INSERT INTO history (username,productname , amount , price, grub,day,time	)" +
                $"VALUES('" + Form1.globaluser + "','" + databuy.Rows[i].Cells[1].Value + "','" + databuy.Rows[i].Cells[3].Value + "','" + Convert.ToInt32(databuy.Rows[i].Cells[2].Value) * Convert.ToInt32(databuy.Rows[i].Cells[3].Value) + "','" + lastgrub + "','" + label2.Text + "','" + label3.Text + "')";
                cmd = new MySqlCommand(history, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
            Formbill Formproduct = new Formbill();
            Hide();
            Formproduct.ShowDialog();
            

        }

        private void Formproduct_Load(object sender, EventArgs e) //เเสดงในdatagit
        {
            //ดึงข้อทมูลจากtableออกมาเเสดงในดาต้ากิต 
            label2.Text = System.DateTime.Now.ToShortDateString();
            label3.Text = System.DateTime.Now.ToShortTimeString();

            MySqlConnection conn = databaseConnection();
            conn.Open();
            string sql = "SELECT * FROM product";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            DataSet ds = new DataSet();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(ds);
            dataProduct.DataSource = ds.Tables[0].DefaultView;
            conn.Close();

            buy_btn.Enabled = false;
        }

        private void dataProduct_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return; //เมื่อเราคลิกเลือกที่สินค้าเเล้วให้เเสดงชื่อราคาในlabel
            id_label.Text = dataProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
            nameproduct_label.Text = dataProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            priceproduct_label.Text = dataProduct.Rows[e.RowIndex].Cells[2].Value.ToString();

            img = dataProduct.Rows[e.RowIndex].Cells[4].Value.ToString(); 
            Console.WriteLine(img);


            img_product2.Image = Image.FromFile(img); //เปิดเรียกใช้รูปในไฟล์

            count_buy_numberupdown.Enabled = true;
        }
        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void buy_btn_Click(object sender, EventArgs e)
        {

        }

        private void databuy_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var hti = databuy.HitTest(e.X, e.Y);
                databuy.Rows[hti.RowIndex].Selected = true;
                DeletecontextMenuStrip.Show(databuy, e.X, e.Y); //ให้โชว์ที่ติดแหน่งนั้น
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = databuy.CurrentCell.RowIndex; //เก็บค่า index แถว
            if (MessageBox.Show("Are you sure", "Title_here", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                shoppingCart.RemoveAt(index); //ลบข้อมูลที่เลือก
            }
            
            //รีเฟชรตาราง
            databuy.DataSource = null;
            databuy.DataSource = shoppingCart;
            //รวมค่า
            Sumprice_product = shoppingCart.Sum(y => y.ราคารวม);

            pricesum_label.Text = Sumprice_product.ToString();
        }

        private void count_buy_numberupdown_ValueChanged(object sender, EventArgs e)
        {
            if(count_buy_numberupdown.Value <= 0) //ถ้าปุ่มน้อยกว่า 0 ถ้าเป็นจริงจะปิดปุ่มไม่ให้กดซื้อ เเต่ถ้าไม่จะดึงค่าออกมา
            {
                buy_btn.Enabled = false;
            }
            else
            {
                dataProduct.CurrentRow.Selected = true; 
                int index = dataProduct.CurrentCell.RowIndex;
                int amount = Convert.ToInt32(dataProduct.Rows[index].Cells[3].Value.ToString());
                if (count_buy_numberupdown.Value <= amount) //ถ้า<= เป็นจริงจะเปิดปุ่มถ้าไม่amountก็ต้อง = count_buy_numberupdown.Value
                {
                    buy_btn.Enabled = true;
                } else
                {
                    count_buy_numberupdown.Value = amount;
                }
                
            }
        }

        private void buy_btn_Click_1(object sender, EventArgs e)
        {
            if (buy_btn.Enabled)
            {
                buy_btn.Enabled = false;
            }
            else
            {
                buy_btn.Enabled = true;
            }
        }


        
        private void buy_btn_Click_3(object sender, EventArgs e)
        {
            float price = float.Parse(priceproduct_label.Text);
            cart item = new cart()
            {
                รหัสสินค้า = id_label.Text,
                ชื่อสินค้า = nameproduct_label.Text,
                ราคา = float.Parse(priceproduct_label.Text),
                จำนวนสินค้าที่ซื้อ = float.Parse(count_buy_numberupdown.Text),
                ราคารวม = float.Parse(count_buy_numberupdown.Value.ToString()) * price

            };
            Console.WriteLine(float.Parse(count_buy_numberupdown.Value.ToString()) * price);
            //เพิ่มเข้า databuy
            shoppingCart.Add(item);
            databuy.DataSource = null;
            databuy.DataSource = shoppingCart;
            databuy.CurrentCell = databuy.Rows[databuy.Rows.Count - 1].Cells[0];
            //รวมค่า
            float TotalTopay;
            TotalTopay = shoppingCart.Sum(y => y.ราคารวม); //นับจำนวนที่ขาย
            pricesum_label.Text = TotalTopay.ToString();
            allprice = pricesum_label.Text;


        }

        private void databuy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //เปลี่ยนข้อมูลเป็นตัวนสเพื่อเเสดงในlabel
                databuy.CurrentRow.Selected = true;
                id_label.Text = databuy.Rows[e.RowIndex].Cells[0].Value.ToString();
                nameproduct_label.Text = databuy.Rows[e.RowIndex].Cells[1].Value.ToString();
                priceproduct_label.Text = databuy.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch
            {

            }
            
        }

      

        private void databuy_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public static string allprice;

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongDateString();
            label3.Text = DateTime.Now.ToLongTimeString();
        }

        private void dataProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            e.Graphics.DrawImage(new Bitmap(@"Z:\img_store\pay.png"), new Point(220 + 200, 790 + 160));
            

            
            e.Graphics.DrawString("Vegetable SHOP", new Font("Century Gothic", 26, FontStyle.Bold), Brushes.DarkBlue, new Point(270, 60));
            e.Graphics.DrawString("Date " + System.DateTime.Now.ToString("dd/MM/yyyy HH : mm : ss น."), new Font("Century Gothic", 14, FontStyle.Regular), Brushes.RosyBrown, new PointF(500, 150));
            e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("Century Gothic", 16, FontStyle.Regular), Brushes.Maroon, new Point(80, 190));
            e.Graphics.DrawString(" ชื่อสินค้า                           จำนวน                       ราคา", new Font("Century Gothic", 20, FontStyle.Regular), Brushes.Brown, new Point(130, 220));
            
            MySqlConnection conn = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=project;");
            MySqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = $"SELECT productname,amount,price,day FROM history WHERE grub = '{Formproduct.lastgrub}'";
            MySqlDataReader dr = cmd.ExecuteReader();
            int  y_ = 0;
            while (dr.Read())
            {
                e.Graphics.DrawString(dr.GetString("productname"), new Font("Century Gothic", 16, FontStyle.Bold), Brushes.LightCoral, new Point(140, 270 + 40 * y_));
                e.Graphics.DrawString(dr.GetString("amount"), new Font("Century Gothic", 16, FontStyle.Bold), Brushes.LightCoral, new Point(480, 270 + 40 * y_));
                e.Graphics.DrawString(dr.GetString("price"), new Font("Century Gothic", 16, FontStyle.Bold), Brushes.LightCoral, new Point(745, 270 + 40 * y_));
                
                y_++; //+yเพื่อเพิ่มบรรทัด
            }
            conn.Close();

            e.Graphics.DrawString("-----------------------------------------------------------------------------", new Font("Century Gothic", 16, FontStyle.Regular), Brushes.Maroon, new Point(80, 900));
            int y = 320;
            
            //e.Graphics.DrawString("" + "", new Font("Century Gothic", 16, FontStyle.Regular), Brushes.PaleVioletRed, new PointF(80, 280));
            //e.Graphics.DrawString("" + "", new Font("Century Gothic", 16, FontStyle.Regular), Brushes.PaleVioletRed, new PointF(650, 280));
            
            
            {
                
                e.Graphics.DrawString($"รวม {pricesum_label.Text} Bath", new Font("Century Gothic", 24, FontStyle.Regular), Brushes.DarkBlue, new Point(80, (y + 590) + 45));

                e.Graphics.DrawString("บัญชีพร้อมเพย์               " , new Font("Century Gothic", 24, FontStyle.Regular), Brushes.DarkBlue, new Point(120, ((y + 600) + 45) + 45));
                e.Graphics.DrawString("นายธนกฤต แม้นดินแดง           " , new Font("Century Gothic", 24, FontStyle.Regular), Brushes.DarkBlue, new Point(80, (((y + 600) + 45) + 45) + 45));


            }
           
        }

        private void pricesum_label_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void DeletecontextMenuStrip_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
