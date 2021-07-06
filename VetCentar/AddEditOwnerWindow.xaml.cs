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
    public partial class AddEditOwnerWindow : Window
    {
        private vlasnik owner;
        private bool edit = false;

        public AddEditOwnerWindow(vlasnik owner)
        {
            InitializeComponent();
            if(owner != null)
            {
                this.owner = owner;
                edit = true;
                Title = Properties.Resources.Izmjena_vlasnika;
                nameTextBox.Text = owner.ime;
                surnameTextBox.Text = owner.prezime;
                emailTextBox.Text = owner.email;
                phoneTextBox.Text = owner.telefon;
            } 
            else
            {
                Title = Properties.Resources.Dodavanje_vlasnika;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(checkName() && checkSurname() && checkPhone() && checkEmail())
            {
                using (VetCentarDB db = new VetCentarDB())
                {
                    if(!edit)
                    {
                        vlasnik ownerToAdd = new vlasnik()
                        {
                            ime = nameTextBox.Text,
                            prezime = surnameTextBox.Text,
                            email = emailTextBox.Text,
                            telefon = phoneTextBox.Text
                        };

                        db.vlasniks.Add(ownerToAdd);
                        db.SaveChanges();
                    }
                    else
                    {
                        var ownerToEdit = (from o in db.vlasniks where o.id == owner.id select o).First();
                        ownerToEdit.ime = nameTextBox.Text;
                        ownerToEdit.prezime = surnameTextBox.Text;
                        ownerToEdit.telefon = phoneTextBox.Text;
                        ownerToEdit.email = emailTextBox.Text;

                        db.SaveChanges();
                    }

                    Close();
                }
            }
            else
            {
                MessageBox.Show(Properties.Resources.Popunjena_polja);
            }
        }

        private bool checkName()
        {
            if("".Equals(nameTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool checkSurname()
        {
            if ("".Equals(surnameTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool checkPhone()
        {
            if ("".Equals(phoneTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool checkEmail()
        {
            if ("".Equals(emailTextBox.Text))
            {
                return false;
            }
            return true;
        }
    }
}
