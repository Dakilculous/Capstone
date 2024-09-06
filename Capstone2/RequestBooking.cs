using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Guna.UI2.WinForms;

namespace Capstone2
{
    public partial class RequestBooking : Form
    {
        private BookingQuery bookingQuery;
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private string connectionString;

        public RequestBooking()
        {
            InitializeComponent();
            sqlConnect = new SqlConnection("Data Source=LAPTOP-USNUQ76P\\SQLEXPRESS;Initial Catalog=Captone;Integrated Security=True");
            bookingQuery = new BookingQuery();
            RefreshListOfClubMembers();

        }

        private int RequestID()
        {
            int newRequestID = 1;

            try
            {
                // Open the connection
                sqlConnect.Open();

                // Create a SQL command to get the current maximum RequestID
                string query = "SELECT ISNULL(MAX(UserID), 0) FROM TB_Request";
                sqlCommand = new SqlCommand(query, sqlConnect);

                // Execute the query and get the current max RequestID
                object result = sqlCommand.ExecuteScalar();

                if (result != null)
                {
                    newRequestID = Convert.ToInt32(result) + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating RequestID: " + ex.Message);
            }
            finally
            {
                // Ensure the connection is closed
                sqlConnect.Close();
            }

            return newRequestID;
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void RequestBooking_Load(object sender, EventArgs e)
        {
            Start.CustomFormat = "HH:mm:ss";
            End.CustomFormat = "HH:mm:ss";
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BnRequest_Click(object sender, EventArgs e)
        {

            int ID = RequestID();
            string Activity = TbEventName.Text;
            DateTime Date = Convert.ToDateTime(CbEventDate.Text);

            TimeSpan TimeStart = Start.Value.TimeOfDay;
            TimeSpan TimeEnd = End.Value.TimeOfDay;

            // Validate the PAX input
            int PAX = 0;
            if (!string.IsNullOrWhiteSpace(TbPAX.Text))
            {
                if (!int.TryParse(TbPAX.Text, out PAX))
                {
                    MessageBox.Show("Please enter a valid number for PAX.");
                    return; // Exit the method if the input is not valid
                }
            }

            string Person = TbPerson.Text;

            // Validate the Contact number input
            long Contact;
            if (!long.TryParse(TbContactNo.Text, out Contact))
            {
                MessageBox.Show("Please enter a valid number for Contact.");
                return; // Exit the method if the input is not valid
            }

            if (bookingQuery.Requester(ID, Activity, Date, TimeStart, TimeEnd, PAX, Person, Contact))
            {
                RefreshListOfClubMembers();
                MessageBox.Show("Request successfully.");
            }
            else
            {
                MessageBox.Show("Error request.");
            }
        }

        private void TbEventName_TextChanged(object sender, EventArgs e)
        {

        }

        private void CbEventDate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CbEventTime_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TbPAX_TextChanged(object sender, EventArgs e)
        {

        }

        private void TbPerson_TextChanged(object sender, EventArgs e)
        {

        }

        private void TbContactNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void Start_ValueChanged(object sender, EventArgs e)
        {
        }

        private void End_ValueChanged(object sender, EventArgs e)
        {
        }
        public void RefreshListOfClubMembers()
        {
            if (bookingQuery != null)
            {
                bookingQuery.DisplayList();
                DataGrid.DataSource = bookingQuery.bindingSource;
            }
            else
            {
                MessageBox.Show("Booking query is not initialized.");
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.Show();
            this.Hide();
        }
    }
}
