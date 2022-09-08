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
using System.Windows.Shapes;

namespace testWPF
{
    /// <summary>
    /// Interaction logic for PBEForm.xaml
    /// </summary>
    public partial class PBEForm : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        bool LoadVendor = true;
        public PBEForm()
        {
            InitializeComponent();
            if (LoadVendor)
                loadComboBoxVendor();
        }

        //FUNCTION TO LOAD VENDOR COMBOBOX
        private void loadComboBoxVendor()
        {

            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();

            string query = "SELECT vendor_code, vendor_name FROM tblVendor";
            SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
            sqlcmd.CommandType = System.Data.CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd);

            adapter.Fill(dt);
            cb_PBEvendor.ItemsSource = dt.DefaultView;
            cb_PBEvendor.DisplayMemberPath = "vendor_name";
            cb_PBEvendor.SelectedValuePath = "vendor_code";
            sqlcmd.Dispose();

            sqlCon.Close();

        }

        //FUNCTION TO CLEAR TEXTBOXES AND UNSELECT COMBOBOX
        public void clearData()
        {
            LoadVendor = false;
            tb_PBEinvoice.Clear();
            cb_PBEvendor.SelectedItem = null;
            tb_PBEdate.Clear();

            cb_PBEpc1.SelectedItem = null;
            cb_PBEpc2.SelectedItem = null;
            cb_PBEpc3.SelectedItem = null;
            cb_PBEpc4.SelectedItem = null;
            cb_PBEpc5.SelectedItem = null;

            tb_PBEpn1.Clear();
            tb_PBEpn2.Clear();
            tb_PBEpn3.Clear();
            tb_PBEpn4.Clear();
            tb_PBEpn5.Clear();

            tb_PBEcp1.Clear();
            tb_PBEcp2.Clear();
            tb_PBEcp3.Clear();
            tb_PBEcp4.Clear();
            tb_PBEcp5.Clear();

            tb_PBEq1.Clear();
            tb_PBEq2.Clear();
            tb_PBEq3.Clear();
            tb_PBEq4.Clear();
            tb_PBEq5.Clear();

            tb_PBEp1.Clear();
            tb_PBEp2.Clear();
            tb_PBEp3.Clear();
            tb_PBEp4.Clear();
            tb_PBEp5.Clear();

            tb_PBEtp.Clear();

        }

        //FUNCTION TO CHECK BEFORE ADDING DATA TO DATABASE
        public bool isValid()
        {
            if (tb_PBEinvoice.Text == string.Empty)
            {
                MessageBox.Show("Invoice No. is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cb_PBEvendor.SelectedItem == null)
            {
                MessageBox.Show("Vendor is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (tb_PBEdate.Text == string.Empty)
            {
                MessageBox.Show("Date is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
           

            return true;
        }

        //ADD CLICK EVENT FUNCTION TO ADD DATA TO tblPurchaseBill TABLE
        private void btn_AddPB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                if (isValid())
                {
                    bool k = false;
                    if (tb_PBEq1.Text != null && tb_PBEq1.Text != "")
                    {

                        SqlCommand cmd = new SqlCommand("INSERT INTO tblPurchaseBill VALUES (@Invoice_Id, @Vendor_Code, @Product_Code, @Quantity_Received, @Purchase_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Invoice_Id", tb_PBEinvoice.Text);
                        cmd.Parameters.AddWithValue("@Vendor_Code", cb_PBEvendor.SelectedValue);
                        cmd.Parameters.AddWithValue("@Product_Code", cb_PBEpc1.SelectedValue);
                        cmd.Parameters.AddWithValue("@Quantity_Received", Convert.ToInt32(tb_PBEq1.Text));
                        cmd.Parameters.AddWithValue("@Purchase_DOE", Convert.ToDateTime(tb_PBEdate.Text));


                        cmd.ExecuteNonQuery();

                        int Stock_Quantity =Convert.ToInt32(((System.Data.DataRowView)(cb_PBEpc1.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity + Convert.ToInt32(tb_PBEq1.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='"+ cb_PBEpc1.SelectedValue+"';", sqlCon);
                        cmd1.CommandType = CommandType.Text;              


                        cmd1.ExecuteNonQuery();


                        //  SqlCommand cmd2 = new SqlCommand("INSERT INTO tblPurchaseBilltotal VALUES (@Invoice_Id, @total_bill)", sqlCon);
                        //  cmd2.CommandType = CommandType.Text;
                        //  cmd2.Parameters.AddWithValue("@Invoice_Id", tb_PBEinvoice.Text);
                        //  cmd2.Parameters.AddWithValue("@total_bill", tb_PBEtp.Text);



                        //  cmd2.ExecuteNonQuery();

                        k = true;
                    }

                    if (tb_PBEq2.Text != null && tb_PBEq2.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblPurchaseBill VALUES (@Invoice_Id, @Vendor_Code, @Product_Code, @Quantity_Received, @Purchase_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Invoice_Id", tb_PBEinvoice.Text);
                        cmd.Parameters.AddWithValue("@Vendor_Code", cb_PBEvendor.SelectedValue);
                        cmd.Parameters.AddWithValue("@Product_Code", cb_PBEpc2.SelectedValue);
                        cmd.Parameters.AddWithValue("@Quantity_Received", tb_PBEq2.Text);
                        cmd.Parameters.AddWithValue("@Purchase_DOE", Convert.ToDateTime(tb_PBEdate.Text));


                        cmd.ExecuteNonQuery();

                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_PBEpc2.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity + Convert.ToInt32(tb_PBEq2.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + cb_PBEpc2.SelectedValue + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;
                    }
                    if (tb_PBEq3.Text != null && tb_PBEq3.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblPurchaseBill VALUES (@Invoice_Id, @Vendor_Code, @Product_Code, @Quantity_Received, @Purchase_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Invoice_Id", tb_PBEinvoice.Text);
                        cmd.Parameters.AddWithValue("@Vendor_Code", cb_PBEvendor.SelectedValue);
                        cmd.Parameters.AddWithValue("@Product_Code", cb_PBEpc3.SelectedValue);
                        cmd.Parameters.AddWithValue("@Quantity_Received", tb_PBEq3.Text);
                        cmd.Parameters.AddWithValue("@Purchase_DOE", Convert.ToDateTime(tb_PBEdate.Text));


                        cmd.ExecuteNonQuery();

                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_PBEpc3.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity + Convert.ToInt32(tb_PBEq3.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + cb_PBEpc3.SelectedValue + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;
                    }
                    if (tb_PBEq4.Text != null && tb_PBEq4.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblPurchaseBill VALUES (@Invoice_Id, @Vendor_Code, @Product_Code, @Quantity_Received, @Purchase_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Invoice_Id", tb_PBEinvoice.Text);
                        cmd.Parameters.AddWithValue("@Vendor_Code", cb_PBEvendor.SelectedValue);
                        cmd.Parameters.AddWithValue("@Product_Code", cb_PBEpc4.SelectedValue);
                        cmd.Parameters.AddWithValue("@Quantity_Received", tb_PBEq4.Text);
                        cmd.Parameters.AddWithValue("@Purchase_DOE", Convert.ToDateTime(tb_PBEdate.Text));


                        cmd.ExecuteNonQuery();

                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_PBEpc4.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity + Convert.ToInt32(tb_PBEq4.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + cb_PBEpc4.SelectedValue + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;
                    }
                    if (tb_PBEq5.Text != null && tb_PBEq5.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblPurchaseBill VALUES (@Invoice_Id, @Vendor_Code, @Product_Code, @Quantity_Received, @Purchase_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Invoice_Id", tb_PBEinvoice.Text);
                        cmd.Parameters.AddWithValue("@Vendor_Code", cb_PBEvendor.SelectedValue);
                        cmd.Parameters.AddWithValue("@Product_Code", cb_PBEpc5.SelectedValue);
                        cmd.Parameters.AddWithValue("@Quantity_Received", tb_PBEq5.Text);
                        cmd.Parameters.AddWithValue("@Purchase_DOE", Convert.ToDateTime(tb_PBEdate.Text));


                        cmd.ExecuteNonQuery();

                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_PBEpc5.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity + Convert.ToInt32(tb_PBEq5.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + cb_PBEpc5.SelectedValue + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;

                        k = true;
                    }

                    if (k == true)
                    {
                        MessageBox.Show("Purchase Bill Entry Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Some Field Missing. Entry Unsuccessful.");
                    }
                    
                    
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

        private void cb_PBEvendor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadComboBoxProduct();
        }

        //FUNCTION TO LOAD PRODUCT COMBOBOX
        private void loadComboBoxProduct()
        {
            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();
            string query = "select [Product_Name],[Product_Code],[CP_Unit],[Stock_Quantity] from [IMDB].[dbo].[tblProduct] where [Vendor]='" + cb_PBEvendor.SelectedValue.ToString() + "'";
            SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
            sqlcmd.CommandType = System.Data.CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd);

            adapter.Fill(dt);
            cb_PBEpc1.ItemsSource = dt.DefaultView;
            cb_PBEpc1.DisplayMemberPath = "Product_Code";
            cb_PBEpc1.SelectedValuePath = "Product_Code";
            cb_PBEpc2.ItemsSource = dt.DefaultView;
            cb_PBEpc2.DisplayMemberPath = "Product_Code";
            cb_PBEpc2.SelectedValuePath = "Product_Code";
            cb_PBEpc3.ItemsSource = dt.DefaultView;
            cb_PBEpc3.DisplayMemberPath = "Product_Code";
            cb_PBEpc3.SelectedValuePath = "Product_Code";
            cb_PBEpc4.ItemsSource = dt.DefaultView;
            cb_PBEpc4.DisplayMemberPath = "Product_Code";
            cb_PBEpc4.SelectedValuePath = "Product_Code";
            cb_PBEpc5.ItemsSource = dt.DefaultView;
            cb_PBEpc5.DisplayMemberPath = "Product_Code";
            cb_PBEpc5.SelectedValuePath = "Product_Code";

            sqlcmd.Dispose();

            sqlCon.Close();
        }

        private void cb_PBEpc1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tb_PBEcp1.Text = ((System.Data.DataRowView)(cb_PBEpc1.SelectedItem))["CP_Unit"].ToString();
                tb_PBEpn1.Text = ((System.Data.DataRowView)(cb_PBEpc1.SelectedItem))["Product_Name"].ToString();

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

        private void cb_PBEpc2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tb_PBEcp2.Text = ((System.Data.DataRowView)(cb_PBEpc2.SelectedItem))["CP_Unit"].ToString();
                tb_PBEpn2.Text = ((System.Data.DataRowView)(cb_PBEpc2.SelectedItem))["Product_Name"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }


            finally
            {

                //sqlCon.Close();
            }

        }

        private void cb_PBEpc3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tb_PBEcp3.Text = ((System.Data.DataRowView)(cb_PBEpc3.SelectedItem))["CP_Unit"].ToString();
                tb_PBEpn3.Text = ((System.Data.DataRowView)(cb_PBEpc3.SelectedItem))["Product_Name"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }


            finally
            {

                //sqlCon.Close();
            }

        }

        private void cb_PBEpc4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tb_PBEcp4.Text = ((System.Data.DataRowView)(cb_PBEpc4.SelectedItem))["CP_Unit"].ToString();
                tb_PBEpn4.Text = ((System.Data.DataRowView)(cb_PBEpc4.SelectedItem))["Product_Name"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }


            finally
            {

                //sqlCon.Close();
            }

        }

        private void cb_PBEpc5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tb_PBEcp5.Text = ((System.Data.DataRowView)(cb_PBEpc5.SelectedItem))["CP_Unit"].ToString();
                tb_PBEpn5.Text = ((System.Data.DataRowView)(cb_PBEpc5.SelectedItem))["Product_Name"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }


            finally
            {

                //sqlCon.Close();
            }

        }

        private void tb_PBEq1_LostFocus(object sender, RoutedEventArgs e)
        {
            double Cost_price = Convert.ToDouble(tb_PBEcp1.Text);
            int Quatity = Convert.ToInt32(tb_PBEq1.Text);
            tb_PBEp1.Text = (Quatity * Cost_price).ToString();
            if (tb_PBEp1.Text != null)
                tb_PBEtp.Text = tb_PBEp1.Text;
        }

        private void tb_PBEq2_LostFocus(object sender, RoutedEventArgs e)
        {
            double Cost_price = Convert.ToDouble(tb_PBEcp2.Text);
            int Quatity = Convert.ToInt32(tb_PBEq2.Text);
            tb_PBEp2.Text = (Quatity * Cost_price).ToString();
            if (tb_PBEp2.Text != null && tb_PBEp1.Text != null)
            {
                double total1 = Convert.ToDouble(tb_PBEp1.Text);
                int total2 = (int)Convert.ToDouble(tb_PBEp2.Text);

                tb_PBEtp.Text = (total1 + total2).ToString();
            }

        }

        private void tb_PBEq3_LostFocus(object sender, RoutedEventArgs e)
        {
            double Cost_price = Convert.ToDouble(tb_PBEcp3.Text);
            int Quatity = Convert.ToInt32(tb_PBEq3.Text);
            tb_PBEp3.Text = (Quatity * Cost_price).ToString();
            if (tb_PBEp2.Text != null && tb_PBEp1.Text != null && tb_PBEp3.Text != null)
            {
                double total1 = Convert.ToDouble(tb_PBEp1.Text);
                double total2 = Convert.ToDouble(tb_PBEp2.Text);
                double total3 = Convert.ToDouble(tb_PBEp3.Text);

                tb_PBEtp.Text = (total1 + total2 + total3).ToString();
            }
            //tb_PBEtp.Text = (Convert.ToDouble(tb_PBEp1.Text) + Convert.ToDouble(tb_PBEp2.Text) + Convert.ToDouble(tb_PBEp3.Text)).ToString();
        }

        private void tb_PBEq4_LostFocus(object sender, RoutedEventArgs e)
        {
            double Cost_price = Convert.ToDouble(tb_PBEcp4.Text);
            int Quatity = Convert.ToInt32(tb_PBEq4.Text);
            tb_PBEp4.Text = (Quatity * Cost_price).ToString();
            if (tb_PBEp2.Text != null && tb_PBEp1.Text != null && tb_PBEp3.Text != null && tb_PBEp4.Text != null)
                tb_PBEtp.Text = (Convert.ToDouble(tb_PBEp1.Text) + Convert.ToDouble(tb_PBEp2.Text) + Convert.ToDouble(tb_PBEp3.Text) + Convert.ToDouble(tb_PBEp4.Text)).ToString();


        }

        private void tb_PBEq5_LostFocus(object sender, RoutedEventArgs e)
        {
            double Cost_price = Convert.ToDouble(tb_PBEcp5.Text);
            int Quatity = Convert.ToInt32(tb_PBEq5.Text);
            tb_PBEp5.Text = (Quatity * Cost_price).ToString();
            if (tb_PBEp2.Text != null && tb_PBEp1.Text != null && tb_PBEp3.Text != null && tb_PBEp4.Text != null && tb_PBEp5.Text != null)
                tb_PBEtp.Text = (Convert.ToDouble(tb_PBEp1.Text) + Convert.ToDouble(tb_PBEp2.Text)
                    + Convert.ToDouble(tb_PBEp3.Text) + Convert.ToDouble(tb_PBEp4.Text) + Convert.ToDouble(tb_PBEp5.Text)).ToString();

        }
    }

}
