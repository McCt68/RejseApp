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

    // Er sat til Main Entry point via StartupUri i App.xaml
    public partial class MainWindow : Window
    {
        public ObservableCollection<Rejse> Rejser { get; set; }

        private bool erDataGemt = false; // Holder styr på om data allerede er gemt
                
        SaveDataModel saveDataModel = new SaveDataModel();
        LoadDataModel loadDataModel = new LoadDataModel();

        // Constructor
        public MainWindow()
        {
            InitializeComponent();

            // Kald LoadDataModel
            loadDataModel.LoadData();

            // Sæt Collection til Data fra LoadDataModel
            Rejser = loadDataModel.FerieDataXML;                               

            // Sæt dataContext for dette vindue til Rejser property -
            // Dette gør at alle bindings i XAML opdaterer automatisk når Rejser listen ændrer sig -
            // Det er fordi at en ObservableCollection implementerer INotifyPropertyChanged interfacet.
            DataContext = Rejser;
        }

        private void Btn_OpretNyRejse(object sender, RoutedEventArgs e)
        {
            // Opret nyt vindue
            OpretRejseWindow opretRejseWindow = new OpretRejseWindow();

            bool? visOpretRejseWindow = opretRejseWindow.ShowDialog();

            if (visOpretRejseWindow == true)
            {
                Rejser.Add(opretRejseWindow.NyRejse);

                erDataGemt = false;
            }
        }

        private void Btn_SletRejse(object sender, RoutedEventArgs e)
        {
            switch (ListBox.SelectedItem)
            {
                case null:
                    MessageBox.Show("Husk at markere Rejse, der skal slettes");
                    break;
                case Rejse selectedRejse:
                    Rejser.Remove(selectedRejse);
                    MessageBox.Show("Rejse slettet");
                    erDataGemt = false;
                    break;
                default:
                    MessageBox.Show("Ugyldigt valg");
                    break;
            }                                             

            if (!erDataGemt)
            {
                // save to xml
                var serializer = new XmlSerializer(typeof(ObservableCollection<Rejse>));

                using (var stream = new FileStream("rejser.xml", FileMode.Create))
                {
                    serializer.Serialize(stream, Rejser);
                }
                erDataGemt = true;
            }

        }

        private void btn_SorterPris(object sender, RoutedEventArgs e)
        {

            // Using LINQ - Language Integrated Query - 
            // A Query is a set of instructions that defines what data to retrive from a -
            // given data source, and what form the order the returned data should have.                       

            // Brug af LINQ Library, til at sortere min collection
            ObservableCollection<Rejse> sorterEfterPris = new ObservableCollection<Rejse>(
                from rejse in Rejser
                orderby rejse.Pris
                select rejse);

            // Replace the existing collection with the sorted collection
            Rejser = sorterEfterPris;

            // Set the DataContext of the window to the sorted collection
            DataContext = Rejser;
        }

        private void btn_SorterDato(object sender, RoutedEventArgs e)
        {
            // Brug af LINQ med en Lambda metode. Giver samme resultat som ved at bruge SQL syntax -
            // Som jeg brugte da jeg sorterede efter Pris
            var sortedRejser = new ObservableCollection<Rejse>(Rejser.OrderBy(rejse => rejse.Dato));

            // Replace the existing collection with the sorted collection
            Rejser = sortedRejser;

            // Set the DataContext of the window to the sorted collection
            DataContext = Rejser;
        }

        private void btn_GemData(object sender, RoutedEventArgs e)
        {
            while (!erDataGemt)
            {
                saveDataModel.FerieData = Rejser;
                saveDataModel.SaveData();
                erDataGemt = true;
                MessageBox.Show("Data gemt på hardisk");
            }            
        }

        private void btn_SorterAlfabetisk(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Rejser.Count; i++)
            {
                // Indre loop i kører igennem antallet af rejser minus det antal gange det ydre loop j har kørt minus 1
                for (int j = 0; j < Rejser.Count - i - 1; j++)
                {
                    // .Compare() sammenligner 2 stings og Returner en int -
                    // Hvis result returnered fra .Compare er mindre end 0. Så er string1 mindre end string2
                    // Hvis result returnered fra .Compare er lig 0. Så er string1 lig string2
                    // Hvis result returnered fra .Compare er større end 0. Så er string1 større end string2
                    // Vi sammenligner så at sige den Rejse loopet er nået til med den næste i rækkefølgen -
                    // Hvis den første Rejse i alfabetisk orden er større end den næste så byt om på dem.
                    if (string.Compare(Rejser[j].Destination, Rejser[j + 1].Destination, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        Rejse temporary = Rejser[j]; 
                        Rejser[j] = Rejser[j + 1];
                        Rejser[j + 1] = temporary;
                    }
                }
            }
        }
    }
}

