using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for VetWindow.xaml
    /// </summary>
    public partial class VetWindow : Window
    {
        private zaposleni current;
        private List<ljubimac> allPets;

        public VetWindow(zaposleni current)
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

            DisplayPets();
            DisplayTypes();           
        }

        private void DisplayTypes()
        {
            using (VetCentarDB db = new VetCentarDB())
            {
                var allTypes = (from k in db.vrstas select k.naziv).ToList();
                allTypes.Add("svi");
                typeComboBox.ItemsSource = allTypes;
            }
        }

        private void DisplayPets()
        {
            petsTable.ItemsSource = null;
            petsTable.Items.Refresh();

            using (VetCentarDB db = new VetCentarDB())
            {
                allPets = (from p in db.ljubimacs where p.obrisan == false select p).ToList();
                petsTable.ItemsSource = allPets;
                petsTable.Items.Refresh();
            }
        }

        private void DisplayPetsByType(string naziv)
        {
            petsTable.ItemsSource = null;
            petsTable.Items.Refresh();

            using (VetCentarDB db = new VetCentarDB())
            {
                if("svi".Equals(naziv))
                {
                    allPets = (from p in db.ljubimacs where p.obrisan == false select p).ToList();
                }
                else
                {
                    allPets = (from p in db.ljubimacs where p.vrsta.naziv.Equals(naziv) && p.obrisan == false select p).ToList();
                }
                petsTable.ItemsSource = allPets;
                petsTable.Items.Refresh();
            }
        }

        private void DeletePetButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckSelectedPet())
            {
                MessageBoxResult result = MessageBox.Show(Properties.Resources.Odluka, "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (VetCentarDB db = new VetCentarDB())
                    {
                        var petToDelete = (from p in db.ljubimacs where p.id == ((ljubimac)petsTable.SelectedItem).id select p).First();
                        if (petToDelete != null)
                        {
                            petToDelete.obrisan = true;
                            db.SaveChanges();

                            DisplayPets();
                        }
                    }
                }
                petsTable.SelectedItem = null;
            }
        }

        private void ExaminationButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckSelectedPet())
            {
                new ExaminationsWindow((ljubimac)petsTable.SelectedItem, current).ShowDialog();
                petsTable.SelectedItem = null;
            }
        }

        private void AddPetButton_Click(object sender, RoutedEventArgs e)
        {
            new AddEditPetWindow(null).ShowDialog();
            DisplayPets();
            petsTable.SelectedItem = null;
        }

        private void EditPetButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckSelectedPet())
            {
                new AddEditPetWindow((ljubimac) petsTable.SelectedItem).ShowDialog();
                DisplayPets();
                petsTable.SelectedItem = null;
            }
        }

        private void OwnersButton_Click(object sender, RoutedEventArgs e)
        {
            new OwnersWindow().ShowDialog();
        }

        private void MedicinesButton_Click(object sender, RoutedEventArgs e)
        {
            new MedicinesWindow().ShowDialog();
        }

        private void TypeComboBox_Selected(object sender, RoutedEventArgs e)
        {
            DisplayPetsByType((string)typeComboBox.SelectedItem);
        }

        private bool CheckSelectedPet()
        {
            if (petsTable.SelectedItem == null)
            {
                MessageBox.Show(Properties.Resources.Niste_izabrali_ljubimca);
                return false;
            }
            return true;
        }

        private void TypesButton_Click(object sender, RoutedEventArgs e)
        {
            new PetTypesWindow().ShowDialog();

            DisplayTypes();
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

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            var filteredPets = allPets.Where(pet => pet.ime.ToLower().StartsWith(searchTextBox.Text.ToLower()));
            petsTable.ItemsSource = filteredPets;
        }

        private void LogoutItem_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
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
