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
    /// Interaction logic for Category.xaml
    /// </summary>
    public partial class Category : Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public Category()
        {
            InitializeComponent();
            LoadCategoryGrid();
            clearData();
        }


        public void clearData()
        {
            tb_Category.Clear();
            textbox_CSelect.Clear();
        }
        private void LoadCategoryGrid()
        {
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();

                string query = "SELECT * FROM tblCategory";
                SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.CommandType = System.Data.CommandType.Text;
                DataTable dt = new DataTable();
                SqlDataReader sdr = sqlcmd.ExecuteReader();
                dt.Load(sdr);
                DataGrid_Category.ItemsSource = dt.DefaultView;
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

        //CHECK FOR CATEGORY TEXTBOX TO BE FILLED BEFORE ADDING NEW CATEGORY
        public bool isValid()
        {
            if (tb_Category.Text == string.Empty)
            {
                MessageBox.Show("Category Name is Required", "Failed");
                return false;
            }
            return true;
        }

        private void btn_AddCat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    if (sqlCon.State == System.Data.ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "SELECT count(1) FROM tblCategory where Category_Name= @CategoryName;";
                    SqlCommand sqlcmd = new SqlCommand(query, sqlCon);
                    sqlcmd.CommandType = System.Data.CommandType.Text;
                    sqlcmd.Parameters.AddWithValue("@CategoryName", tb_Category.Text);

                    int count = (int)sqlcmd.ExecuteScalar();
                    if (count == 0)
                    {
                        string query1 = "INSERT INTO tblCategory ( Category_Name ) VALUES (@CategoryName); ";
                        SqlCommand sqlcmd1 = new SqlCommand(query1, sqlCon);
                        sqlcmd1.CommandType = System.Data.CommandType.Text;
                        sqlcmd1.Parameters.AddWithValue("@CategoryName", tb_Category.Text);

                        DataTable dt = new DataTable();
                        sqlcmd1.ExecuteNonQuery();
                        LoadCategoryGrid();

                    }
                    else
                    {
                        MessageBox.Show("Category Name is already there.");
                    }
                }
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

        private void btn_CDelete_Click(object sender, RoutedEventArgs e)
        {

            
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM tblCategory WHERE Category_Name ='" + textbox_CSelect.Text + "'", sqlCon);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted successfully");
                clearData();
                LoadCategoryGrid();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted" + ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void btn_CUp_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();

            SqlCommand cmd = new SqlCommand("UPDATE tblCategory set Category_Name ='" + tb_Category.Text + "' WHERE Category_Name ='" + textbox_CSelect.Text + "'", sqlCon);
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
    }
}
