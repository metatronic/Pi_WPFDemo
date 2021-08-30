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

namespace LoginScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int passwordAttempt;
        public MainWindow()
        {
            InitializeComponent();
            passwordAttempt = 0;
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            if (userNameTxt.Text == "admin" && passwordTxt.Password == "123")
            {
                MessageBox.Show("Succesfull");
                var window = new Registration();
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("incorrect username or password");
                if (passwordAttempt < 3)
                {
                    userNameTxt.Text = "";
                    passwordTxt.Password = "";
                    passwordAttempt++;
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
