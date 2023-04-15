using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanificationEmployee
{
    class Employes
    {
        public int Numero { get; set; }
        public List<JourDeTravail> JoursTravailles { get; set; }
        public List<Projet> Projets { get; set; }

        public void Employe()
        {
            JoursTravailles = new List<JourDeTravail>();
            Projets = new List<Projet>();
        }
    }
}
