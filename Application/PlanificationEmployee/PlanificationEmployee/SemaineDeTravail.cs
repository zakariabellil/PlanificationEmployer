using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanificationEmployee
{
    class SemaineDeTravail
    {
        public int NumeroEmploye { get; set; }
        public Dictionary<string, JourDeTravail> Jours { get; set; }

        public SemaineDeTravail()
        {
            Jours = new Dictionary<string, JourDeTravail>();
        }
    }
}
