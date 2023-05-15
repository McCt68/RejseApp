using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RejseApp.Repository
{
    internal interface IRepository
    {

        // Her er en slags polymorphism

        // Jeg siger at disse metoder skal implementeres i de klasser som er børn af dette interface.               

        void Save();
        void Load();
    }
}
