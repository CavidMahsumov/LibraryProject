using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable table;
        SqlConnection conn;
        string cs = "";
        SqlDataReader reader;
        DataSet dataSet;
        SqlDataAdapter dataadapter;
        DataRowView DataRowView;
        string ProductName;
        string ProductId;
        string price;
        string Description;
        int count;
        int count1;
        string customerid;
        public MainWindow()
        {
            InitializeComponent();
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlTransaction transaction = null;

            using (conn = new SqlConnection())
            {
                conn.ConnectionString = cs;
                conn.Open();
                transaction = conn.BeginTransaction();
                ++count;
                SqlCommand command = new SqlCommand("sp_CheckStudent", conn);
                command.CommandType = CommandType.StoredProcedure;

               

                var param1 = new SqlParameter();
                param1.Value = FirstNametxtBox.Text.ToString();
                param1.ParameterName = "@Firstname";
                param1.SqlDbType = SqlDbType.NVarChar;
                command.Parameters.Add(param1);


                var param2 = new SqlParameter();
                param2.Value = passwordtxtbox.Text;
                param2.ParameterName = "@Password";
                param2.SqlDbType = SqlDbType.NVarChar;
                command.Parameters.Add(param2);
                command.Transaction = transaction;


                try
                {
                    command.ExecuteNonQuery();
                    transaction.Commit();
                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    conn.Close();
              
                }
            

            }
        }
    }
}
