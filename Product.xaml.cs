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
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Product : Page
    {
        public Product()
        {
            InitializeComponent();
            LoadProductGrid();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        private void LoadProductGrid()
        {
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT [Product_Code] 'Product Code', [Product_Name] 'Product Name', [Description], [Category], [Location],[Unit], [CP_Unit], [SP_Unit], [Buffer_Quantity] 'Buffer Quantity', [Stock_Quantity] 'Stock Quantity', [Date] 'DOG'  FROM  [tblProduct]";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.CommandType = System.Data.CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataReader sdr = sqlcmd.ExecuteReader();
                dt.Load(sdr);
                dg_Pview.ItemsSource = dt.DefaultView;
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

        private void btnProductAdd_Click(object sender, RoutedEventArgs e)
        {
            ProductForm pdt = new ProductForm();

            pdt.Show();
            
        }

        private void btnProductEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductForm pdt = new ProductForm();
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("Select Product_Code, Product_Name, Description, Category, Vendor, Location, Unit, CP_Unit, SP_Unit, Buffer_Quantity, Stock_Quantity, Date FROM tblProduct WHERE Product_Code ='" + tb_ProductSearch.Text + "'", sqlCon);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    pdt.textbox_Pcode.Text = da.GetValue(0).ToString();
                    pdt.textbox_Pname.Text = da.GetValue(1).ToString();
                    pdt.textbox_Pdesc.Text = da.GetValue(2).ToString();
                    pdt.cb_Pcategory.SelectedItem = da.GetValue(3).ToString();
                    pdt.cb_Pvendor.SelectedItem= da.GetValue(4).ToString();
                    pdt.textbox_Plocation.Text = da.GetValue(5).ToString();
                    pdt.textbox_Punit.Text = da.GetValue(6).ToString(); 
                    pdt.textbox_Pcp.Text = da.GetValue(7).ToString();
                    pdt.textbox_Psp.Text = da.GetValue(8).ToString();
                    pdt.textbox_Pbuffer.Text = da.GetValue(9).ToString();
                    pdt.textbox_Pstock.Text = da.GetValue(10).ToString();
                    pdt.textbox_Pdog.Text = da.GetValue(5).ToString();
                }


                sqlCon.Close();

                pdt.Show();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }





            finally
            {

                sqlCon.Close();
            }

           
        }
    }
}
