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
using System.Xml;

namespace Capstone2
{
    public partial class Reschedule : Form
    {
        private string connectionString;
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;
        public DataTable dataTable;
        public BindingSource bindingSource;

        public string _Activity, _Person;
        public int _Date, _TimeStart, _TimeEnd, _PAX, _Contact;

        private BookingQuery requestBookingQuery;
        private RequestBooking requestBooking;
        private Client client;

        public Reschedule(Client client)
        {
            InitializeComponent();
            connectionString = "Data Source=LAPTOP-USNUQ76P\\SQLEXPRESS;Initial Catalog=Captone;Integrated Security=True";
            sqlConnect = new SqlConnection(connectionString);
            this.client = client;
        }

        SqlConnection Con = new SqlConnection("Data Source=LAPTOP-USNUQ76P\\SQLEXPRESS;Initial Catalog=Captone;Integrated Security=True");
        private void Reschedule_Load(object sender, EventArgs e)
        {
            populate();
            displayID();
        }

        private void BnResched_Click(object sender, EventArgs e)
        {
            UpdateInfo();
            client.RefreshListOfClubMembers();

        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Client client = new Client();
            client.Show();
            this.Hide();
        }

        private void cbUserID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sqlConnect.Open();
                sqlCommand = new SqlCommand("SELECT Activity, Date, TimeStart, TimeEnd, PAX, Person, Contact FROM TB_Request WHERE UserID = @UserID", sqlConnect);
                sqlCommand.Parameters.Add("@UserID", SqlDbType.BigInt).Value = cbUserID.Text;

                sqlReader = sqlCommand.ExecuteReader();

                if (sqlReader.Read())
                {
                    TbEventName.Text = sqlReader["Activity"].ToString();
                    CbEventDate.Text = sqlReader["Date"].ToString();
                    Start.Text = sqlReader["TimeStart"].ToString();
                    End.Text = sqlReader["TimeEnd"].ToString();
                    TbPAX.Text = sqlReader["PAX"].ToString();
                    TbPerson.Text = sqlReader["Person"].ToString();
                    TbContactNo.Text = sqlReader["Contact"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving information: " + ex.Message);
            }
            finally
            {
                sqlConnect.Close();
            }
        }

        public void UpdateInfo()
        {
            try
            {
                sqlConnect.Open();

                TimeSpan TimeStart = Start.Value.TimeOfDay;
                TimeSpan TimeEnd = End.Value.TimeOfDay;

                sqlCommand = new SqlCommand("UPDATE TB_Request SET Activity = @Activity, Date = @Date, TimeStart = @TimeStart, TimeEnd = @TimeEnd, PAX = @PAX, Person = @Person, Contact = @Contact WHERE UserID = @UserID", sqlConnect);

                sqlCommand.Parameters.Add("@Activity", SqlDbType.VarChar).Value = TbEventName.Text;
                sqlCommand.Parameters.Add("@Date", SqlDbType.DateTime).Value = Convert.ToDateTime(CbEventDate.Text);
                sqlCommand.Parameters.Add("@TimeStart", SqlDbType.Time).Value = TimeStart;
                sqlCommand.Parameters.Add("@TimeEnd", SqlDbType.Time).Value = TimeEnd;
                sqlCommand.Parameters.Add("@PAX", SqlDbType.BigInt).Value = Convert.ToInt32(TbPAX.Text);
                sqlCommand.Parameters.Add("@Person", SqlDbType.VarChar).Value = TbPerson.Text;
                sqlCommand.Parameters.Add("@Contact", SqlDbType.BigInt).Value = Convert.ToInt64(TbContactNo.Text);
                sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(cbUserID.Text);

                sqlCommand.ExecuteNonQuery();
                populate();
                MessageBox.Show("Updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating information: " + ex.Message);
            }
            finally
            {
                sqlConnect.Close();
            }
        }

        public void displayID()
        {
            try
            {
                sqlConnect.Open();
                sqlCommand = new SqlCommand("SELECT UserID FROM TB_Request", sqlConnect);
                sqlReader = sqlCommand.ExecuteReader();

                while (sqlReader.Read())
                {
                    cbUserID.Items.Add(sqlReader["UserID"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying ID: " + ex.Message);
            }
            finally
            {
                sqlConnect.Close();
            }
        }
        public void populate()
        {
            try
            {
                Con.Open();
                string MyQuery = "Select * from TB_Request";
                SqlDataAdapter da = new SqlDataAdapter(MyQuery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                DataGrid.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
        private void DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
