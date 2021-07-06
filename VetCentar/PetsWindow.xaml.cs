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
    /// Interaction logic for PetsWindow.xaml
    /// </summary>
    public partial class PetsWindow : Window
    {
        private vlasnik owner;

        public PetsWindow(vlasnik owner)
        {
            InitializeComponent();
            this.owner = owner;

            DisplayPetsOfOwner();
        }

        private void DisplayPetsOfOwner()
        {
            using (VetCentarDB db = new VetCentarDB())
            {
                var all = (from p in db.ljubimacs where p.obrisan == false && p.vlasnik_id == owner.id select p).ToList();
                petsOfOwnersTable.ItemsSource = all;
            }
        }
    }
}
