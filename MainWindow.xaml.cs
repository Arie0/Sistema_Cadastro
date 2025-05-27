using proj1.Views;
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

namespace proj1
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Menu_Click1(object sender, RoutedEventArgs e)
        {
            var fornecedor = new FornecedorView();
            fornecedor.ShowDialog();
        }
        private void Menu_Click2(object sender, RoutedEventArgs e)
        {
            var prod = new ProdutoView();
            prod.ShowDialog();
        }
        private void MenuSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
