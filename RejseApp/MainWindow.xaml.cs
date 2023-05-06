using RejseApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace RejseApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        SaveDataModel saveDataModel = new SaveDataModel();
        public ObservableCollection<rejse> rejser { get; set; }

        private bool erDataGemt = false; // Holder styr på om data allerede er gemt

        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists("rejser.xml"))
            {
                // Load XML from bin/debug
                var serializer = new XmlSerializer(typeof(ObservableCollection<rejse>));
                using (var reader = new StreamReader("rejser.xml"))
                {
                    rejser = (ObservableCollection<rejse>)serializer.Deserialize(reader);
                }
            }
            else
            {
                rejser = new ObservableCollection<rejse>();
            }

            // Sets the DataContext of the window to the "Rejser" property -
            // which means that any bindings to this property will update automatically when the collection changes.
            DataContext = rejser;
        }

        private void Btn_OpretNyRejse(object sender, RoutedEventArgs e)
        {
            // Opret nyt vindue
            OpretRejseWindow opretRejseWindow = new OpretRejseWindow();

            bool? result = opretRejseWindow.ShowDialog();

            if (result == true)
            {
                string pris = opretRejseWindow.priceTextBox.Text;
                decimal decimalPris;
                if (Decimal.TryParse(pris, out decimalPris))

                    // Tilføj den nye rejse med de data bruger har indtastet
                    rejser.Add(new rejse
                    {
                        Destination = "Destination: " + opretRejseWindow.destinationTextBox.Text + " - ",
                        Pris = decimalPris,
                        Dato = opretRejseWindow.datePicker.SelectedDate.Value
                    });
                erDataGemt = false;
            }
        }

        private void Btn_SletRejse(object sender, RoutedEventArgs e)
        {
            if (ListBox.SelectedItems != null)
            {
                rejser.Remove(ListBox.SelectedItem as rejse);
                MessageBox.Show("Rejse slettet");
                erDataGemt = false;
            }
            

            if (!erDataGemt)
            {
                // save to xml
                var serializer = new XmlSerializer(typeof(ObservableCollection<rejse>));

                using (var stream = new FileStream("rejser.xml", FileMode.Create))
                {
                    serializer.Serialize(stream, rejser);
                }
                erDataGemt = true;                
            }

        }

        private void btn_SorterPris(object sender, RoutedEventArgs e)
        {
            // prøve at sortere med for loop
            for (int i = 0; i < rejser.Count; i++)
            {

            }

            // Using LINQ - Language Integrated Query - 
            // A Query is a set of instructions that defines what data to retrive from a -
            // given data source, and what form the order the returned data should have.                       

            // Brug af LINQ Library, til at sortere min collection
            ObservableCollection<rejse> sorterEfterPris = new ObservableCollection<rejse>(
                from rejse in rejser
                orderby rejse.Pris
                select rejse);

            // Replace the existing collection with the sorted collection
            rejser = sorterEfterPris;


            //// Brug af LINQ med en Lambda metode. giver samme resultat som ved at bruge SQL syntax
            //var sortedRejser = new ObservableCollection<Rejse>(Rejser.OrderBy(rejse => rejse.Pris));
            //// Replace the existing collection with the sorted collection
            //// Rejser = sortedRejser;

            // Set the DataContext of the window to the sorted collection
            DataContext = rejser;
        }

        private void btn_SorterDato(object sender, RoutedEventArgs e)
        {
            // Brug af LINQ med en Lambda metode. Giver samme resultat som ved at bruge SQL syntax -
            // Som jeg brugte da jeg sorterede efter Pris
            var sortedRejser = new ObservableCollection<rejse>(rejser.OrderBy(rejse => rejse.Dato));

            // Replace the existing collection with the sorted collection
            rejser = sortedRejser;

            // Set the DataContext of the window to the sorted collection
            DataContext = rejser;


        }

        private void btn_GemData(object sender, RoutedEventArgs e)
        {

            // skal dette bruges ??
            for (int i = 0; i < rejser.Count; i++)
            {
                saveDataModel.FerieData += $"{rejser[i].Destination} ";
                saveDataModel.FerieData += $"{rejser[i].Pris} ";
                saveDataModel.FerieData += $"{rejser[i].Dato} {Environment.NewLine}";
                // saveDataModel.FerieData += $"{Rejser[i].Dato.ToShortDateString} {Environment.NewLine}";
            }

            if (!erDataGemt)
            {
                // save to xml
                var serializer = new XmlSerializer(typeof(ObservableCollection<rejse>));                

                using (var stream = new FileStream("rejser.xml", FileMode.Create))                
                {
                    serializer.Serialize(stream, rejser);
                }
                erDataGemt = true;
                MessageBox.Show("Data gemt på hardisk");
            }


        }

        private void btn_SorterAlfabetisk(object sender, RoutedEventArgs e)            
        {            
            
            for (int i = 0;i < rejser.Count; i++)
            {
                for(int j = 0; j < rejser.Count - i - 1; j++)
                {
                    if (string.Compare(rejser[j].Destination, rejser[j+1].Destination, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        rejse temporary = rejser[j];
                        rejser[j] = rejser[j+1];
                        rejser[j+1] = temporary;
                    }
                }
            }
        }
    }
}

