using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanificationEmployee
    {
    public class feuilleDeTemps
    {
        public int numeroEmploye;
        static private List<Tuple<int, int>> jour1 = new List<Tuple<int, int>>();
        static private List<Tuple<int, int>> jour2 = new List<Tuple<int, int>>();
        static private List<Tuple<int, int>> jour3 = new List<Tuple<int, int>>();
        static private List<Tuple<int, int>> jour4 = new List<Tuple<int, int>>();
        static private List<Tuple<int, int>> jour5 = new List<Tuple<int, int>>();
        static private List<Tuple<int, int>> weekend1 = new List<Tuple<int, int>>();
        static private List<Tuple<int, int>> weekend2 = new List<Tuple<int, int>>();
        public List<Tuple<int, int>>[] joursOuvrable = { jour1, jour2, jour3, jour4, jour5 };
        public List<Tuple<int, int>>[] weekend = { weekend1, weekend2 };
        public List<Tuple<int, int>>[] semaine = { jour1, jour2, jour3, jour4, jour5, weekend1, weekend2 };

        public int semaineFeuilleDeTemps;
        
        public feuilleDeTemps(JObject jsonObjet)
            {
            jour1.Clear();
            jour2.Clear();
            jour3.Clear();
            jour4.Clear();
            jour5.Clear();
            weekend1.Clear();
            weekend2.Clear();
            numeroEmploye = (int)jsonObjet["numeroEmploye"];
            semaineFeuilleDeTemps = (int)jsonObjet["semaineFeuilleDeTemps"];

            foreach (JToken token in jsonObjet["jour1"])
                jour1.Add(new Tuple<int, int>((int)token["Item1"], (int)token["Item2"]));

            foreach (JToken token in jsonObjet["jour2"])
                jour2.Add(new Tuple<int, int>((int)token["Item1"], (int)token["Item2"]));

            foreach (JToken token in jsonObjet["jour3"])
                jour3.Add(new Tuple<int, int>((int)token["Item1"], (int)token["Item2"]));

            foreach (JToken token in jsonObjet["jour4"])
                jour4.Add(new Tuple<int, int>((int)token["Item1"], (int)token["Item2"]));

            foreach (JToken token in jsonObjet["jour5"])
                jour5.Add(new Tuple<int, int>((int)token["Item1"], (int)token["Item2"]));

            foreach (JToken token in jsonObjet["weekend1"])
                weekend1.Add(new Tuple<int, int>((int)token["Item1"], (int)token["Item2"]));

            foreach (JToken token in jsonObjet["weekend2"])
                weekend2.Add(new Tuple<int, int>((int)token["Item1"], (int)token["Item2"]));
            }

        public feuilleDeTemps(List<rangeFeuilleDeTempsUI> rangesFeuilleDeTemps, int idEmp = 900, int numSemaineFeuilleDeTemps = 1)
            {
            semaineFeuilleDeTemps = numSemaineFeuilleDeTemps;
            numeroEmploye = idEmp;
            jour1.Clear();
            jour2.Clear();
            jour3.Clear();
            jour4.Clear();
            jour5.Clear();
            weekend1.Clear();
            weekend2.Clear();
            foreach (rangeFeuilleDeTempsUI range in rangesFeuilleDeTemps)
                {
                if (range.Lundi != "" && range.Lundi != null)
                    {
                    jour1.Add(new Tuple<int, int>(int.Parse( range.CodeProjet), int.Parse(range.Lundi)));
                    }
                if (range.Mardi != "" && range.Mardi != null)
                    {
                    jour2.Add(new Tuple<int, int>(int.Parse(range.CodeProjet), int.Parse(range.Mardi)));
                    }
                if (range.Mercredi != "" && range.Mercredi != null)
                    {
                    jour3.Add(new Tuple<int, int>(int.Parse(range.CodeProjet), int.Parse(range.Mercredi)));
                    }
                if (range.Jeudi != "" && range.Jeudi != null)
                    {
                    jour4.Add(new Tuple<int, int>(int.Parse(range.CodeProjet), int.Parse(range.Jeudi)));
                    }
                if (range.Vendredi != "" && range.Vendredi != null)
                    {
                    jour5.Add(new Tuple<int, int>(int.Parse(range.CodeProjet), int.Parse(range.Vendredi)));
                    }
                if (range.Samedi != "" && range.Samedi != null)
                    {
                    weekend1.Add(new Tuple<int, int>(int.Parse(range.CodeProjet), int.Parse(range.Samedi)));
                    }
                if (range.Dimanche != "" && range.Dimanche != null)
                    {
                    weekend2.Add(new Tuple<int, int>(int.Parse(range.CodeProjet), int.Parse(range.Dimanche)));
                    }
                }
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
        private Dictionary<string, List<Tuple<int, int>>> convertirJoursEnDictionair() { 
        return  new Dictionary<string, List<Tuple<int, int>>>()  { { "jour1", jour1 },
                                                                   { "jour2", jour2 },
                                                                   { "jour3", jour3 },
                                                                   { "jour4", jour4 },
                                                                   { "jour5", jour5 },
                                                                   { "weekend1", weekend1 },
                                                                   { "weekend2", weekend2 },};                                                
        }

        private Dictionary<string, int> convertirIDEmpEnDictionair()
        {
            return new Dictionary<string, int>()  { { "numeroEmploye", numeroEmploye } };
        }

        private Dictionary<string, int> convertirIDSemEnDictionair()
        {
            return new Dictionary<string, int>() { { "semaineFeuilleDeTemps", semaineFeuilleDeTemps } };
        }

        private JObject convertireFeuilleDeTempsEnJobject() {
            mappeurJson mappeurJson = new mappeurJson();
            JObject partieIDEmp = mappeurJson.getJsonObject(convertirIDEmpEnDictionair());
            JObject partieIDSem = mappeurJson.getJsonObject(convertirIDSemEnDictionair());
            JObject partieJours = mappeurJson.getJsonObject(convertirJoursEnDictionair());
            partieIDEmp.Merge(partieIDSem);
            partieIDEmp.Merge(partieJours);

            return partieIDEmp;

        }
        public Dictionary<int, rangeFeuilleDeTempsUI> convertireFeuilleDeTempsEnListeDeRange()
        {
            Dictionary<int,rangeFeuilleDeTempsUI> rangesFeuilleDeTempsDictionary = new Dictionary<int, rangeFeuilleDeTempsUI>();
            


            for (int i = 0; i < semaine.Count(); i++)
            {   

                foreach (Tuple<int, int> Projet in semaine[i]) {                                          
                        switch (i)
                            {
                            case 0:
                                {
                                if (rangesFeuilleDeTempsDictionary.ContainsKey(Projet.Item1))
                                    rangesFeuilleDeTempsDictionary[Projet.Item1].Lundi = Projet.Item2.ToString();
                                else
                                    rangesFeuilleDeTempsDictionary.Add(Projet.Item1, new rangeFeuilleDeTempsUI { CodeProjet = Projet.Item1.ToString(), Lundi = Projet.Item2.ToString() });
                                break;
                                }
                                
                            case 1:
                                {                                                            
                                if (rangesFeuilleDeTempsDictionary.ContainsKey(Projet.Item1))
                                    rangesFeuilleDeTempsDictionary[Projet.Item1].Mardi = Projet.Item2.ToString();
                                else
                                    rangesFeuilleDeTempsDictionary.Add(Projet.Item1, new rangeFeuilleDeTempsUI { CodeProjet = Projet.Item1.ToString(), Mardi = Projet.Item2.ToString() });
                                break;
                                }
                                
                            case 2:
                                {                                                   
                                if (rangesFeuilleDeTempsDictionary.ContainsKey(Projet.Item1))
                                    rangesFeuilleDeTempsDictionary[Projet.Item1].Mercredi = Projet.Item2.ToString();
                                else
                                    rangesFeuilleDeTempsDictionary.Add(Projet.Item1, new rangeFeuilleDeTempsUI { CodeProjet = Projet.Item1.ToString(), Mercredi = Projet.Item2.ToString() });
                                break;
                                }
                                
                            case 3:
                                {
                                
                             
                                if (rangesFeuilleDeTempsDictionary.ContainsKey(Projet.Item1))
                                    rangesFeuilleDeTempsDictionary[Projet.Item1].Jeudi = Projet.Item2.ToString();
                                else
                                    rangesFeuilleDeTempsDictionary.Add(Projet.Item1, new rangeFeuilleDeTempsUI { CodeProjet = Projet.Item1.ToString(), Jeudi = Projet.Item2.ToString() });
                                break;
                                }
                                
                            case 4:
                                {                                                            
                                if (rangesFeuilleDeTempsDictionary.ContainsKey(Projet.Item1))
                                    rangesFeuilleDeTempsDictionary[Projet.Item1].Vendredi = Projet.Item2.ToString();
                                else
                                    rangesFeuilleDeTempsDictionary.Add(Projet.Item1, new rangeFeuilleDeTempsUI { CodeProjet = Projet.Item1.ToString(), Vendredi = Projet.Item2.ToString() });
                                break;
                                }
                               
                            

                    }
                
                
                }
            }


            return rangesFeuilleDeTempsDictionary;

        }
        public void enregistrerMaFeuilleDeTempeEnFormatJson(string outputFilePath, string fileName) {
            mappeurJson mappeurJson = new mappeurJson();
            mappeurJson.SetJsonFileFromJObject(outputFilePath, fileName, convertireFeuilleDeTempsEnJobject());
        }

        private string AfficherListe(List<Tuple<int, int>> liste)
            {
            if (liste.Count == 0)
                {
                return "      - Aucun projet.\n";
                }
            string s = "";
            foreach (Tuple<int, int> t in liste)
                {
                s += $"       - Projet {t.Item1} : {t.Item2} minutes.\n";
                }
            return s;
            }
        }
    }
