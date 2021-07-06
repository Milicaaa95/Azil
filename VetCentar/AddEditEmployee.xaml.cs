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
    /// Interaction logic for AddEditEmployee.xaml
    /// </summary>
    public partial class AddEditEmployee : Window
    {
        private bool edit = false;
        private zaposleni employee;

        public AddEditEmployee()
        {
            InitializeComponent();
            Title = Properties.Resources.Dodavanje_zaposlenog;
            passwordLabel.Content = Properties.Resources.Lozinka;

        }

        public AddEditEmployee(zaposleni z)
        {
            InitializeComponent();

            this.employee = z;

            nameTextBox.Text = z.ime;
            surnameTextBox.Text = z.prezime;
            jmbgTextBox.Text = z.jmbg;
            addressTextBox.Text = z.adresa;
            phoneTextBox.Text = z.telefon;
            qualificationTextBox.Text = z.strucna_sprema;
            usernameTextBox.Text = z.korisnicko_ime;

            edit = true;
            Title = Properties.Resources.Izmjena_zaposlenog;
            passwordLabel.Content = Properties.Resources.Nova_lozinka;
            adminLabel.Visibility = Visibility.Hidden;
            adminCheckBox.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckUsername() && CheckName() && CheckSurname() && CheckJmbg() && CheckPhone() && CheckAddress() && CheckQualification())
            {
                if (!edit)
                {
                    if (CheckPassword())
                    {
                        using (VetCentarDB db = new VetCentarDB())
                        {
                            zaposleni z = new zaposleni();
                            z.ime = nameTextBox.Text;
                            z.prezime = surnameTextBox.Text;
                            z.adresa = addressTextBox.Text;
                            z.telefon = phoneTextBox.Text;
                            z.jmbg = jmbgTextBox.Text;
                            z.strucna_sprema = qualificationTextBox.Text;
                            z.korisnicko_ime = usernameTextBox.Text;
                            z.lozinka = VetCentarUtilities.GetSHA256(passwordBox.Password);
                            z.aktivan = true;
                            db.zaposlenis.Add(z);
                            db.SaveChanges();

                            if(adminCheckBox.IsChecked == true)
                            {
                                administrator a = new administrator();
                                a.zaposleni_id = z.id;
                                db.administrators.Add(a);
                                db.SaveChanges();
                            }
                            else
                            {
                                veterinar v = new veterinar();
                                v.zaposleni_id = z.id;
                                db.veterinars.Add(v);
                                db.SaveChanges();
                            }

                            Close();
                        }
                    }
                }
                else
                {
                    using (VetCentarDB db = new VetCentarDB())
                    {
                        var employeeToEdit = (from z in db.zaposlenis where z.id == employee.id select z).First();

                        if(employeeToEdit != null)
                        {
                            employeeToEdit.ime = nameTextBox.Text;
                            employeeToEdit.prezime = surnameTextBox.Text;
                            employeeToEdit.jmbg = jmbgTextBox.Text;
                            employeeToEdit.telefon = phoneTextBox.Text;
                            employeeToEdit.adresa = addressTextBox.Text;
                            employeeToEdit.strucna_sprema = qualificationTextBox.Text;
                            employeeToEdit.korisnicko_ime = usernameTextBox.Text;

                            if(CheckPassword())
                            {
                                employeeToEdit.lozinka = VetCentarUtilities.GetSHA256(passwordBox.Password);
                            }

                            db.SaveChanges();
                            Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(Properties.Resources.Popunjena_polja);
            }
        }

        private bool CheckUsername()
        {
            if("".Equals(usernameTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool CheckPassword()
        {
            if("".Equals(passwordBox.Password))
            {
                return false;
            }
            return true;
        }

        private bool CheckName()
        {
            if("".Equals(nameTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool CheckSurname()
        {
            if ("".Equals(surnameTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool CheckJmbg()
        {
            if ("".Equals(jmbgTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool CheckAddress()
        {
            if ("".Equals(addressTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool CheckPhone()
        {
            if ("".Equals(phoneTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private bool CheckQualification()
        {
            if ("".Equals(qualificationTextBox.Text))
            {
                return false;
            }
            return true;
        }

    }
}
