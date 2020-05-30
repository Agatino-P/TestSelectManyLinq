using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestSelectMany
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Collections.IEnumerable lbIts;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            lbIts = lbResults.ItemsSource;
            lbResults.ItemsSource = null;
        }
        
        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            lbResults.ItemsSource =lbIts;
        }

    }
}
