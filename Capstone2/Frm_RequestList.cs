using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capstone2
{
    public partial class Frm_RequestList : Form
    {
        private BookingQuery bookingQuery;
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private string connectionString;
        public Frm_RequestList()
        {
            InitializeComponent();
            sqlConnect = new SqlConnection("Data Source=LAPTOP-USNUQ76P\\SQLEXPRESS;Initial Catalog=Captone;Integrated Security=True");
            bookingQuery = new BookingQuery();
            RefreshListOfClubMembers();
            btnDelete.Click += new EventHandler(btnDelete_Click);
        }

        private void Frm_RequestList_Load(object sender, EventArgs e)
        {

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DataGrid.SelectedRows.Count > 0)
            {
                int selectedUserID = Convert.ToInt32(DataGrid.SelectedRows[0].Cells["UserID"].Value);
                DialogResult result = MessageBox.Show("Are you sure you want to delete this request?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (DeleteMember(selectedUserID))
                    {
                        MessageBox.Show("Member deleted successfully.");
                        RefreshListOfClubMembers();
                    }
                    else
                    {
                        MessageBox.Show("Error deleting member.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a member to delete.");
            }
        }
        private bool DeleteMember(int userID)
        {
            try
            {
                sqlConnect.Open();
                sqlCommand = new SqlCommand("DELETE FROM TB_Request WHERE UserID = @UserID", sqlConnect);
                sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                sqlCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting request: " + ex.Message);
                return false;
            }
            finally
            {
                sqlConnect.Close();
            }
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
    }
}
