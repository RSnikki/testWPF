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
    /// Interaction logic for SaleAcceptanceEntry.xaml
    /// </summary>
    public partial class SaleAcceptanceEntry : Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public SaleAcceptanceEntry()
        {
            InitializeComponent();
        }

        //TO OPEN SAEForm WINDOW ON CLICKING ADD BUTTON
        private void btnSAEAdd_Click(object sender, RoutedEventArgs e)
        {
            SAEform sbef = new SAEform();
            sbef.Show();

        }

        
        //TO LOAD DATAGRID WITH DATA TABLE
        private void btnSAEView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT Sale_Id 'Sale No', SB_ID, Customer_Name, Customer_Contact, Product_Code, Quantity_Sold, Sale_DOE FROM tblSaleBill WHERE Sale_Id='" + tb_SAEsearch.Text + "'";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.CommandType = System.Data.CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataReader sdr = sqlcmd.ExecuteReader();

                dt.Load(sdr);
                dg_SAEview.ItemsSource = dt.DefaultView;
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
