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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnVendor_Click(object sender, RoutedEventArgs e)
        {
            //FOR OPENING NEW VENDOR PAGE IN MAINWINDOW IFRAME
            Main.Content = new Vendor();

            //TO CHANGE BUTTON COLOR ON CLICK
            btnVendor.Background = Brushes.Navy;
            btnProduct.Background = Brushes.RosyBrown;
            btnPBE.Background = Brushes.RosyBrown;
            btnSAE.Background = Brushes.RosyBrown;
            btnRSE.Background = Brushes.RosyBrown;
            btnInventory.Background = Brushes.RosyBrown;
            btnReport.Background = Brushes.RosyBrown;
            btnUser.Background = Brushes.RosyBrown;
            btnCategory.Background = Brushes.RosyBrown;
            btnLogout.Background = Brushes.RosyBrown;
            
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            //FOR OPENING NEW PRODUCT PAGE IN MAINWINDOW IFRAME
            Main.Content = new Product();

            //TO CHANGE BUTTON COLOR ON CLICK
            btnVendor.Background = Brushes.RosyBrown;
            btnProduct.Background = Brushes.Navy;
            btnPBE.Background = Brushes.RosyBrown;
            btnSAE.Background = Brushes.RosyBrown;
            btnRSE.Background = Brushes.RosyBrown;
            btnInventory.Background = Brushes.RosyBrown;
            btnReport.Background = Brushes.RosyBrown;
            btnUser.Background = Brushes.RosyBrown;
            btnCategory.Background = Brushes.RosyBrown;
            btnLogout.Background = Brushes.RosyBrown;
        }

        private void btnPBE_Click(object sender, RoutedEventArgs e)
        {
            //FOR OPENING NEW PBE PAGE IN MAINWINDOW IFRAME
            Main.Content = new PurchaseBillEntry();

            //TO CHANGE BUTTON COLOR ON CLICK
            btnVendor.Background = Brushes.RosyBrown;
            btnProduct.Background = Brushes.RosyBrown;
            btnPBE.Background = Brushes.Navy;
            btnSAE.Background = Brushes.RosyBrown;
            btnRSE.Background = Brushes.RosyBrown;
            btnInventory.Background = Brushes.RosyBrown;
            btnReport.Background = Brushes.RosyBrown;
            btnUser.Background = Brushes.RosyBrown;
            btnCategory.Background = Brushes.RosyBrown;
            btnLogout.Background = Brushes.RosyBrown;
        }

        private void btnSAE_Click(object sender, RoutedEventArgs e)
        {
            //FOR OPENING NEW SAE PAGE IN MAINWINDOW IFRAME
            Main.Content = new SaleAcceptanceEntry();

            //TO CHANGE BUTTON COLOR ON CLICK
            btnVendor.Background = Brushes.RosyBrown;
            btnProduct.Background = Brushes.RosyBrown;
            btnPBE.Background = Brushes.RosyBrown;
            btnSAE.Background = Brushes.Navy;
            btnRSE.Background = Brushes.RosyBrown;
            btnInventory.Background = Brushes.RosyBrown;
            btnReport.Background = Brushes.RosyBrown;
            btnUser.Background = Brushes.RosyBrown;
            btnCategory.Background = Brushes.RosyBrown;
            btnLogout.Background = Brushes.RosyBrown;
        }

        private void btnRSE_Click(object sender, RoutedEventArgs e)
        {
            //FOR OPENING NEW RSE PAGE IN MAINWINDOW IFRAME
            Main.Content = new ReturnSaleEntry();

            //TO CHANGE BUTTON COLOR ON CLICK
            btnVendor.Background = Brushes.RosyBrown;
            btnProduct.Background = Brushes.RosyBrown;
            btnPBE.Background = Brushes.RosyBrown;
            btnSAE.Background = Brushes.RosyBrown;
            btnRSE.Background = Brushes.Navy;
            btnInventory.Background = Brushes.RosyBrown;
            btnReport.Background = Brushes.RosyBrown;
            btnUser.Background = Brushes.RosyBrown;
            btnCategory.Background = Brushes.RosyBrown;
            btnLogout.Background = Brushes.RosyBrown;
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            //FOR OPENING NEW INVENTORY PAGE IN MAINWINDOW IFRAME
            Main.Content = new Inventory();

            //TO CHANGE BUTTON COLOR ON CLICK
            btnVendor.Background = Brushes.RosyBrown;
            btnProduct.Background = Brushes.RosyBrown;
            btnPBE.Background = Brushes.RosyBrown;
            btnSAE.Background = Brushes.RosyBrown;
            btnRSE.Background = Brushes.RosyBrown;
            btnInventory.Background = Brushes.Navy;
            btnReport.Background = Brushes.RosyBrown;
            btnUser.Background = Brushes.RosyBrown;
            btnCategory.Background = Brushes.RosyBrown;
            btnLogout.Background = Brushes.RosyBrown;
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            //FOR OPENING NEW REPORT PAGE IN MAINWINDOW IFRAME
            Main.Content = new Report();

            //TO CHANGE BUTTON COLOR ON CLICK
            btnVendor.Background = Brushes.RosyBrown;
            btnProduct.Background = Brushes.RosyBrown;
            btnPBE.Background = Brushes.RosyBrown;
            btnSAE.Background = Brushes.RosyBrown;
            btnRSE.Background = Brushes.RosyBrown;
            btnInventory.Background = Brushes.RosyBrown;
            btnReport.Background = Brushes.Navy;
            btnUser.Background = Brushes.RosyBrown;
            btnCategory.Background = Brushes.RosyBrown;
            btnLogout.Background = Brushes.RosyBrown;
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            //FOR OPENING NEW USER PAGE IN MAINWINDOW IFRAME
            Main.Content = new UserList();

            //TO CHANGE BUTTON COLOR ON CLICK
            btnVendor.Background = Brushes.RosyBrown;
            btnProduct.Background = Brushes.RosyBrown;
            btnPBE.Background = Brushes.RosyBrown;
            btnSAE.Background = Brushes.RosyBrown;
            btnRSE.Background = Brushes.RosyBrown;
            btnInventory.Background = Brushes.RosyBrown;
            btnReport.Background = Brushes.RosyBrown;
            btnUser.Background = Brushes.Navy;
            btnCategory.Background = Brushes.RosyBrown;
            btnLogout.Background = Brushes.RosyBrown;
        }

        private void btnCategory_Click(object sender, RoutedEventArgs e)
        {
            //FOR OPENING NEW CATEGORY PAGE IN MAINWINDOW IFRAME
            Main.Content = new Category();

            //TO CHANGE BUTTON COLOR ON CLICK
            btnVendor.Background = Brushes.RosyBrown;
            btnProduct.Background = Brushes.RosyBrown;
            btnPBE.Background = Brushes.RosyBrown;
            btnSAE.Background = Brushes.RosyBrown;
            btnRSE.Background = Brushes.RosyBrown;
            btnInventory.Background = Brushes.RosyBrown;
            btnReport.Background = Brushes.RosyBrown;
            btnUser.Background = Brushes.RosyBrown;
            btnCategory.Background = Brushes.Navy;
            btnLogout.Background = Brushes.RosyBrown;
        }

        

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            //TO CHANGE BUTTON COLOR ON CLICK
            btnVendor.Background = Brushes.RosyBrown;
            btnProduct.Background = Brushes.RosyBrown;
            btnPBE.Background = Brushes.RosyBrown;
            btnSAE.Background = Brushes.RosyBrown;
            btnRSE.Background = Brushes.RosyBrown;
            btnInventory.Background = Brushes.RosyBrown;
            btnReport.Background = Brushes.RosyBrown;
            btnUser.Background = Brushes.RosyBrown;
            btnCategory.Background = Brushes.RosyBrown;
            btnLogout.Background = Brushes.Navy;

            // TO LOGOUT FROM CURRENT SESSION AND OPEN LOGIN PAGE
            var mv = new LoginScreen();
            mv.Show();
            this.Close();
        
        }
    }
}
