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
    public class SaveDataModel
    {
        // Constructor
        public SaveDataModel()
        {
            FerieData = new ObservableCollection<Rejse>();
        }

        public ObservableCollection<Rejse> FerieData { get; set; }

        public void SaveData()
        {
            // save to xml
            var serializer = new XmlSerializer(typeof(ObservableCollection<Rejse>));

            using (var stream = new FileStream("rejser.xml", FileMode.Create))
            {
                serializer.Serialize(stream, FerieData);
            }
        }
    }
}
