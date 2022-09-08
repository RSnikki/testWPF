using System;
using System.Collections.Generic;
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

namespace testWPF
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList : Page
    {
        public UserList()
        {
            InitializeComponent();
            LoadUserGrid();
        }

        
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
       
        public void clearData()
        {
            txtNewUserName.Clear();
            PwdUser.Clear();
            textbox_UsrSearch.Clear();
        }
        
        //FOR SHOWING TABLE(tblUser) DATA ON DATAGRID(DataGrid_User) ON PAGE(UserList)
        private void LoadUserGrid()
        {
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT * FROM tblUser";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.CommandType = System.Data.CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataReader sdr = sqlcmd.ExecuteReader();
                dt.Load(sdr);
                DataGrid_User.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
        
        //CHECK FOR USERNAME AND PASSWORD TEXTBOX TO BE FILLED BEFORE ADDING NEW USER
        public bool isValid()
        {
            if(txtNewUserName.Text == string.Empty)
            {
                MessageBox.Show("UserName is Required", "Failed");
                return false;
            }
            if (PwdUser.Text == string.Empty)
            {
                MessageBox.Show("Password is Required", "Failed");
                return false;
            }
            return true;
        }

        //FUNCTIONALITY FOR BUTTON(ADD USER)
        private void btnUserSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "SELECT count(1) FROM tblUser where Username= @NewUserName;";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@NewUserName", txtNewUserName.Text);

                    int count = (int)sqlcmd.ExecuteScalar();
                    if (count == 0)
                    {
                        string query1 = "INSERT INTO tblUser ( Username, password) VALUES(@NewUserName,@password); ";
                        SqlCommand sqlcmd1 = new SqlCommand(query1, sqlCon);
                        sqlcmd1.CommandType = System.Data.CommandType.Text;
                        sqlcmd1.Parameters.AddWithValue("@NewUserName", txtNewUserName.Text);
                        sqlcmd1.Parameters.AddWithValue("@Password", PwdUser.Text);
                        DataTable dt = new DataTable();
                        sqlcmd1.ExecuteNonQuery();
                        LoadUserGrid();
                        MessageBox.Show("New User is Added Successfully.");
                    }
                    else
                    { 
                        MessageBox.Show("Username is already there.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

       
        /// <summary>
        /// FUNCTIONALITY FOR BUTTON(UPDATE)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_UsrUpdate_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            
            SqlCommand cmd = new SqlCommand("UPDATE tblUser set Username='"+txtNewUserName.Text+"', password ='"+PwdUser.Text+"' WHERE Username='"+textbox_UsrSearch.Text+"'", sqlCon );
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been updated successfully");    
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                clearData();
                LoadUserGrid();
            }
        }

        ////FUNCTIONALITY FOR BUTTON(DELETE)
        private void btn_UsrDel_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM tblUser WHERE Username='" + textbox_UsrSearch.Text + "'", sqlCon);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted successfully");
                clearData();
                LoadUserGrid();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted"+ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }

}
