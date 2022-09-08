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
    /// Interaction logic for Inventory.xaml
    /// </summary>
    public partial class Inventory : Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public Inventory()
        {
            InitializeComponent();
        }

        private void btn_View_Click(object sender, RoutedEventArgs e)
        {
            if (rb_1.IsChecked == true)
            {
                try
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                        sqlCon.Open();

                    string query = "SELECT [Product_Code],[Product_Name], [IMDB].[dbo].[tblVendor].[VENDOR_NAME], [Buffer_Quantity],[Stock_Quantity] FROM [IMDB].[dbo].[tblProduct] INNER JOIN[IMDB].[dbo].[tblVendor] ON[IMDB].[dbo].[tblProduct].Vendor = [IMDB].[dbo].[tblVendor].VENDOR_CODE WHERE[IMDB].[dbo].[tblProduct].Stock_Quantity < [IMDB].[dbo].[tblProduct].Buffer_Quantity";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    DataTable dt = new DataTable();
                    SqlDataReader sdr = sqlcmd.ExecuteReader();

                    dt.Load(sdr);
                    dg_Iview.ItemsSource = dt.DefaultView;
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


            if (rb_2.IsChecked == true)
            {
                try
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                        sqlCon.Open();


                    string query = "SELECT [Product_Code],[Product_Name], [Description], [Category], [IMDB].[dbo].[tblVendor].[VENDOR_NAME], [Location], [SP_Unit], [Stock_Quantity] FROM [IMDB].[dbo].[tblProduct] INNER JOIN[IMDB].[dbo].[tblVendor] ON[IMDB].[dbo].[tblProduct].Vendor = [IMDB].[dbo].[tblVendor].VENDOR_CODE WHERE[IMDB].[dbo].[tblProduct].Product_Code = '"+tb1_Psearch.Text+"'";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    DataTable dt = new DataTable();
                    SqlDataReader sdr = sqlcmd.ExecuteReader();

                    dt.Load(sdr);
                    dg_Iview.ItemsSource = dt.DefaultView;
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


            if (rb_3.IsChecked == true)
            {
                dg_Iview.ItemsSource = null;
             
                try
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                        sqlCon.Open();
                    

                    string query = "SELECT 'Purchase' AS TYPE, [Invoice_Id] 'Number', [Product_Code], [Quantity_Received] 'Quantity', [Purchase_DOE] 'Date' FROM[IMDB].[dbo].[tblPurchaseBill] WHERE [Product_Code] = '" + tb2_Psearch.Text + "' UNION SELECT 'Sale' AS TYPE, [Sale_Id] 'Number', [Product_Code], [Quantity_Sold] 'Quantity', [Sale_DOE] 'Date' FROM [IMDB].[dbo].[tblSaleBill] WHERE [Product_Code] = '" + tb2_Psearch.Text + "' UNION SELECT 'Return' AS TYPE, [ReturnSale_Id] 'Number', [Product_Code], [Quantity_Return] 'Quantity', [Return_DOE] 'Date' FROM [IMDB].[dbo].[tblRSBill] WHERE [Product_Code] = '" + tb2_Psearch.Text + "' ORDER BY Date";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    DataTable dt = new DataTable();
                    SqlDataReader sdr = sqlcmd.ExecuteReader();

                    dt.Load(sdr);
                    dg_Iview.ItemsSource = dt.DefaultView;
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


        private void rb_1_Checked(object sender, RoutedEventArgs e)
        {
            rb_1.Background = Brushes.Yellow;
            rb_1.Foreground = Brushes.Navy;
            rb_2.Background = Brushes.White;
            rb_2.Foreground = Brushes.Black;
            rb_3.Background = Brushes.White;
            rb_3.Foreground = Brushes.Black;
            tb1_Psearch.Clear();
            tb2_Psearch.Clear();
        }

        private void rb_2_Checked(object sender, RoutedEventArgs e)
        {
            rb_1.Background = Brushes.White;
            rb_1.Foreground = Brushes.Black;
            rb_2.Background = Brushes.Yellow;
            rb_2.Foreground = Brushes.Navy;
            rb_3.Background = Brushes.White;
            rb_3.Foreground = Brushes.Black;
            tb2_Psearch.Clear();
        }

        private void rb_3_Checked(object sender, RoutedEventArgs e)
        {
            rb_1.Background = Brushes.White;
            rb_1.Foreground = Brushes.Black;
            rb_2.Background = Brushes.White;
            rb_2.Foreground = Brushes.Black;
            rb_3.Background = Brushes.Yellow;
            rb_3.Foreground = Brushes.Navy;
            tb1_Psearch.Clear();
        }

        private void dg_Iview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tb2_Psearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
