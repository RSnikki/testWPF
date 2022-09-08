using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;

namespace testWPF
{
    /// <summary>
    /// Interaction logic for ProductForm.xaml
    /// </summary>
    public partial class ProductForm : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public ProductForm()
        {
            InitializeComponent();
            loadComboBoxVendor();
        }

        //Function to Load  ComboBox in ProductForm
        private void loadComboBoxVendor()
        {
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT vendor_code, vendor_name FROM tblVendor";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.CommandType = System.Data.CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd);
                
                adapter.Fill(dt);
                cb_Pvendor.ItemsSource = dt.DefaultView;
                cb_Pvendor.DisplayMemberPath = "vendor_name";
                cb_Pvendor.SelectedValuePath = "vendor_code";
                sqlcmd.Dispose();


                string query1 = "SELECT Category_id, Category_Name FROM tblCategory;";
                SqlCommand sqlcmd1 = new SqlCommand(query1, sqlCon);
                sqlcmd.CommandType = System.Data.CommandType.Text;
                DataTable dt1 = new DataTable();
                SqlDataAdapter adapter1 = new SqlDataAdapter(sqlcmd1);

                adapter1.Fill(dt1);
                cb_Pcategory.ItemsSource = dt1.DefaultView;
                cb_Pcategory.DisplayMemberPath = "Category_Name";
                cb_Pcategory.SelectedValuePath = "Category_id";
                sqlcmd1.Dispose();
                sqlCon.Close();
                
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

        public void clearData()
        {
            textbox_Pcode.Clear();
            textbox_Pname.Clear();
            textbox_Pdesc.Clear();
            cb_Pcategory.SelectedItem = null;
            cb_Pvendor.SelectedItem = null;
            textbox_Plocation.Clear();
            textbox_Punit.Clear();
            textbox_Pcp.Clear();
            textbox_Psp.Clear();
            textbox_Pbuffer.Clear();
            textbox_Pstock.Clear();
            textbox_Pdog.Clear();


        }

       

        public bool isValid()
        {
            if(textbox_Pcode.Text == string.Empty)
            {
                MessageBox.Show("Product Code is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_Pname.Text == string.Empty)
            {
                MessageBox.Show("Product Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cb_Pcategory.SelectedItem == null)
              {
                MessageBox.Show("Product Category is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
              }
                if (cb_Pvendor.SelectedItem == null)
                  {
                     MessageBox.Show("Product Vendor is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                     return false;
                  }
            if (textbox_Plocation.Text == string.Empty)
            {
                MessageBox.Show("Product Storage Location is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_Punit.Text == string.Empty)
            {
                MessageBox.Show("Product Measuring Unit is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
               return false;
            }
            if (textbox_Pcp.Text == string.Empty)
            {
                MessageBox.Show("Cost Price per Unit is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_Psp.Text == string.Empty)
            {
                MessageBox.Show("Selling Price per Unit is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_Pbuffer.Text == string.Empty)
            {
                MessageBox.Show("Product Buffer Quantity Unit is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_Pstock.Text == string.Empty)
            {
               MessageBox.Show("Product Stock Quantity Unit is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (textbox_Pdog.Text == string.Empty)
            {
                MessageBox.Show("Product Date Of Generation is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void btn_PFormSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO tblProduct VALUES (@Product_Code, @Product_Name, @Description, @Category, @Vendor, @Location, @Unit, @CP_Unit, @SP_Unit, @Buffer_Quantity, @Stock_Quantity, @Date)", sqlCon);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Product_Code", textbox_Pcode.Text);
                    cmd.Parameters.AddWithValue("@Product_Name", textbox_Pname.Text);
                    cmd.Parameters.AddWithValue("@Description", textbox_Pdesc.Text);
                    cmd.Parameters.AddWithValue("@Category", cb_Pcategory.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Vendor", cb_Pvendor.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Location", textbox_Plocation.Text);
                    cmd.Parameters.AddWithValue("@Unit", textbox_Punit.Text);
                    cmd.Parameters.AddWithValue("@CP_Unit", textbox_Pcp.Text);
                    cmd.Parameters.AddWithValue("@SP_Unit", textbox_Psp.Text);
                    cmd.Parameters.AddWithValue("@Buffer_Quantity", textbox_Pbuffer.Text);
                    cmd.Parameters.AddWithValue("@Stock_Quantity", textbox_Pstock.Text);
                    cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(textbox_Pdog.Text));
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    sqlCon.Close();
                    MessageBox.Show("New Product Successfully Added");
                    clearData();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
           

        }

        private void btn_pFormUpdate_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("UPDATE tblProduct set Product_Name='" + textbox_Pname.Text + "', Description ='" + textbox_Pdesc.Text + "', Category = '"+cb_Pcategory.SelectedValue+"', Vendor = '"+cb_Pvendor.SelectedValue+"', Location = '" + textbox_Plocation.Text + "', Unit = '"+textbox_Punit.Text+"', CP_Unit = '"+textbox_Pcp.Text+"', SP_Unit = '"+textbox_Psp.Text+"', Buffer_Quantity = '"+textbox_Pbuffer.Text+"', Stock_Quantity = '"+textbox_Pstock.Text+"', Date = '"+textbox_Pdog.Text+"'   WHERE Product_Code = '" + textbox_Pcode.Text + "'", sqlCon);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been updated successfully");
            }
            
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                clearData();

            }
            
        }

        private void cb_Pvendor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
