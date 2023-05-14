using RejseApp.Models;
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

namespace RejseApp
{
    /// <summary>
    /// Interaction logic for OpretRejseWindow.xaml
    /// </summary>
    public partial class OpretRejseWindow : Window
    {
        public Rejse NyRejse { get; private set; }
        public OpretRejseWindow()
        {
            InitializeComponent();

            destinationTextBox.Focus();
            datePicker.SelectedDate = DateTime.Today;
        }
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(destinationTextBox.Text)
                || string.IsNullOrWhiteSpace(prisTextBox.Text)
                || datePicker.SelectedDate == null)
            {
                MessageBox.Show("Husk at skrive alle værdier.");
                return;
            }

            // Sandt hvis Text ikke kan converteres til decimal
            if (!decimal.TryParse(prisTextBox.Text, out decimal price))
            {
                MessageBox.Show("Pris skal være et tal.");
                return;
            }

            NyRejse = new Rejse
            {
                Destination = $"{destinationTextBox.Text}  - ",
                Pris = price,
                Dato = datePicker.SelectedDate.Value
            };

            // DialogResult = true;
            this.DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // DialogResult = false;
            this.DialogResult = false;
            Close();
        }
    }
}
