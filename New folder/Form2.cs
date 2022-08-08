using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace store
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Formhistory FormAdmin = new Formhistory();
            this.Hide();
            FormAdmin.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Formdelete FormAdmin = new Formdelete();
            this.Hide();
            FormAdmin.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 Formdelete = new Form1();
            this.Hide();
            Formdelete.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
