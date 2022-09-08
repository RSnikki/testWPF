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
    /// Interaction logic for RSEform.xaml
    /// </summary>
    public partial class RSEform : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public RSEform()
        {
            InitializeComponent();
        }

        public bool isValid()
        {
            if (tb_RSEno.Text == string.Empty)
            {
                MessageBox.Show("Return Bill No. is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (tb_RSEsaleno.Text == string.Empty)
            {
                MessageBox.Show("Sale No. is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (tb_RSEdate.Text == string.Empty)
            {
                MessageBox.Show("Date is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
           
            return true;
        }



        public void searchSale()
        {
            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();
            string query = "SELECT SB_ID, Product_Code, Quantity_Sold FROM tblSaleBill WHERE Sale_Id = tb_RSEsaleno.Text";
            SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
            sqlcmd.CommandType = System.Data.CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd);
            

        }

       

        private void tb_RSEsaleno_LostFocus(object sender, RoutedEventArgs e)
        {
            searchSale();
        }

        private void btn_SearchSale_Click(object sender, RoutedEventArgs e)
        {
            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();
            string query = "SELECT [SB_ID]      ,[Sale_Id]      ,[dbo].[tblSaleBill].[Product_Code]	  ,[Product_Name]      ,[Quantity_Sold]	,[SP_Unit] ,[Stock_Quantity]  FROM[dbo].[tblSaleBill] inner join[dbo].[tblProduct]            on[dbo].[tblSaleBill].Product_Code =[dbo].[tblProduct].Product_Code  where[dbo].[tblSaleBill].Sale_Id = '" + tb_RSEsaleno.Text + "';";
            SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
            sqlcmd.CommandType = System.Data.CommandType.Text;
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcmd);
            adapter.Fill(dt);
            cb_RSEpn1.ItemsSource = dt.DefaultView;
            cb_RSEpn1.DisplayMemberPath = "Product_Name";
            cb_RSEpn1.SelectedValuePath = "SB_ID";
            cb_RSEpn2.ItemsSource = dt.DefaultView;
            cb_RSEpn2.DisplayMemberPath = "Product_Name";
            cb_RSEpn2.SelectedValuePath = "SB_ID";
            cb_RSEpn3.ItemsSource = dt.DefaultView;
            cb_RSEpn3.DisplayMemberPath = "Product_Name";
            cb_RSEpn3.SelectedValuePath = "SB_ID";
            cb_RSEpn4.ItemsSource = dt.DefaultView;
            cb_RSEpn4.DisplayMemberPath = "Product_Name";
            cb_RSEpn4.SelectedValuePath = "SB_ID";
            cb_RSEpn5.ItemsSource = dt.DefaultView;
            cb_RSEpn5.DisplayMemberPath = "Product_Name";
            cb_RSEpn5.SelectedValuePath = "SB_ID";
         
            
        }

        private void cb_RSEpn1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                tb_RSEsp1.Text = ((System.Data.DataRowView)(cb_RSEpn1.SelectedItem))["SP_Unit"].ToString();
                tb_RSEqs1.Text = ((System.Data.DataRowView)(cb_RSEpn1.SelectedItem))["Quantity_Sold"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void cb_RSEpn2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {

                tb_RSEsp2.Text = ((System.Data.DataRowView)(cb_RSEpn2.SelectedItem))["SP_Unit"].ToString();
                tb_RSEqs2.Text = ((System.Data.DataRowView)(cb_RSEpn2.SelectedItem))["Quantity_Sold"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void cb_RSEpn3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {

                tb_RSEsp3.Text = ((System.Data.DataRowView)(cb_RSEpn3.SelectedItem))["SP_Unit"].ToString();
                tb_RSEqs3.Text = ((System.Data.DataRowView)(cb_RSEpn3.SelectedItem))["Quantity_Sold"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void cb_RSEpn4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {

                tb_RSEsp4.Text = ((System.Data.DataRowView)(cb_RSEpn4.SelectedItem))["SP_Unit"].ToString();
                tb_RSEqs4.Text = ((System.Data.DataRowView)(cb_RSEpn4.SelectedItem))["Quantity_Sold"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void cb_RSEpn5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {

                tb_RSEsp5.Text = ((System.Data.DataRowView)(cb_RSEpn5.SelectedItem))["SP_Unit"].ToString();
                tb_RSEqs5.Text = ((System.Data.DataRowView)(cb_RSEpn5.SelectedItem))["Quantity_Sold"].ToString();

            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void tb_RSEq1_LostFocus(object sender, RoutedEventArgs e)
        {
            double Return_price = Convert.ToDouble(tb_RSEsp1.Text);
            int Quantity = Convert.ToInt32(tb_RSEq1.Text);
            if (Quantity <= (Convert.ToInt32(tb_RSEqs1.Text)))
            {
                tb_RSEp1.Text = (Quantity * Return_price).ToString();
                if (tb_RSEp1.Text != null)
                    tb_RSEtp.Text = tb_RSEp1.Text;
            }
            else
            {
                MessageBox.Show("Entered Return Quantity is More than Sold Quantity.");
            }
    
        }

        private void tb_RSEq2_LostFocus(object sender, RoutedEventArgs e)
        {
            double Return_price = Convert.ToDouble(tb_RSEsp2.Text);
            int Quantity = Convert.ToInt32(tb_RSEq2.Text);
            if (Quantity <= (Convert.ToInt32(tb_RSEqs2.Text)))
            {
                tb_RSEp2.Text = (Quantity * Return_price).ToString();
                if (tb_RSEp2.Text != null && tb_RSEp1.Text != null)
                {
                    double total1 = Convert.ToDouble(tb_RSEp1.Text);
                    double total2 = Convert.ToDouble(tb_RSEp2.Text);

                    tb_RSEtp.Text = (total1 + total2).ToString();
                }

            }
            else
            {
                MessageBox.Show("Entered Return Quantity is More than Sold Quantity.");
            }

        }

        private void tb_RSEq3_LostFocus(object sender, RoutedEventArgs e)
        {
            double Return_price = Convert.ToDouble(tb_RSEsp3.Text);
            int Quantity = Convert.ToInt32(tb_RSEq3.Text);
            if (Quantity <= (Convert.ToInt32(tb_RSEqs3.Text)))
            {
                tb_RSEp3.Text = (Quantity * Return_price).ToString();
                if (tb_RSEp2.Text != null && tb_RSEp1.Text != null && tb_RSEp3.Text != null)
                {
                    double total1 = Convert.ToDouble(tb_RSEp1.Text);
                    double total2 = Convert.ToDouble(tb_RSEp2.Text);
                    double total3 = Convert.ToDouble(tb_RSEp3.Text);

                    tb_RSEtp.Text = (total1 + total2 + total3).ToString();
                }
            }
            else
            {
                MessageBox.Show("Entered Return Quantity is More than Sold Quantity.");
            }
            

        }

        private void tb_RSEq4_LostFocus(object sender, RoutedEventArgs e)
        {
            double Return_price = Convert.ToDouble(tb_RSEsp4.Text);
            int Quantity = Convert.ToInt32(tb_RSEq4.Text);
            if (Quantity <= (Convert.ToInt32(tb_RSEqs4.Text)))
            {
                tb_RSEp4.Text = (Quantity * Return_price).ToString();
                if (tb_RSEp2.Text != null && tb_RSEp1.Text != null && tb_RSEp3.Text != null && tb_RSEp4.Text != null)
                    tb_RSEtp.Text = (Convert.ToDouble(tb_RSEp1.Text) + Convert.ToDouble(tb_RSEp2.Text) + Convert.ToDouble(tb_RSEp3.Text) + Convert.ToDouble(tb_RSEp4.Text)).ToString();
            }
            else
            {
                MessageBox.Show("Entered Return Quantity is More than Sold Quantity.");
            }
           

        }

        private void tb_RSEq5_LostFocus(object sender, RoutedEventArgs e)
        {
            double Return_price = Convert.ToDouble(tb_RSEsp5.Text);
            int Quantity = Convert.ToInt32(tb_RSEq5.Text);
            if (Quantity <= (Convert.ToInt32(tb_RSEqs5.Text)))
            {
                tb_RSEp5.Text = (Quantity * Return_price).ToString();
                if (tb_RSEp1.Text != null && tb_RSEp2.Text != null && tb_RSEp3.Text != null && tb_RSEp4.Text != null && tb_RSEp5.Text != null)
                    tb_RSEtp.Text = (Convert.ToDouble(tb_RSEp1.Text) + Convert.ToDouble(tb_RSEp2.Text)
                        + Convert.ToDouble(tb_RSEp3.Text) + Convert.ToDouble(tb_RSEp4.Text) + Convert.ToDouble(tb_RSEp5.Text)).ToString();
            }
            else
            {
                MessageBox.Show("Entered Return Quantity is More than Sold Quantity.");
            }
            

        }

        private void btn_AddRSB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                if (isValid())
                {
                    bool k = false;
                    if (tb_RSEq1.Text != null && tb_RSEq1.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblRSBill VALUES (@ReturnSale_Id, @Sale_Id, @Product_Code ,@Quantity_Return, @Return_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ReturnSale_Id", tb_RSEno.Text);
                        cmd.Parameters.AddWithValue("@Sale_Id", tb_RSEsaleno.Text);
                        cmd.Parameters.AddWithValue("@Product_Code", ((System.Data.DataRowView)(cb_RSEpn1.SelectedItem))["Product_Code"].ToString());
                        cmd.Parameters.AddWithValue("@Quantity_Return", Convert.ToInt32(tb_RSEq1.Text));
                        cmd.Parameters.AddWithValue("@Return_DOE", Convert.ToDateTime(tb_RSEdate.Text));


                        cmd.ExecuteNonQuery();

                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_RSEpn1.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity + Convert.ToInt32(tb_RSEq1.Text);
                        string product_code= ((System.Data.DataRowView)(cb_RSEpn1.SelectedItem))["Product_Code"].ToString();

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + product_code + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;

                    }

                    if (tb_RSEq2.Text != null && tb_RSEq2.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblRSBill VALUES (@ReturnSale_Id, @Sale_Id, @Product_Code ,@Quantity_Return, @Return_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ReturnSale_Id", tb_RSEno.Text);
                        cmd.Parameters.AddWithValue("@Sale_Id", tb_RSEsaleno.Text);

                        cmd.Parameters.AddWithValue("@Product_Code", ((System.Data.DataRowView)(cb_RSEpn2.SelectedItem))["Product_Code"].ToString());
                        cmd.Parameters.AddWithValue("@Quantity_Return", Convert.ToInt32(tb_RSEq2.Text));
                        cmd.Parameters.AddWithValue("@Return_DOE", Convert.ToDateTime(tb_RSEdate.Text));


                        cmd.ExecuteNonQuery();

                        string product_code = ((System.Data.DataRowView)(cb_RSEpn2.SelectedItem))["Product_Code"].ToString();
                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_RSEpn2.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity + Convert.ToInt32(tb_RSEq2.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + product_code + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;
                    }

                    if (tb_RSEq3.Text != null && tb_RSEq3.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblRSBill VALUES (@ReturnSale_Id, @Sale_Id, @Product_Code ,@Quantity_Return, @Return_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ReturnSale_Id", tb_RSEno.Text);
                        cmd.Parameters.AddWithValue("@Sale_Id", tb_RSEsaleno.Text);

                        cmd.Parameters.AddWithValue("@Product_Code", ((System.Data.DataRowView)(cb_RSEpn3.SelectedItem))["Product_Code"].ToString());
                        cmd.Parameters.AddWithValue("@Quantity_Return", Convert.ToInt32(tb_RSEq3.Text));
                        cmd.Parameters.AddWithValue("@Return_DOE", Convert.ToDateTime(tb_RSEdate.Text));


                        cmd.ExecuteNonQuery();

                        string product_code = ((System.Data.DataRowView)(cb_RSEpn3.SelectedItem))["Product_Code"].ToString();
                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_RSEpn3.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity + Convert.ToInt32(tb_RSEq3.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + product_code + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;
                    }

                    if (tb_RSEq4.Text != null && tb_RSEq4.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblRSBill VALUES (@ReturnSale_Id, @Sale_Id, @Product_Code ,@Quantity_Return, @Return_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ReturnSale_Id", tb_RSEno.Text);
                        cmd.Parameters.AddWithValue("@Sale_Id", tb_RSEsaleno.Text);

                        cmd.Parameters.AddWithValue("@Product_Code", ((System.Data.DataRowView)(cb_RSEpn4.SelectedItem))["Product_Code"].ToString());
                        cmd.Parameters.AddWithValue("@Quantity_Return", Convert.ToInt32(tb_RSEq4.Text));
                        cmd.Parameters.AddWithValue("@Return_DOE", Convert.ToDateTime(tb_RSEdate.Text));


                        cmd.ExecuteNonQuery();

                        string product_code = ((System.Data.DataRowView)(cb_RSEpn4.SelectedItem))["Product_Code"].ToString();
                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_RSEpn4.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity + Convert.ToInt32(tb_RSEq4.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + product_code + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;
                    }

                    if (tb_RSEq5.Text != null && tb_RSEq5.Text != "")
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO tblRSBill VALUES (@ReturnSale_Id, @Sale_Id, @Product_Code ,@Quantity_Return, @Return_DOE)", sqlCon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ReturnSale_Id", tb_RSEno.Text);
                        cmd.Parameters.AddWithValue("@Sale_Id", tb_RSEsaleno.Text);

                        cmd.Parameters.AddWithValue("@Product_Code", ((System.Data.DataRowView)(cb_RSEpn5.SelectedItem))["Product_Code"].ToString());
                        cmd.Parameters.AddWithValue("@Quantity_Return", Convert.ToInt32(tb_RSEq5.Text));
                        cmd.Parameters.AddWithValue("@Return_DOE", Convert.ToDateTime(tb_RSEdate.Text));


                        cmd.ExecuteNonQuery();

                        string product_code = ((System.Data.DataRowView)(cb_RSEpn5.SelectedItem))["Product_Code"].ToString();
                        int Stock_Quantity = Convert.ToInt32(((System.Data.DataRowView)(cb_RSEpn5.SelectedItem))["Stock_Quantity"].ToString());
                        Stock_Quantity = Stock_Quantity + Convert.ToInt32(tb_RSEq5.Text);

                        SqlCommand cmd1 = new SqlCommand("update tblproduct set Stock_Quantity = " + Stock_Quantity + " where product_code='" + product_code + "';", sqlCon);
                        cmd1.CommandType = CommandType.Text;


                        cmd1.ExecuteNonQuery();

                        k = true;

                    }

                    if (k == true)
                    {
                        MessageBox.Show("Return Bill Entry Successful.");
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

       
    }
}
