using BLAPI;
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

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for LogInUser.xaml
    /// </summary>
    public partial class NewUser  : Window
    {
        IBL bl;
        public NewUser(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userName = userNameTextBox.Text;
                string passcode = passwordTextBox.Text;
                bool AcountType = (bool)AcountTypeCheckBox.IsChecked;
                if (userName == "" || passcode == "")
                {
                    MessageBox.Show("ERROR", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                BO.User newUser = new BO.User() { UserName = userName, passCode = passcode, managaccount = AcountType };
                bl.addNewUser(newUser);
                MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch(BO.BadUserException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
