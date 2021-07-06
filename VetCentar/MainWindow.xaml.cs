using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Threading;
using System.Resources;
using System.IO;

namespace VetCentar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string username;
        private string password;
        private zaposleni current;

        public MainWindow()
        {
            InitializeComponent();

            VetCentarUtilities.englishMenuItem = englishMenuItem;
            VetCentarUtilities.serbianMenuItem = serbianMenuItem;
            VetCentarUtilities.style1MenuItem = style1MenuItem;
            VetCentarUtilities.style2MenuItem = style2MenuItem;
            VetCentarUtilities.style3MenuItem = style3MenuItem;

            if(VetCentarUtilities.selectedLanguage == 1)
            {
                englishMenuItem.IsChecked = true;
            }
            else
            {
                serbianMenuItem.IsChecked = true;
            }

            if(VetCentarUtilities.selectedTheme == 1)
            {
                style1MenuItem.IsChecked = true;
            }
            else if(VetCentarUtilities.selectedTheme == 2)
            {
                style2MenuItem.IsChecked = true;
            }
            else
            {
                style3MenuItem.IsChecked = true;
            }


        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if("".Equals(usernameTextBox.Text) || passwordTextBox.Password.Length == 0)
            {
                errorLabel.Content = Properties.Resources.Greška_prazno;
            }
            else
            {
                bool found = false;
                username = usernameTextBox.Text;
                password = VetCentarUtilities.GetSHA256(passwordTextBox.Password);

                using (VetCentarDB db = new VetCentarDB())
                {
                    var allAdmins = (from a in db.administrators where a.zaposleni.aktivan == true select a).ToList();
                    var allVets = (from v in db.veterinars where v.zaposleni.aktivan == true select v).ToList();

                    foreach(var admin in allAdmins)
                    {
                        if(username.Equals(admin.zaposleni.korisnicko_ime) && password.Equals(admin.zaposleni.lozinka))
                        {
                            found = true;
                            current = admin.zaposleni;
                            if(current.jezik != null || current.tema != null)
                            {
                                VetCentarUtilities.ChangeLook(current);
                            }
                            new AdminWindow(admin.zaposleni).Show();
                            CloseMain();
                            Close();
                            break;
                        }
                    }

                    if(!found)
                    {
                        foreach (var vet in allVets)
                        {
                            if (username.Equals(vet.zaposleni.korisnicko_ime) && password.Equals(vet.zaposleni.lozinka))
                            {
                                found = true;
                                current = vet.zaposleni;
                                if (current.jezik != null || current.tema != null)
                                {
                                    VetCentarUtilities.ChangeLook(current);
                                }
                                new VetWindow(vet.zaposleni).Show();
                                CloseMain();
                                Close();
                                break;
                            }
                        }
                    }

                    if(!found)
                    {
                        errorLabel.Content = Properties.Resources.Greška_nema;
                        Reset();
                    }
                }
            }
        }

        private void Reset()
        {
            usernameTextBox.Text = "";
            passwordTextBox.Clear();
        }

        private void SerbianMenuItem_Click(object sender, RoutedEventArgs e)
        {
            VetCentarUtilities.SetSerbian(current);
        }

        private void EnglishMenuItem_Click(object sender, RoutedEventArgs e)
        {
            VetCentarUtilities.SetEnglish(current);
        }

        private void Style1MenuItem_Click(object sender, RoutedEventArgs e)
        {
            VetCentarUtilities.SetStyle1(null);
        }

        private void Style2MenuItem_Click(object sender, RoutedEventArgs e)
        {
            VetCentarUtilities.SetStyle2(null);
        }

        private void Style3MenuItem_Click(object sender, RoutedEventArgs e)
        {
            VetCentarUtilities.SetStyle3(null);
        }

        private void UsernameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            errorLabel.Content = "";
        }

        private void PasswordTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            errorLabel.Content = "";
        }

        private void CloseMain()
        {
            foreach (Window form in Application.Current.Windows)
            {
                if (form is MainWindow)
                {
                    form.Close();
                }
            }
        }
    }
}
