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
    /// Interaction logic for VendorForm.xaml
    /// </summary>
    public partial class VendorForm : Window
    {
        public VendorForm()
        {
            InitializeComponent();
        }

        SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=IMDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        public void clearData()
        {
            textbox_Vname.Clear();
            textbox_Vaddr.Clear();
            textbox_Vcontact.Clear();
            textbox_Vemail.Clear();
            textbox_Vdate.Clear();
        }

        

        //CHECK FOR USERNAME AND PASSWORD TEXTBOX TO BE FILLED BEFORE ADDING NEW USER
        public bool isValid()
        {
            if (textbox_Vcode.Text == string.Empty)
            {
                MessageBox.Show("Vendor Code is Required.", "Failed");
                return false;
            }
            if (textbox_Vname.Text == string.Empty)
            {
                MessageBox.Show("Vendor Name is Required", "Failed");
                return false;
            }
            if (textbox_Vaddr.Text == string.Empty)
            {
                MessageBox.Show("Vendor Address is Required", "Failed");
                return false;
            }
            if (textbox_Vcontact.Text == string.Empty)
            {
                MessageBox.Show("Vendor Contact Info is Required", "Failed");
                return false;
            }
            if (textbox_Vemail.Text == string.Empty)
            {
                MessageBox.Show("Vendor E-Mail Address is Required", "Failed");
                return false;
            }
            if (textbox_Vdate.Text == string.Empty)
            {
                MessageBox.Show("Date is Required", "Failed");
                return false;
            }
            return true;
        }

        private void btn_VFormSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO tblVendor VALUES (@VENDOR_CODE, @VENDOR_NAME, @ADDRESS, @V_CONTACT_NO, @EMAIL_ID, @DOG)", sqlCon);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@VENDOR_CODE",textbox_Vcode.Text);
                    cmd.Parameters.AddWithValue("@VENDOR_NAME", textbox_Vname.Text);
                    cmd.Parameters.AddWithValue("@ADDRESS", textbox_Vaddr.Text);
                    cmd.Parameters.AddWithValue("@V_CONTACT_NO", textbox_Vcontact.Text);
                    cmd.Parameters.AddWithValue("@EMAIL_ID", textbox_Vemail.Text);
                    cmd.Parameters.AddWithValue("@DOG",Convert.ToDateTime(textbox_Vdate.Text));
                    sqlCon.Open();
                    cmd.ExecuteNonQuery();
                    sqlCon.Close();
                    MessageBox.Show("New Vendor Successfully Added");
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

        private void btn_VFormUpdate_Click(object sender, RoutedEventArgs e)
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand("UPDATE tblVendor set VENDOR_NAME='" + textbox_Vname.Text + "', ADDRESS ='" + textbox_Vaddr.Text + "', V_CONTACT_NO = '"+textbox_Vcontact.Text+"', EMAIL_ID = '"+textbox_Vemail.Text+ "', DOG = '" +textbox_Vdate.Text+"'  WHERE VENDOR_CODE='" + textbox_Vcode.Text + "'", sqlCon);
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
