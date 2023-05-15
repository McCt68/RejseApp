using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RejseApp.Repository
{
    internal class RepositoryImplHD : IRepository
    {
        // Her implementerer jeg interfacet Repository. Og har metoder til at gemme og hente data fra harddisk
        // Jeg kunne så f.eks lave en anden implementering af Repository interfacet, der arbejder med data fra en sky.

        // Det er lavet som en singleton. Så kan man ikke komme til at skrive til HD fra flere instancer -
        // da der kun er denne ene instans.

        // Constructor er privat så klassen ikke kan instantieres direkte. Den har heller aldrig parametre
        private RepositoryImplHD() { }

        private static RepositoryImplHD _instance;

        public static RepositoryImplHD Instance()
        {
            // Instantier ny instance af  RepositoryImplHD hvis vi ikke har en i forvejen.
            if (_instance == null)
            {
                _instance = new RepositoryImplHD();
            }
            return _instance;
        }


        public void Load()
        {
            throw new NotImplementedException();

        }

        public void Save()
        {
            // throw new NotImplementedException();
            MessageBox.Show("Hej fra Repo Interface singleton");
            
        }
    }
}
