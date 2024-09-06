using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using System.Web.UI.HtmlControls;

namespace Capstone2
{
    public partial class LoginF : Form
    {
        public LoginF()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection("Data Source=LAPTOP-USNUQ76P\\SQLEXPRESS;Initial Catalog=Captone;Integrated Security=True");

        private void label3_Click(object sender, EventArgs e)
        {
            UsernameTb.Clear();
            PasswordTb.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
                PasswordTb.UseSystemPasswordChar = true;
            else
                PasswordTb.UseSystemPasswordChar = false;
        }

        private void BnBookIncharge_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from tb_employee where Username= '" + UsernameTb.Text + "'and Password= '" + PasswordTb.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Employee employee = new Employee();
                employee.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
            }

        }

        private void BnAdmin_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from tb_admin where Username= '" + UsernameTb.Text + "'and Password= '" + PasswordTb.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                Employee employee = new Employee();
                employee.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password");
            }

        }

        private void Client_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PasswordTb_TextChanged(object sender, EventArgs e)
        {
            PasswordTb.UseSystemPasswordChar = true;
        }
    }
}
