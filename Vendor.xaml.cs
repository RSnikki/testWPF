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
using testWPF.Model;

namespace testWPF
{
    /// <summary>
    /// Interaction logic for Vendor.xaml
    /// </summary>
    public partial class Vendor : Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public Vendor()
        {
            InitializeComponent();
            LoadVendorGrid();
        }

        private void LoadVendorGrid()
        {
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT * FROM tblVendor";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.CommandType = System.Data.CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataReader sdr = sqlcmd.ExecuteReader();

                dt.Load(sdr);
                Vendor_DataGrid.ItemsSource = dt.DefaultView;
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

        private void btnVendorAdd_Click(object sender, RoutedEventArgs e)
        {
            VendorForm ven = new VendorForm();

            ven.Show();
        }

        private void btnVendorEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VendorForm ven = new VendorForm();
                sqlCon.Open();
                SqlCommand cmd = new SqlCommand("Select VENDOR_CODE, VENDOR_NAME, ADDRESS, V_CONTACT_NO, EMAIL_ID, DOG FROM tblVendor WHERE VENDOR_CODE ='" + tb_VendorSearch.Text + "'", sqlCon);
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    ven.textbox_Vcode.Text = da.GetValue(0).ToString();
                    ven.textbox_Vname.Text = da.GetValue(1).ToString();
                    ven.textbox_Vaddr.Text = da.GetValue(2).ToString();
                    ven.textbox_Vcontact.Text = da.GetValue(3).ToString();
                    ven.textbox_Vemail.Text = da.GetValue(4).ToString();
                    ven.textbox_Vdate.Text = da.GetValue(5).ToString();
                }


                sqlCon.Close();
          
                ven.Show();
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
