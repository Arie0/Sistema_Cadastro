using proj1.ViewModels;
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

namespace proj1.Views
{
    /// <summary>
    /// Lógica interna para FornecedorView.xaml
    /// </summary>
    public partial class FornecedorView : Window
    {
        public FornecedorView()
        {
            InitializeComponent();
            DataContext = new FornecedorViewModel();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        

    }
}
