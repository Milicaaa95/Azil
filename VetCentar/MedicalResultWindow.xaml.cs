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
    /// Interaction logic for MedicalResultWindow.xaml
    /// </summary>
    public partial class MedicalResultWindow : Window
    {
        private ljubimac pet;
        private zaposleni current;

        public MedicalResultWindow(ljubimac pet, zaposleni current)
        {
            InitializeComponent();
            this.pet = pet;
            this.current = current;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            nalaz result = new nalaz()
            {
                datum_pregleda = DateTime.Now,
                dijagnoza = opinionTextBox.Text,
                veterinar_id = current.id,
                ljubimac_id = pet.id
            };

            using (VetCentarDB db = new VetCentarDB())
            {
                db.nalazs.Add(result);
                db.SaveChanges();
                Close();
            }
        }
    }
}
