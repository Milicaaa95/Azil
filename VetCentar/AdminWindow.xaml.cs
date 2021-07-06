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

namespace VetCentar
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private zaposleni current;
        private List<zaposleni> allEmployees;

        public AdminWindow(zaposleni current)
        {
            InitializeComponent();


            if (current.jezik == 1)
            {
                englishMenuItem.IsChecked = true;
            }
            else if(current.jezik == 2)
            {
                serbianMenuItem.IsChecked = true;
            }
            else
            {
                if (VetCentarUtilities.selectedLanguage == 1)
                {
                    englishMenuItem.IsChecked = true;
                }
                else
                {
                    serbianMenuItem.IsChecked = true;
                }
            }

            if (current.tema == 1)
            {
                style1MenuItem.IsChecked = true;
            }
            else if (current.tema == 2)
            {
                style2MenuItem.IsChecked = true;
            }
            else if(current.tema == 3)
            {
                style3MenuItem.IsChecked = true;
            }
            else
            {
                if (VetCentarUtilities.selectedTheme == 1)
                {
                    style1MenuItem.IsChecked = true;
                }
                else if (VetCentarUtilities.selectedTheme == 2)
                {
                    style2MenuItem.IsChecked = true;
                }
                else
                {
                    style3MenuItem.IsChecked = true;
                }
            }

            VetCentarUtilities.englishMenuItem = englishMenuItem;
            VetCentarUtilities.serbianMenuItem = serbianMenuItem;
            VetCentarUtilities.style1MenuItem = style1MenuItem;
            VetCentarUtilities.style2MenuItem = style2MenuItem;
            VetCentarUtilities.style3MenuItem = style3MenuItem;

            this.current = current;
            currentUserItem.Header = current.ime;

            DisplayActiveEmployees();
        }

        private void DisplayActiveEmployees()
        {
            employeesTable.ItemsSource = null;
            employeesTable.Items.Refresh();
            using (VetCentarDB db = new VetCentarDB())
            {
                allEmployees = (from e in db.zaposlenis where e.aktivan == true select e).ToList();
                employeesTable.ItemsSource = allEmployees;
                employeesTable.Items.Refresh();
            }

            deactivatedButton.Content = Properties.Resources.Deaktivirani_nalozi;
            activateButton.Content = Properties.Resources.Deaktiviraj;
        }

        private void DisplayDeactivatedEmployees()
        {
            employeesTable.ItemsSource = null;
            employeesTable.Items.Refresh();
            using (VetCentarDB db = new VetCentarDB())
            {
                allEmployees = (from e in db.zaposlenis where e.aktivan == false select e).ToList();
                employeesTable.ItemsSource = allEmployees;
                employeesTable.Items.Refresh();
            }

            deactivatedButton.Content = Properties.Resources.Aktivni_nalozi;
            activateButton.Content = Properties.Resources.Aktiviraj;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new AddEditEmployee().ShowDialog();

            DisplayActiveEmployees();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            zaposleni z = (zaposleni)employeesTable.SelectedItem;
            if (z == null)
            {
                MessageBox.Show(Properties.Resources.Niste_izabrali_zaposlenog);
            }
            else
            {
                new AddEditEmployee(z).ShowDialog();
            }

            DisplayActiveEmployees();
            employeesTable.SelectedItem = null;
        }

        private void ActivateButton_Click(object sender, RoutedEventArgs e)
        {
            bool active = false;

            zaposleni z = (zaposleni)employeesTable.SelectedItem;
            if (z == null)
            {
                MessageBox.Show(Properties.Resources.Niste_izabrali_zaposlenog);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(Properties.Resources.Odluka, "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Button button = (Button)sender;
                    using (VetCentarDB db = new VetCentarDB())
                    {
                        var employeeToEdit = (from em in db.zaposlenis where em.id == z.id select em).First();

                        if ("Aktiviraj".Equals(button.Content) || "Activate".Equals(button.Content))
                        {
                            employeeToEdit.aktivan = true;
                        }
                        else
                        {
                            employeeToEdit.aktivan = false;
                            active = true;
                        }
                        db.SaveChanges();

                        if (active)
                        {
                            DisplayActiveEmployees();
                        }
                        else
                        {
                            DisplayDeactivatedEmployees();
                        }
                    }
                }
            }
        }

        private void DeactivatedButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if ("Deaktivirani nalozi".Equals(button.Content) || "Deactivated accounts".Equals(button.Content))
            {
                DisplayDeactivatedEmployees();
            }
            else
            {
                DisplayActiveEmployees();
            }
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
            VetCentarUtilities.SetStyle1(current);
        }

        private void Style2MenuItem_Click(object sender, RoutedEventArgs e)
        {
            VetCentarUtilities.SetStyle2(current);
        }

        private void Style3MenuItem_Click(object sender, RoutedEventArgs e)
        {
            VetCentarUtilities.SetStyle3(current);
        }

        private void LogoutItem_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            var filteredEmployees = allEmployees.Where(employee => employee.ime.ToLower().StartsWith(searchTextBox.Text.ToLower()));
            employeesTable.ItemsSource = filteredEmployees;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (VetCentarDB db = new VetCentarDB())
            {
                var employee = (from z in db.zaposlenis where z.id == current.id select z).First();

                employee.jezik = VetCentarUtilities.selectedLanguage;
                employee.tema = VetCentarUtilities.selectedTheme;

                db.SaveChanges();
            }
        }
    }
}
