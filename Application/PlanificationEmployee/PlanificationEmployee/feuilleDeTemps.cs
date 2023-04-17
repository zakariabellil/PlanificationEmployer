using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanificationEmployee {
    public class feuilleDeTemps {
        public int numeroEmploye;
        public List<Tuple<int, int>> jour1 = new List<Tuple<int, int>>();
        public List<Tuple<int, int>> jour2 = new List<Tuple<int, int>>();
        public List<Tuple<int, int>> jour3 = new List<Tuple<int, int>>();
        public List<Tuple<int, int>> jour4 = new List<Tuple<int, int>>();
        public List<Tuple<int, int>> jour5 = new List<Tuple<int, int>>();
        public List<Tuple<int, int>> weekend1 = new List<Tuple<int, int>>();
        public List<Tuple<int, int>> weekend2 = new List<Tuple<int, int>>();
        public List<Tuple<int, int>>[] joursOuvrable = {jour1, jour2, jour3, jour4, jour5, weekend1, weekend2 };
        public List<Tuple<int, int>>[] weekend = {weekend1, weekend2};
        public feuilleDeTemps(JObject jsonObjet) {
            numeroEmploye = (int)jsonObjet["numeroEmploye"];
            foreach (JToken token in jsonObjet["jour1"])               
                jour1.Add(new Tuple<int, int>((int)token["projet"], (int)token["minutes"]));                
       
            foreach (JToken token in jsonObjet["jour2"])               
                jour2.Add(new Tuple<int, int>((int)token["projet"], (int)token["minutes"]));               

            foreach (JToken token in jsonObjet["jour3"])
                jour3.Add(new Tuple<int, int>((int)token["projet"], (int)token["minutes"]));

            foreach (JToken token in jsonObjet["jour4"])
                jour4.Add(new Tuple<int, int>((int)token["projet"], (int)token["minutes"]));

            foreach (JToken token in jsonObjet["jour5"])
                jour5.Add(new Tuple<int, int>((int)token["projet"], (int)token["minutes"]));

            foreach (JToken token in jsonObjet["weekend1"])
                weekend1.Add(new Tuple<int, int>((int)token["projet"], (int)token["minutes"]));
         
            foreach (JToken token in jsonObjet["weekend2"])
                weekend2.Add(new Tuple<int, int>((int)token["projet"], (int)token["minutes"]));


        }
         public override string ToString()
        {
            string s = $"Feuille de temps de l'employé n°{numeroEmploye} :\n";
            s += "Jour 1 :\n";
            s += AfficherListe(jour1);
            s += "Jour 2 :\n";
            s += AfficherListe(jour2);
            s += "Jour 3 :\n";
            s += AfficherListe(jour3);
            s += "Jour 4 :\n";
            s += AfficherListe(jour4);
            s += "Jour 5 :\n";
            s += AfficherListe(jour5);
            s += "Week-end 1 :\n";
            s += AfficherListe(weekend1);
            s += "Week-end 2 :\n";
            s += AfficherListe(weekend2);
            return s;
        }

        private string AfficherListe(List<Tuple<int, int>> liste)
        {
            if (liste.Count == 0)
            {
                return "- Aucun projet.\n";
            }
            string s = "";
            foreach (Tuple<int, int> t in liste)
            {
                s += $"- Projet {t.Item1} : {t.Item2} minutes.\n";
            }
            return s;
        }
    }
}
