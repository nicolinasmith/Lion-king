using Lion_king.DAL;
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

namespace Lion_king
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

        /* CONNECTION: Lägg i "Manage user secrets"
           "ConnectionStrings": {
            "develop": "Server=localhost;Port=5432;User ID=lionking_user;Password=lejonkungen;Database=Lion king;"
        }*/

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DbRepository db = new();
        }

    }
}
