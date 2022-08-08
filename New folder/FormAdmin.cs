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
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 FormAdmin = new Form2();
            this.Hide();
            FormAdmin.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
