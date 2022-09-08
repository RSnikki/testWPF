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
    /// Interaction logic for PurchaseBillEntry.xaml
    /// </summary>
    public partial class PurchaseBillEntry : Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public PurchaseBillEntry()
        {
            InitializeComponent();
        }

        //TO OPEN PBEForm WINDOW
        private void btnPBEAdd_Click(object sender, RoutedEventArgs e)
        {
            PBEForm pbef = new PBEForm();
            pbef.Show();
        }

        
        //TO LOAD DATAGRID WITH TABLE DATA
        private void btnPBEView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT Invoice_Id 'Invoice No', PB_ID, Vendor_Code, Product_Code, Quantity_Received, Purchase_DOE FROM tblPurchaseBill WHERE Invoice_Id='" + tb_PBEsearch.Text + "'";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.CommandType = System.Data.CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataReader sdr = sqlcmd.ExecuteReader();

                dt.Load(sdr);
                dg_PBEview.ItemsSource = dt.DefaultView;
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
