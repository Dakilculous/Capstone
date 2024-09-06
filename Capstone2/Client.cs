using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone2
{

    public partial class Client : Form
    {
        private BookingQuery bookingQuery;
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        public Client()
        {
            InitializeComponent();
            bookingQuery = new BookingQuery();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=LAPTOP-USNUQ76P\SQLEXPRESS;Initial Catalog=Captone;Integrated Security=True");
        void populate()
        {
            try
            {
                Con.Open();
                string MyQuery = "Select * from TB_Request";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                guna2DataGridView1.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        private void Client_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            RequestBooking booking = new RequestBooking();
            booking.Show();
            this.Hide();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Reschedule resched = new Reschedule(this);
            resched.ShowDialog();
            this.Close();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }
        public void RefreshListOfClubMembers()
        {
            bookingQuery.DisplayList();
            guna2DataGridView1.DataSource = bookingQuery.bindingSource;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
