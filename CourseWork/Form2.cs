using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
      
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                Form2 s = new Form2();
                s.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Пароль или логин не верены!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
