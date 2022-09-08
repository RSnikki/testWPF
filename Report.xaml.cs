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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public Report()
        {
            InitializeComponent();
        }

        private void rb_r1_Checked(object sender, RoutedEventArgs e)
        {
            rb_r1.Background = Brushes.Yellow;
            rb_r1.Foreground = Brushes.Navy;
            rb_r2.Background = Brushes.White;
            rb_r2.Foreground = Brushes.Black;
            rb_r3.Background = Brushes.White;
            rb_r3.Foreground = Brushes.Black;
            tb_Sdate2.Clear();
            tb_Edate2.Clear();
            tb_Sdate3.Clear();
            tb_Edate3.Clear();
        }

        private void rb_r2_Checked(object sender, RoutedEventArgs e)
        {
            rb_r1.Background = Brushes.White;
            rb_r1.Foreground = Brushes.Black;
            rb_r2.Background = Brushes.Yellow;
            rb_r2.Foreground = Brushes.Navy;
            rb_r3.Background = Brushes.White;
            rb_r3.Foreground = Brushes.Black;
            tb_Sdate1.Clear();
            tb_Edate1.Clear();
            tb_Sdate3.Clear();
            tb_Edate3.Clear();


        }

        private void rb_r3_Checked(object sender, RoutedEventArgs e)
        {
            rb_r1.Background = Brushes.White;
            rb_r1.Foreground = Brushes.Black;
            rb_r2.Background = Brushes.White;
            rb_r2.Foreground = Brushes.Black;
            rb_r3.Background = Brushes.Yellow;
            rb_r3.Foreground = Brushes.Navy;
            tb_Sdate1.Clear();
            tb_Edate1.Clear();
            tb_Sdate2.Clear();
            tb_Edate2.Clear();

        }

        private void btn_Rview_Click(object sender, RoutedEventArgs e)
        {
            if (rb_r1.IsChecked == true)
            {
                try
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                        sqlCon.Open();

                    string query = "SELECT [PB_ID], [Invoice_Id] 'Invoice No', [Vendor_Code], [IMDB].[dbo].[tblProduct].[Product_Name] , [IMDB].[dbo].[tblProduct].[Product_Code], [Quantity_Received], [Purchase_DOE] FROM [IMDB].[dbo].[tblPurchaseBill] INNER JOIN [IMDB].[dbo].[tblProduct] ON [IMDB].[dbo].[tblPurchaseBill].[Product_Code]=[IMDB].[dbo].[tblProduct].[Product_Code]  WHERE [Purchase_DOE] BETWEEN '" + tb_Sdate1.Text + "' AND '" + tb_Edate1.Text + "' ";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    DataTable dt = new DataTable();
                    SqlDataReader sdr = sqlcmd.ExecuteReader();

                    dt.Load(sdr);
                    dg_Rview.ItemsSource = dt.DefaultView;
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


            if (rb_r2.IsChecked == true)
            {
                try
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                        sqlCon.Open();


                    string query = "SELECT [SB_ID], [Sale_Id] 'Sale No', [Customer_Name], [Customer_Contact], [IMDB].[dbo].[tblProduct].[Product_Name] , [IMDB].[dbo].[tblProduct].[Product_Code], [Quantity_Sold], [Sale_DOE] FROM [IMDB].[dbo].[tblSaleBill] INNER JOIN [IMDB].[dbo].[tblProduct] ON [IMDB].[dbo].[tblSaleBill].[Product_Code]=[IMDB].[dbo].[tblProduct].[Product_Code]  WHERE [Sale_DOE] BETWEEN '"+ tb_Sdate2.Text +"' AND '"+ tb_Edate2.Text +"' ";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    DataTable dt = new DataTable();
                    SqlDataReader sdr = sqlcmd.ExecuteReader();

                    dt.Load(sdr);
                    dg_Rview.ItemsSource = dt.DefaultView;
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


            if (rb_r3.IsChecked == true)
            {

                try
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                        sqlCon.Open();


                    string query = "SELECT [RS_ID], [ReturnSale_Id] 'Return Sale No',[Sale_Id] 'Sale No', [IMDB].[dbo].[tblProduct].[Product_Name] , [IMDB].[dbo].[tblProduct].[Product_Code], [Quantity_Return], [Return_DOE] FROM [IMDB].[dbo].[tblRSBill] INNER JOIN [IMDB].[dbo].[tblProduct] ON [IMDB].[dbo].[tblRSBill].[Product_Code]=[IMDB].[dbo].[tblProduct].[Product_Code]  WHERE [Return_DOE] BETWEEN '" + tb_Sdate3.Text + "' AND '" + tb_Edate3.Text+ "'  ";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    DataTable dt = new DataTable();
                    SqlDataReader sdr = sqlcmd.ExecuteReader();

                    dt.Load(sdr);
                    dg_Rview.ItemsSource = dt.DefaultView;
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
}
