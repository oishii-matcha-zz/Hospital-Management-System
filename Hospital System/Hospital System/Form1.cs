using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtBoxUser.Text;
            string pass = txtBoxPassword.Text;

            if (username == "csa" && pass == "pass")
            {
                //MessageBox.Show("You have entered the correct information!");

                this.Hide();
                Dashboard ds = new Dashboard();
                ds.Show();
            }
            else
            {
                MessageBox.Show("Oops! Please enter the correct information!");
            }
        }
    }
}
