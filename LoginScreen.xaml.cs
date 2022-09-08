using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
//using testWPF.Ibal;
using testWPF.Model;

namespace testWPF
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window//, ILoginCred
    {
      // private ILoginCred LoginCred { get; set; }
        //public LoginScreen(ILoginCred LoginCred)
        //{
        //    this.LoginCred = LoginCred;
        //}
        public LoginScreen()
        {
            InitializeComponent();

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //if (txtUsername.Text != "" && txtPassword.Password != "")
            //{
            //    loginCredential obj = new loginCredential();
            //    obj.Username = txtUsername.Text;
            //    obj.Password = txtPassword.Password.ToString();
                
            //    bool IsAuthenticated=LoginCred.Authenticate(obj);
            //}
            SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT COUNT(1) FROM [dbo].[tblUser] where Username=@Username and password=@Password";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.CommandType = System.Data.CommandType.Text;
                sqlcmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                sqlcmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                int count = Convert.ToInt32(sqlcmd.ExecuteScalar());

                if (count == 1)
                {
                    MainWindow dashboard = new MainWindow();
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username Or Password is incorrect;");
                    txtUsername.Clear();
                    txtPassword.Clear();
                    btnSubmit.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnSubmit.IsEnabled = false;
            }
            finally
            {
                sqlCon.Close();
            }
        }

    

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if(txtUsername.Text!="" && txtPassword.Password != "")
            {
                btnSubmit.IsEnabled = true;
            }
            else
                btnSubmit.IsEnabled = false;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Password != "")
            {
                btnSubmit.IsEnabled = true;
            }
            else
                btnSubmit.IsEnabled = false;
        }
    }
}
