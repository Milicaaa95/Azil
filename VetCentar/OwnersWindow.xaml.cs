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

namespace VetCentar
{
    /// <summary>
    /// Interaction logic for OwnersWindow.xaml
    /// </summary>
    public partial class OwnersWindow : Window
    {
        private List<vlasnik> allOwners;

        public OwnersWindow()
        {
            InitializeComponent();
            DisplayOwners();
        }


        private void DisplayOwners()
        {
            ownersTable.ItemsSource = null;
            ownersTable.Items.Refresh();
            using (VetCentarDB db = new VetCentarDB())
            {
                allOwners = (from o in db.vlasniks select o).ToList();
                ownersTable.ItemsSource = allOwners;
                ownersTable.Items.Refresh();
            }
        }

        private void AddOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            new AddEditOwnerWindow(null).ShowDialog();
            DisplayOwners();
            ownersTable.SelectedItem = null;
        }

        private void EditOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckSelectedOwner())
            {
                new AddEditOwnerWindow((vlasnik)ownersTable.SelectedItem).ShowDialog();
                DisplayOwners();
                ownersTable.SelectedItem = null;
            }
        }

        private void AllPetsButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckSelectedOwner())
            {
                new PetsWindow((vlasnik) ownersTable.SelectedItem).ShowDialog();
            }
        }

        private bool CheckSelectedOwner()
        {
            if(ownersTable.SelectedItem != null)
            {
                return true;
            }
            MessageBox.Show(Properties.Resources.Niste_izabrali_vlasnika);
            return false;
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            var filteredOwners = allOwners.Where(owner => owner.ime.ToLower().StartsWith(searchTextBox.Text.ToLower()));
            ownersTable.ItemsSource = filteredOwners;
        }
    }
}
