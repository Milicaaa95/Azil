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
    /// Interaction logic for MedicinesWindow.xaml
    /// </summary>
    public partial class MedicinesWindow : Window
    {
        private List<lijek> allMedicines;

        public MedicinesWindow()
        {
            InitializeComponent();

            DisplayMedicines();
        }

        private void DisplayMedicines()
        {
            medicinesTable.ItemsSource = null;
            medicinesTable.Items.Refresh();

            using (VetCentarDB db = new VetCentarDB())
            {
                allMedicines = (from m in db.lijeks select m).ToList();
                medicinesTable.ItemsSource = allMedicines;
                medicinesTable.Items.Refresh();
            }
        }

        private void AddMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditMedicineWindow window = new AddEditMedicineWindow(null);
            window.Title = Properties.Resources.Dodavanje_lijeka;
            window.ShowDialog();

            DisplayMedicines();
        }

        private void EditMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckSelectedMedicine())
            {
                AddEditMedicineWindow window = new AddEditMedicineWindow((lijek) medicinesTable.SelectedItem);
                window.Title = Properties.Resources.Izmjena_lijeka;
                window.ShowDialog();

                DisplayMedicines();
            }
        }

        private bool CheckSelectedMedicine()
        {
            if(medicinesTable.SelectedItem == null)
            {
                MessageBox.Show(Properties.Resources.Niste_izabrali_lijek);
                return false;
            }
            return true;
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            var filteredMedicines = allMedicines.Where(medicine => medicine.naziv.ToLower().StartsWith(searchTextBox.Text.ToLower()));
            medicinesTable.ItemsSource = filteredMedicines;
        }
    }
}
