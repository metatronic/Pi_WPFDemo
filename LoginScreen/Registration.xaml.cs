using System;
using System.Collections.Generic;
using System.IO;
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

namespace LoginScreen
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        string gender;
        string status;
        public Registration()
        {
            InitializeComponent();
            gender = "";
            status = "";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UserNameTxt.Text == "")
            {
                MessageBox.Show("Enter UserName");
            }
            else
            {
                string path = @"D:\Code\Users\" + UserNameTxt.Text + ".txt";
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    StreamWriter sw = new StreamWriter(fileStream);
                    sw.WriteLine($"User Name={UserNameTxt.Text}");
                    sw.WriteLine($"Password={PasswordTxt.Password}");
                    sw.WriteLine($"Address={AddressTxt.Text}");
                    ComboBoxItem cb = (ComboBoxItem)CountryCombo.SelectedItem;
                    sw.WriteLine($"Country={cb.Content}");
                    sw.WriteLine($"Telephone={TelTxt.Text}");
                    sw.WriteLine($"Mobile={MobTxt.Text}");
                    sw.WriteLine($"Gender={gender}");
                    sw.WriteLine($"Status={status}");
                    string qualification = "";
                    foreach (object item in QualificationListbox.SelectedItems)
                    {
                        ListBoxItem li = (ListBoxItem)item;
                        qualification += li.Content + " ";
                    }
                    sw.WriteLine($"Qualification={qualification}");
                    sw.Close();
                }
                UpdateFileList();
                ShowRichText(path);
            }

        }

        void ShowRichText(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fileStream);
                string userContent = sr.ReadToEnd();
                UserInfo.Visibility = Visibility.Visible;
                UserInfo.Document.Blocks.Clear();
                UserInfo.AppendText(userContent);
            }
        }

        private void TelCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            TelTxt.IsEnabled = true;
        }

        private void MobCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            MobTxt.IsEnabled = true;
        }

        private void MobCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            MobTxt.IsEnabled = false;
        }

        private void TelCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            TelTxt.IsEnabled = false;
        }

        private void GenderRadio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            gender = rd.Content.ToString();
        }

        private void StatusRadio_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            status = rd.Content.ToString();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            UserInfo.Visibility = Visibility.Hidden;
        }

        private void DisplayExistingCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ExistingFilesListbox.Visibility = Visibility.Visible;
            UpdateFileList();
        }

        private void DisplayExistingCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ExistingFilesListbox.Visibility = Visibility.Hidden;
        }

        void UpdateFileList()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(@"D:\Code\Users\");
            FileInfo[] fi = directoryInfo.GetFiles();
            ExistingFilesListbox.Items.Clear();
            foreach (FileInfo item in fi)
            {
                ExistingFilesListbox.Items.Add(item.FullName);
            }
        }

        private void ExistingFilesListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string li = (sender as ListBox).SelectedItem as string;
            if (li != null)
            {
                ShowRichText(li);
            }
        }
    }
}
