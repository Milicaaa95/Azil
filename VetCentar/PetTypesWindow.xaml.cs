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
    /// Interaction logic for PetTypesWindow.xaml
    /// </summary>
    public partial class PetTypesWindow : Window
    {
        public PetTypesWindow()
        {
            InitializeComponent();

            DisplayTypes();
        }

        private void DisplayTypes()
        {
            typesTable.ItemsSource = null;
            typesTable.Items.Refresh();

            using (VetCentarDB db = new VetCentarDB())
            {
                var allTypes = (from t in db.vrstas select t).ToList();
                typesTable.ItemsSource = allTypes;
                typesTable.Items.Refresh();
            }
        }

        private void EditTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if(CheckSelectedType())
            {
                AddEditTypeWindow window = new AddEditTypeWindow((vrsta) typesTable.SelectedItem);
                window.Title = Properties.Resources.Izmjena_vrste;
                window.ShowDialog();

                DisplayTypes();
            }
        }

        private void AddTypeButton_Click(object sender, RoutedEventArgs e)
        {
            AddEditTypeWindow window = new AddEditTypeWindow(null);
            window.Title = Properties.Resources.Dodavanje_vrste;
            window.ShowDialog();

            DisplayTypes();
        }

        private bool CheckSelectedType()
        {
            if(typesTable.SelectedItem == null)
            {
                MessageBox.Show(Properties.Resources.Niste_izabrali_vrstu);
                return false;
            }
            return true;
        }
    }
}
