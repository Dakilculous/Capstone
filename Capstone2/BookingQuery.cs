using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Xml;

namespace Capstone2
{
    internal class BookingQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        public DataTable dataTable;
        public BindingSource bindingSource;
        private string connectionString;

        public BookingQuery()
        {
            connectionString = "Data Source=LAPTOP-USNUQ76P\\SQLEXPRESS;Initial Catalog=Captone;Integrated Security=True";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
            sqlAdapter = new SqlDataAdapter();
        }

        
        public bool Requester(int UserID, string Activity, DateTime Date, TimeSpan TimeStart, TimeSpan TimeEnd, int PAX, string Person, long Contact)
        {
            try
            {
                sqlCommand = new SqlCommand("INSERT INTO TB_Request (UserID, Activity, Date, TimeStart, TimeEnd, PAX, Person, Contact) VALUES(@UserID, @Activity, @Date, @TimeStart, @TimeEnd, @PAX, @Person, @Contact)", sqlConnect);
                sqlCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                sqlCommand.Parameters.Add("@Activity", SqlDbType.VarChar).Value = Activity;
                sqlCommand.Parameters.Add("@Date", SqlDbType.DateTime).Value = Date;
                sqlCommand.Parameters.Add("@TimeStart", SqlDbType.Time).Value = TimeStart.ToString(@"hh\:mm\:ss");
                sqlCommand.Parameters.Add("@TimeEnd", SqlDbType.Time).Value = TimeEnd.ToString(@"hh\:mm\:ss");

                sqlCommand.Parameters.Add("@PAX", SqlDbType.BigInt).Value = PAX;
                sqlCommand.Parameters.Add("@Person", SqlDbType.VarChar).Value = Person;
                sqlCommand.Parameters.Add("@Contact", SqlDbType.BigInt).Value = Contact;

                sqlConnect.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnect.Close();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering student: " + ex.Message);
                return false;
            }
        }
        public bool DisplayList()
        {
            try
            {
                string ViewTB_Request = "SELECT UserID, Activity, Date, TimeStart, TimeEnd, PAX, Person, Contact FROM TB_Request";

                sqlAdapter = new SqlDataAdapter(ViewTB_Request, sqlConnect);

                dataTable.Clear();

                sqlAdapter.Fill(dataTable);

                bindingSource.DataSource = dataTable;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying club members: " + ex.Message);
                return false;
            }
        }

    }
}
