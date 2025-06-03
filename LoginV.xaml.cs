using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
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
using proj1.Data;

namespace proj1
{
    /// <summary>
    /// Lógica interna para LoginV.xaml
    /// </summary>
    public partial class LoginV : Window
    {
        public LoginV()
        {
            InitializeComponent();
        }
        private void btnLogin_Click_1(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(@"SERVER=ARIELE\SQLTEST;Initial Catalog=sistema;Integrated Security=True;Encrypt=False"))
            {
                try
                {
                    con.Open(); 
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE username = @username AND password = @password";
                    SqlCommand cmd = new SqlCommand(query, con);

                    
                    cmd.Parameters.AddWithValue("@username", txt_username.Text);
                    cmd.Parameters.AddWithValue("@password", txt_password.Password);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MainWindow menu = new MainWindow();
                        menu.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Credenciais inválidas");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}");
                }
            } 
        }
        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
