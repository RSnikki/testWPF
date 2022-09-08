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
    /// Interaction logic for ReturnSaleEntry.xaml
    /// </summary>
    public partial class ReturnSaleEntry : Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public ReturnSaleEntry()
        {
            InitializeComponent();
        }

        private void btnRSEAdd_Click(object sender, RoutedEventArgs e)
        {
            RSEform rbef = new RSEform();
            rbef.Show();



        }

        private void btnRSEEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRSEView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT ReturnSale_Id 'Return Sale No', RS_ID, Sale_Id 'Sale No', Product_Code, Quantity_Return, Return_DOE FROM tblRSBill WHERE ReturnSale_Id='" + tb_RSEsearch.Text + "'";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.CommandType = System.Data.CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataReader sdr = sqlcmd.ExecuteReader();

                dt.Load(sdr);
                dg_RSEview.ItemsSource = dt.DefaultView;
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
    }
}
