using System.Collections.Generic;

namespace PlanificationEmployee
{
    public class JourDeTravail
    {
        public int NumeroJour { get; set; }
        public List<int> Projets { get; set; }
        public int TotalMinutes { get; set; }

        public JourDeTravail(int numeroJour)
        {
            NumeroJour = numeroJour;
            Projets = new List<int>();
        }
    }

}