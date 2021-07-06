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
    /// Interaction logic for ExaminationsWindow.xaml
    /// </summary>
    public partial class ExaminationsWindow : Window
    {
        private ljubimac pet;
        private zaposleni current;

        public ExaminationsWindow(ljubimac pet, zaposleni current)
        {
            InitializeComponent();
            this.pet = pet;
            this.current = current;
            DisplayReslts();
        }

        private void DisplayReslts()
        {
            resultsTable.ItemsSource = null;
            resultsTable.Items.Refresh();

            using (VetCentarDB db = new VetCentarDB())
            {
                var allResultsForPet = (from r in db.nalazs where r.ljubimac_id == pet.id select r).ToList();
                resultsTable.ItemsSource = allResultsForPet;
                resultsTable.Items.Refresh();
            }
        }

        private void AddNewResult_Click(object sender, RoutedEventArgs e)
        {
            new MedicalResultWindow(pet, current).ShowDialog();
            DisplayReslts();
        }

        private void TakeMedicine_Click(object sender, RoutedEventArgs e)
        {
            if(CheckSelectedResult())
            {
                new TakingMedicineWindow((nalaz)resultsTable.SelectedItem, current).ShowDialog();
                resultsTable.SelectedItem = null;
            }
        }

        private bool CheckSelectedResult()
        {
            if(resultsTable.SelectedItem == null)
            {
                MessageBox.Show(Properties.Resources.Niste_izabrali_nalaz);
                return false;
            }
            return true;
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckSelectedResult())
            {
                using (VetCentarDB db = new VetCentarDB())
                {
                    var allTakingMedicines = (from m in db.uzimanje_lijeka where m.nalaz_id == ((nalaz)resultsTable.SelectedItem).id select m).ToList(); 
                    ResultDetailsWindow window = new ResultDetailsWindow();
                    uzimanje_lijeka takingMedicine;
                    var vet = (from v in db.zaposlenis where v.id == ((nalaz)resultsTable.SelectedItem).veterinar_id select v).First();
                    window.vetNameLabel.Content += ": " + vet.ime + " " + vet.prezime + " (" + vet.jmbg + ")";

                    for (int i = 1; i <= allTakingMedicines.Count; i++)
                    {
                        takingMedicine = allTakingMedicines.ElementAt(i - 1);
                        window.medicinesTextBox.Text += i + ". " + takingMedicine.lijek.naziv + " (" +  takingMedicine.kolicina + ") - " + takingMedicine.datum_uzimanja + Environment.NewLine;
                    }
                    window.ShowDialog();
                }
                resultsTable.SelectedItem = null;
            }
        }
    }
}
