using System;//fffgc///
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
using BLAPI;


namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class LogInWindow : Window
    {

        IBL bl = BLFactory.GetBL("1");
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void btnGO_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userName = userNameTextBox.Text;
                string passcode = passwordTextBox.Password;
                if (userName == "" || passcode == "")
                {
                    MessageBox.Show("ERROR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                BO.User logtIn = new BO.User();
                logtIn = bl.SignIn(userName, passcode);
                if (rbManagement.IsChecked == true)
                {
                    if (logtIn.managaccount)
                    {
                        managementWindow win = new managementWindow(bl, userName);
                        win.ShowDialog();
                    }
                    else
                        MessageBox.Show("למשתמש זה אין אפשרות כניסה כמנהל", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else if (rbUser.IsChecked == true)
                {
                    UserWindows win = new UserWindows(bl, userName);
                    win.Show();

                }
               
            }
            catch(BO.BadUserException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewUser win = new NewUser(bl);
            win.ShowDialog();
        }

        private void passwordEye_Click_(object sender, RoutedEventArgs e)
        {
            closeEyeBotton.Visibility = Visibility.Hidden;
            eyeBotton.Visibility = Visibility.Visible;
            passwordTextBox.Visibility= Visibility.Hidden;
            passwordTextBox2.Visibility = Visibility.Visible;
            passwordTextBox2.Text = passwordTextBox.Password;
        }
        private void passwordOpenEye_Click(object sender, RoutedEventArgs e)
        {
            closeEyeBotton.Visibility = Visibility.Visible;
            eyeBotton.Visibility = Visibility.Hidden;
            passwordTextBox.Visibility = Visibility.Visible;
            passwordTextBox2.Visibility = Visibility.Hidden;
            passwordTextBox.Password= passwordTextBox2.Text ;
        }
        
    }
} 

