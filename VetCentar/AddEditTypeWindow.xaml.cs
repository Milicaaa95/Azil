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
    /// Interaction logic for AddEditTypeWindow.xaml
    /// </summary>
    public partial class AddEditTypeWindow : Window
    {
        private vrsta type;
        private bool edit = false;
      
        public AddEditTypeWindow(vrsta type)
        {
            InitializeComponent();
            this.type = type;
            Title = Properties.Resources.Dodavanje_vrste;

            if(type != null)
            {
                edit = true;
                nameTextBox.Text = type.naziv;
                Title = Properties.Resources.Izmjena_vrste;
            }
        }

        private void SaveTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckName())
            {
                using (VetCentarDB db = new VetCentarDB())
                {
                    if (!edit)
                    {
                        vrsta typeToAdd = new vrsta()
                        {
                            naziv = nameTextBox.Text
                        };

                        db.vrstas.Add(typeToAdd);
                    }
                    else
                    {
                        var typeToEdit = (from t in db.vrstas where t.id == type.id select t).First();
                        if (typeToEdit != null)
                        {
                            typeToEdit.naziv = nameTextBox.Text;
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
    }
}
