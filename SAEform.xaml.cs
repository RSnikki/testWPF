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
    /// Interaction logic for SAEform.xaml
    /// </summary>
    public partial class SAEform : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public SAEform()
        {
            InitializeComponent();
            loadComboBoxProduct();
        }

        private void loadComboBoxProduct()
        {
            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();
            string query = "SELECT Product_Code, Product_Name, SP_Unit, Stock_Quantity FROM tblProduct";
            SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
            sqlcmd.CommandType = System.Data.CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd);

            adapter.Fill(dt);
            cb_SAEpn1.ItemsSource = dt.DefaultView;
            cb_SAEpn1.DisplayMemberPath = "Product_Name";
            cb_SAEpn1.SelectedValuePath = "Product_Code";
            cb_SAEpn2.ItemsSource = dt.DefaultView;
            cb_SAEpn2.DisplayMemberPath = "Product_Name";
            cb_SAEpn2.SelectedValuePath = "Product_Code";
            cb_SAEpn3.ItemsSource = dt.DefaultView;
            cb_SAEpn3.DisplayMemberPath = "Product_Name";
            cb_SAEpn3.SelectedValuePath = "Product_Code";
            cb_SAEpn4.ItemsSource = dt.DefaultView;
            cb_SAEpn4.DisplayMemberPath = "Product_Name";
            cb_SAEpn4.SelectedValuePath = "Product_Code";
            cb_SAEpn5.ItemsSource = dt.DefaultView;
            cb_SAEpn5.DisplayMemberPath = "Product_Name";
            cb_SAEpn5.SelectedValuePath = "Product_Code";
            sqlcmd.Dispose();

            sqlCon.Close();


        }

        public void clearData()
        {
            tb_SAEbill.Clear();
            tb_SAEcname.Clear();
            tb_SAEcno.Clear();
            tb_SAEdate.Clear();

            cb_SAEpn1.SelectedItem = null;
            cb_SAEpn2.SelectedItem = null;
            cb_SAEpn3.SelectedItem = null;
            cb_SAEpn4.SelectedItem = null;
            cb_SAEpn5.SelectedItem = null;

            tb_SAEpc1.Clear();
            tb_SAEpc2.Clear();
            tb_SAEpc3.Clear();
            tb_SAEpc4.Clear();
            tb_SAEpc5.Clear();

            tb_SAEsp1.Clear();
            tb_SAEsp2.Clear();
            tb_SAEsp3.Clear();
            tb_SAEsp4.Clear();
            tb_SAEsp5.Clear();

            tb_SAEq1.Clear();
            tb_SAEq2.Clear();
            tb_SAEq3.Clear();
            tb_SAEq4.Clear();
            tb_SAEq5.Clear();

            tb_SAEp1.Clear();
            tb_SAEp2.Clear();
            tb_SAEp3.Clear();
            tb_SAEp4.Clear();
            tb_SAEp5.Clear();

            tb_SAEtp.Clear();

        }

        public bool isValid()
        {
            if (tb_SAEbill.Text == string.Empty)
            {
                MessageBox.Show("Bill No. is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (tb_SAEcname.Text == string.Empty)
            {
                MessageBox.Show("Customer Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (tb_SAEcno.Text == string.Empty)
            {
                MessageBox.Show("Customer Contact No. is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (tb_SAEdate.Text == string.Empty)
            {
                MessageBox.Show("Date is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            

            return true;
        }



        private void btn_AddSB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlCon.Open();

                if (isValid())
                {
                    bool k = false;
                    if (tb_SAEq1.Text != null && tb_SAEq1.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblSaleBill VALUES (@Sale_Id, @Customer_Name, @Customer_Contact, @Product_Code ,@Quantity_Sold, @Sale_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Sale_Id", tb_SAEbill.Text);
                        cmd.Parameters.AddWithValue("@Customer_Name", tb_SAEcname.Text);
                        cmd.Parameters.AddWithValue("@Customer_Contact", tb_SAEcno.Text);
                        cmd.Parameters.AddWithValue("@Product_Code", tb_SAEpc1.Text);
                        cmd.Parameters.AddWithValue("@Quantity_Sold", Convert.ToInt32(tb_SAEq1.Text));
                        cmd.Parameters.AddWithValue("@Sale_DOE", Convert.ToDateTime(tb_SAEdate.Text));


                        cmd.ExecuteNonQuery();

                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_SAEpn1.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity - Convert.ToInt32(tb_SAEq1.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + cb_SAEpn1.SelectedValue + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;
                    }

                    if(tb_SAEq2.Text != null && tb_SAEq2.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblSaleBill VALUES (@Sale_Id, @Customer_Name, @Customer_Contact, @Product_Code ,@Quantity_Sold, @Sale_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Sale_Id", tb_SAEbill.Text);
                        cmd.Parameters.AddWithValue("@Customer_Name", tb_SAEcname.Text);
                        cmd.Parameters.AddWithValue("@Customer_Contact", tb_SAEcno.Text);
                        cmd.Parameters.AddWithValue("@Product_Code", tb_SAEpc2.Text);
                        cmd.Parameters.AddWithValue("@Quantity_Sold", Convert.ToInt32(tb_SAEq2.Text));
                        cmd.Parameters.AddWithValue("@Sale_DOE", Convert.ToDateTime(tb_SAEdate.Text));


                        cmd.ExecuteNonQuery();

                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_SAEpn2.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity - Convert.ToInt32(tb_SAEq2.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + cb_SAEpn2.SelectedValue + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;
                    }

                    if (tb_SAEq3.Text != null && tb_SAEq3.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblSaleBill VALUES (@Sale_Id, @Customer_Name, @Customer_Contact, @Product_Code ,@Quantity_Sold, @Sale_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Sale_Id", tb_SAEbill.Text);
                        cmd.Parameters.AddWithValue("@Customer_Name", tb_SAEcname.Text);
                        cmd.Parameters.AddWithValue("@Customer_Contact", tb_SAEcno.Text);
                        cmd.Parameters.AddWithValue("@Product_Code", tb_SAEpc3.Text);
                        cmd.Parameters.AddWithValue("@Quantity_Sold", Convert.ToInt32(tb_SAEq3.Text));
                        cmd.Parameters.AddWithValue("@Sale_DOE", Convert.ToDateTime(tb_SAEdate.Text));


                        cmd.ExecuteNonQuery();

                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_SAEpn3.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity - Convert.ToInt32(tb_SAEq3.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + cb_SAEpn3.SelectedValue + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;
                    }

                    if (tb_SAEq4.Text != null && tb_SAEq4.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblSaleBill VALUES (@Sale_Id, @Customer_Name, @Customer_Contact, @Product_Code ,@Quantity_Sold, @Sale_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Sale_Id", tb_SAEbill.Text);
                        cmd.Parameters.AddWithValue("@Customer_Name", tb_SAEcname.Text);
                        cmd.Parameters.AddWithValue("@Customer_Contact", tb_SAEcno.Text);
                        cmd.Parameters.AddWithValue("@Product_Code", tb_SAEpc4.Text);
                        cmd.Parameters.AddWithValue("@Quantity_Sold", Convert.ToInt32(tb_SAEq4.Text));
                        cmd.Parameters.AddWithValue("@Sale_DOE", Convert.ToDateTime(tb_SAEdate.Text));


                        cmd.ExecuteNonQuery();

                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_SAEpn4.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity - Convert.ToInt32(tb_SAEq4.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + cb_SAEpn4.SelectedValue + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;
                    }

                    if (tb_SAEq5.Text != null && tb_SAEq5.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblSaleBill VALUES (@Sale_Id, @Customer_Name, @Customer_Contact, @Product_Code ,@Quantity_Sold, @Sale_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Sale_Id", tb_SAEbill.Text);
                        cmd.Parameters.AddWithValue("@Customer_Name", tb_SAEcname.Text);
                        cmd.Parameters.AddWithValue("@Customer_Contact", tb_SAEcno.Text);
                        cmd.Parameters.AddWithValue("@Product_Code", tb_SAEpc5.Text);
                        cmd.Parameters.AddWithValue("@Quantity_Sold", Convert.ToInt32(tb_SAEq5.Text));
                        cmd.Parameters.AddWithValue("@Sale_DOE", Convert.ToDateTime(tb_SAEdate.Text));


                        cmd.ExecuteNonQuery();

                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_SAEpn5.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity - Convert.ToInt32(tb_SAEq5.Text);

                        SqlCommand cmd1 = new SqlCommand("UPDATE tblproduct SET Stock_Quantity = " + Stock_Quantity + " WHERE product_code='" + cb_SAEpn5.SelectedValue + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;
                    }

                    if ( k == true)
                    {
                        MessageBox.Show("Sale Bill Entry is Successful.");
                    }
                    else
                    {
                        MessageBox.Show("Some Field is Missing. Entry Unsuccessful.");
                    }


                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
    
            }
            

        }

        private void cb_SAEpn1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tb_SAEpc1.Text = ((System.Data.DataRowView)(cb_SAEpn1.SelectedItem))["Product_Code"].ToString();
                tb_SAEsp1.Text = ((System.Data.DataRowView)(cb_SAEpn1.SelectedItem))["SP_Unit"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }


            
        }

        private void cb_SAEpn2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tb_SAEpc2.Text = ((System.Data.DataRowView)(cb_SAEpn2.SelectedItem))["Product_Code"].ToString();
                tb_SAEsp2.Text = ((System.Data.DataRowView)(cb_SAEpn2.SelectedItem))["SP_Unit"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void cb_SAEpn3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tb_SAEpc3.Text = ((System.Data.DataRowView)(cb_SAEpn3.SelectedItem))["Product_Code"].ToString();
                tb_SAEsp3.Text = ((System.Data.DataRowView)(cb_SAEpn3.SelectedItem))["SP_Unit"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void cb_SAEpn4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tb_SAEpc4.Text = ((System.Data.DataRowView)(cb_SAEpn4.SelectedItem))["Product_Code"].ToString();
                tb_SAEsp4.Text = ((System.Data.DataRowView)(cb_SAEpn4.SelectedItem))["SP_Unit"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }


        }

        private void cb_SAEpn5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tb_SAEpc5.Text = ((System.Data.DataRowView)(cb_SAEpn5.SelectedItem))["Product_Code"].ToString();
                tb_SAEsp5.Text = ((System.Data.DataRowView)(cb_SAEpn5.SelectedItem))["SP_Unit"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }


            

        }

        private void tb_SAEq1_LostFocus(object sender, RoutedEventArgs e)
        {
            int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_SAEpn1.SelectedItem))["Stock_Quantity"].ToString());
            if (Stock_Quantity == 0)
            {
                MessageBox.Show("Stock Not Available. Please Buy Another Product.");
            }
            if (Stock_Quantity > 0)
            {
                double Sale_price = Convert.ToDouble(tb_SAEsp1.Text);
                int Quantity = Convert.ToInt32(tb_SAEq1.Text);
                if (Quantity <= Stock_Quantity)
                {
                    tb_SAEp1.Text = (Quantity * Sale_price).ToString();
                    if (tb_SAEp1.Text != null)
                        tb_SAEtp.Text = tb_SAEp1.Text;
                }
                else
                {
                    MessageBox.Show("Available Quantity: " + Stock_Quantity + " Please Enter Less than that.");
                }
            }
            
        }

        private void tb_SAEq2_LostFocus(object sender, RoutedEventArgs e)
        {
            int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_SAEpn2.SelectedItem))["Stock_Quantity"].ToString());
            if (Stock_Quantity == 0)
            {
                MessageBox.Show("Stock Not Available. Please Buy Another Product.");
            }
            if (Stock_Quantity > 0)
            {
                double Sale_price = Convert.ToDouble(tb_SAEsp2.Text);
                int Quantity = Convert.ToInt32(tb_SAEq1.Text);
                if (Quantity <= Stock_Quantity)
                {
                    tb_SAEp2.Text = (Quantity * Sale_price).ToString();
                    if (tb_SAEp2.Text != null && tb_SAEp1.Text != null)
                    {
                        double total1 = Convert.ToDouble(tb_SAEp1.Text);
                        Double total2 = Convert.ToDouble(tb_SAEp2.Text);

                        tb_SAEtp.Text = (total1 + total2).ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Available Quantity: " + Stock_Quantity + " Please Enter Less than that.");
                }

                
            }
            
        }

        private void tb_SAEq3_LostFocus(object sender, RoutedEventArgs e)
        {
            int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_SAEpn3.SelectedItem))["Stock_Quantity"].ToString());
            if (Stock_Quantity == 0)
            {
                MessageBox.Show("Stock Not Available. Please Buy Another Product.");
            }
            if (Stock_Quantity > 0)
            {
                double Sale_price = Convert.ToDouble(tb_SAEsp3.Text);
                int Quantity = Convert.ToInt32(tb_SAEq3.Text);
                if (Quantity <= Stock_Quantity)
                {
                    tb_SAEp3.Text = (Quantity * Sale_price).ToString();
                    if (tb_SAEp2.Text != null && tb_SAEp1.Text != null && tb_SAEp3.Text != null)
                    {
                        double total1 = Convert.ToDouble(tb_SAEp1.Text);
                        double total2 = Convert.ToDouble(tb_SAEp2.Text);
                        double total3 = Convert.ToDouble(tb_SAEp3.Text);

                        tb_SAEtp.Text = (total1 + total2 + total3).ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Available Quantity: " + Stock_Quantity + " Please Enter Less than that.");
                }
                
            }
            

        }

        private void tb_SAEq4_LostFocus(object sender, RoutedEventArgs e)
        {
            int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_SAEpn4.SelectedItem))["Stock_Quantity"].ToString());
            if (Stock_Quantity == 0)
            {
                MessageBox.Show("Stock Not Available. Please Buy Another Product.");
            }
            if (Stock_Quantity > 0)
            {
                double Sale_price = Convert.ToDouble(tb_SAEsp4.Text);
                int Quantity = Convert.ToInt32(tb_SAEq4.Text);
                if (Quantity <= Stock_Quantity)
                {
                    tb_SAEp4.Text = (Quantity * Sale_price).ToString();
                    if (tb_SAEp2.Text != null && tb_SAEp1.Text != null && tb_SAEp3.Text != null && tb_SAEp4.Text != null)
                        tb_SAEtp.Text = (Convert.ToDouble(tb_SAEp1.Text) + Convert.ToDouble(tb_SAEp2.Text) + Convert.ToDouble(tb_SAEp3.Text) + Convert.ToDouble(tb_SAEp4.Text)).ToString();
                }
                else
                {
                    MessageBox.Show("Available Quantity: " + Stock_Quantity + " Please Enter Less than that.");
                }
               

            }
            

        }

        private void tb_SAEq5_LostFocus(object sender, RoutedEventArgs e)
        {
            int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_SAEpn5.SelectedItem))["Stock_Quantity"].ToString());
            if (Stock_Quantity == 0)
            {
                MessageBox.Show("Stock Not Available. Please Buy Another Product.");
            }
            if (Stock_Quantity > 0)
            {
                double Sale_price = Convert.ToDouble(tb_SAEsp5.Text);
                int Quantity = Convert.ToInt32(tb_SAEq5.Text);
                if(Quantity <= Stock_Quantity)
                {
                    tb_SAEp5.Text = (Quantity * Sale_price).ToString();
                    if (tb_SAEp1.Text != null && tb_SAEp2.Text != null && tb_SAEp3.Text != null && tb_SAEp4.Text != null && tb_SAEp5.Text != null)
                        tb_SAEtp.Text = (Convert.ToDouble(tb_SAEp1.Text) + Convert.ToDouble(tb_SAEp2.Text)+ Convert.ToDouble(tb_SAEp3.Text) + Convert.ToDouble(tb_SAEp4.Text) + Convert.ToDouble(tb_SAEp5.Text)).ToString();
                }
                else
                {
                    MessageBox.Show("Available Quantity: " + Stock_Quantity + " Please Enter Less than that.");
                }
                
            }
            

        }
    }
}
