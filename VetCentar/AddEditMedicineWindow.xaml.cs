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
    /// Interaction logic for AddEditMedicineWindow.xaml
    /// </summary>
    public partial class AddEditMedicineWindow : Window
    {
        private lijek medicine;
        private bool edit = false;

        public AddEditMedicineWindow(lijek medicine)
        {
            InitializeComponent();
            this.medicine = medicine;

            if(medicine != null)
            {
                edit = true;
                Title = Properties.Resources.Izmjena_lijeka;

                nameTextBox.Text = medicine.naziv;
                quantityTextBox.Text = "" + medicine.kolicina;
                descriptionTextBox.Text = medicine.opis;
            }
            else
            {
                Title = Properties.Resources.Dodavanje_lijeka;
            }
        }

        private void SaveMedicineButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckName() && CheckQuantity())
            {
                using (VetCentarDB db = new VetCentarDB())
                {
                    if (!edit)
                    {
                        lijek medicineToAdd = new lijek()
                        {
                            naziv = nameTextBox.Text,
                            kolicina = int.Parse(quantityTextBox.Text),
                            opis = descriptionTextBox.Text
                        };
                        db.lijeks.Add(medicineToAdd);
                    }
                    else
                    {
                        var medicineToEdit = (from m in db.lijeks where m.id == medicine.id select m).First();
                        if (medicineToEdit != null)
                        {
                            medicineToEdit.naziv = nameTextBox.Text;
                            medicineToEdit.kolicina = int.Parse(quantityTextBox.Text);
                            medicineToEdit.opis = descriptionTextBox.Text;
                        }
                    }

                    db.SaveChanges();
                    Close();
                }
            }
            else
            {
                MessageBox.Show(Properties.Resources.Popunjena_polja);
            }
        }

        private bool CheckName()
        {
            if("".Equals(nameTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool CheckQuantity()
        {
            if("".Equals(quantityTextBox.Text))
            {
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
                    return false;
                }
            }
            return true;
        }
    }
}
