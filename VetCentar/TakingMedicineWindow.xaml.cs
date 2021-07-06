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
    /// Interaction logic for TakingMedicineWindow.xaml
    /// </summary>
    public partial class TakingMedicineWindow : Window
    {
        private nalaz result;
        private zaposleni current;
        private List<lijek> allMedicines;

        public TakingMedicineWindow(nalaz result, zaposleni current)
        {
            InitializeComponent();
            this.result = result;
            this.current = current;
            DisplayMedicines();
        }

        private void DisplayMedicines()
        {
            medicinesTable.ItemsSource = null;
            medicinesTable.Items.Refresh();

            using (VetCentarDB db = new VetCentarDB())
            {
                allMedicines = (from m in db.lijeks where m.kolicina > 0 select m).ToList();
                medicinesTable.ItemsSource = allMedicines;
                medicinesTable.Items.Refresh();
            }
        }

        private void TakeMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckSelectedMedicine() && CheckQuantity())
            {
                lijek selected = (lijek)medicinesTable.SelectedItem;
                using (VetCentarDB db = new VetCentarDB())
                {
                    uzimanje_lijeka takingMedicine = new uzimanje_lijeka()
                    {
                        nalaz_id = result.id,
                        veterinar_id = current.id,
                        lijek_id = selected.id,
                        datum_uzimanja = DateTime.Now,
                        kolicina = int.Parse(quantityTextBox.Text)
                    };
                    db.uzimanje_lijeka.Add(takingMedicine);
                    db.SaveChanges();
                }
                DisplayMedicines();
                quantityTextBox.Text = "";
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

        private bool CheckQuantity()
        {
            if("".Equals(quantityTextBox.Text))
            {
                MessageBox.Show(Properties.Resources.Nema_vrijednosti);
                return false;
            }
            else
            {
                try
                {
                    int.Parse(quantityTextBox.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(Properties.Resources.Nije_broj);
                    return false;
                }
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
