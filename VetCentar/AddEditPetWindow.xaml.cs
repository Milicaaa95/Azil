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
    /// Interaction logic for AddEditPetWindow.xaml
    /// </summary>
    public partial class AddEditPetWindow : Window
    {
        private ljubimac pet;
        private bool edit = false;
        private List<vlasnik> allOwners;
        private List<vrsta> allTypes;

        public AddEditPetWindow(ljubimac pet)
        {
            InitializeComponent();
            this.pet = pet;

            ownerComboBox.ItemsSource = AllOwners();
            typeComboBox.ItemsSource = AllTypes();
            Title = Properties.Resources.Dodavanje_ljubimca;

            if (pet != null)
            {
                edit = true;
                Title = Properties.Resources.Izmjena_ljubimca;

                nameTextBox.Text = pet.ime;
                kindTextBox.Text = pet.rasa;
                heightTextBox.Text = pet.visina.ToString();
                weightTextBox.Text = pet.tezina.ToString();
                dateBirth.SelectedDate = pet.datum_rodjenja;

                if("M".Equals(pet.pol))
                {
                    maleRadioButton.IsChecked = true;
                }
                else
                {
                    femaleRadioButton.IsChecked = true;
                }

                using (VetCentarDB db = new VetCentarDB())
                {
                    var owner = (from o in db.vlasniks where o.id == pet.vlasnik_id select o).First();
                    var type = (from t in db.vrstas where t.id == pet.vrsta_id select t).First();
                    if(owner != null && type != null)
                    {
                        ownerComboBox.SelectedIndex = allOwners.IndexOf(owner);
                        typeComboBox.SelectedIndex = allTypes.IndexOf(type);
                    }
                }
            }
        }

        private List<vrsta> AllTypes()
        {
            using (VetCentarDB db = new VetCentarDB())
            {
                allTypes = (from k in db.vrstas select k).ToList();
            }

            return allTypes;
        }

        private List<vlasnik> AllOwners()
        {
            using (VetCentarDB db = new VetCentarDB())
            {
                allOwners = (from o in db.vlasniks select o).ToList();
            }

            return allOwners;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if(CheckName() && CheckKind() && CheckHeight() && CheckWeight() && CheckDateBirth() && CheckMale() && CheckOwner() && CheckType())
            {
                using (VetCentarDB db = new VetCentarDB())
                {
                    if (!edit)
                    {
                        ljubimac pet = new ljubimac()
                        {
                            ime = nameTextBox.Text,
                            rasa = kindTextBox.Text,
                            visina = double.Parse(heightTextBox.Text),
                            tezina = double.Parse(weightTextBox.Text),
                            datum_rodjenja = (DateTime)dateBirth.SelectedDate.Value.Date,
                            pol = maleRadioButton.IsChecked == true ? "M" : "Ž",
                            vlasnik_id = ((vlasnik)ownerComboBox.SelectedItem).id,
                            vrsta_id = ((vrsta)typeComboBox.SelectedItem).id,
                        };

                        db.ljubimacs.Add(pet);
                    } 
                    else
                    {
                        var petToEdit = (from p in db.ljubimacs where p.id == pet.id select p).First();
                        if (petToEdit != null)
                        {
                            petToEdit.ime = nameTextBox.Text;
                            petToEdit.rasa = kindTextBox.Text;
                            petToEdit.visina = double.Parse(heightTextBox.Text);
                            petToEdit.tezina = double.Parse(weightTextBox.Text);
                            petToEdit.datum_rodjenja = (DateTime)dateBirth.SelectedDate.Value.Date;
                            petToEdit.pol = maleRadioButton.IsChecked == true ? "M" : "Ž";
                            petToEdit.vlasnik_id = ((vlasnik)ownerComboBox.SelectedItem).id;
                            petToEdit.vrsta_id = ((vrsta)typeComboBox.SelectedItem).id;
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
            if ("".Equals(nameTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool CheckHeight()
        {
            if ("".Equals(heightTextBox.Text))
            {
                return false;
            }
            else
            {
                try
                {
                    double.Parse(heightTextBox.Text);
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckWeight()
        {
            if ("".Equals(weightTextBox.Text))
            {
                return false;
            }
            try
            {
                double.Parse(weightTextBox.Text);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private bool CheckKind()
        {
            if("".Equals(kindTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool CheckDateBirth()
        {
            if (dateBirth.SelectedDate == null)
            {
                return false;
            }
            return true;
        }

        private bool CheckMale()
        {
            if (maleRadioButton.IsChecked == false && femaleRadioButton.IsChecked == false)
            {
                return false;
            }
            return true;
        }

        private bool CheckOwner()
        {
            if (ownerComboBox.SelectedItem == null)
            {
                return false;
            }
            return true;
        }

        private bool CheckType()
        {
            if(typeComboBox.SelectedItem == null)
            {
                return false;
            }
            return true;
        }
    }
}
