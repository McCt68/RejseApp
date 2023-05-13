using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RejseApp.Models
{
    class LoadDataModel
    {
        public ObservableCollection<Rejse> FerieDataXML { get; set; }

        public void LoadData()
        {
            // Load XML fra current working directory ( hvor .exe befinder sig )
            if (File.Exists("rejser.xml"))
            {
                // Load XML from bin/debug/net6.0-windows
                var serializer = new XmlSerializer(typeof(ObservableCollection<Rejse>));
                using (var reader = new StreamReader("rejser.xml"))
                {
                    // sæt til fil fundet på HD
                    FerieDataXML = (ObservableCollection<Rejse>)serializer.Deserialize(reader);

                }
            }
            else FerieDataXML = new ObservableCollection<Rejse>(); // sæt til ny tom collection
        }

    }
}
