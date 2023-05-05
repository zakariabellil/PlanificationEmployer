using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace PlanificationEmployee
    {
    public class validateurFeuilleDeTemps
    {
        private int tempsTeleTravail;
        private int tempsTotaleTravail;
        private const int TEMPS_DE_TRAVAIL_MAX = 43 * 60;
        private const int TEMPS_DE_TRAVAIL_MIN_EMP_PRODUCTION = 38 * 60;
        private const int TEMPS_DE_TRAVAIL_MIN_EMP_EXPLOITATION = 40 * 60;
        private const int TEMPS_DE_TRAVAIL_MIN_EMP_ADMINISTRATION = 40 * 60;
        private const int TEMPS_DE_TELETRAVAIL_MAX_EMP_ADMINISTRATION = 0 * 60;
        private const int TEMPS_DE_TRAVAIL_BUREAU_MIN_EMP_PRODUCTION_EXPLOITATION = 6 * 60;
        private const int TEMPS_DE_TRAVAIL_BUREAU_MIN_EMP_ADMINISTRATION = 4 * 60;
        private const int TRAVAIL_BUREAU_LIMIT_CODE = 900;
        private const int ADMINISTRATION_LIMIT_CODE = 1000;
        private const int PRODUCTION_LIMIT_CODE = 2000;
        private const string TELETRAVAIL = "TeleTravail";
        private const string TRAVAILBUREAU = "TravailBureau";
        private const string CONGE_MALADIE = "CongeMaladie";
        private const string CONGE_FERIES = "CongeFeries";
        private string typeEmp;
        Dictionary<int, List<int>> SemaineJourneeFeries = new Dictionary<int, List<int>>();

        private Dictionary<string, string> resultatValidationData = new Dictionary<string, string>();
        public Dictionary<string, string> validerFeuilleDeTemps(feuilleDeTemps feuilleDeTemps)
            {
            initTypeEmployer(feuilleDeTemps.numeroEmploye);
            initSemaineCongesFeries();
            int compteurDeJournee = 1;
            string messageValidation;
            foreach (List<Tuple<int, int>> jour in feuilleDeTemps.joursOuvrable)
                {
                Dictionary<string, int> tempsJourneeData = calculerTempsJourneeTravail(jour);
                messageValidation = validerTravailBureauJournee(tempsJourneeData[TRAVAILBUREAU]);

                if (messageValidation == "")
                    resultatValidationData.Add("jour" + compteurDeJournee.ToString(), "Aucun probleme");
                else
                    resultatValidationData.Add("jour" + compteurDeJournee.ToString(), messageValidation);

                tempsTeleTravail = tempsTeleTravail + tempsJourneeData[TELETRAVAIL];
                tempsTotaleTravail = tempsTotaleTravail + tempsJourneeData[TRAVAILBUREAU] + tempsJourneeData[TELETRAVAIL];

                messageValidation = validerCongeeMaladie(tempsJourneeData, false);            
                if (messageValidation != "")
                    resultatValidationData.Add("jour" + compteurDeJournee.ToString() + " Conges maladie", messageValidation);

                messageValidation = validerCongeeFeries(tempsJourneeData, false, compteurDeJournee, feuilleDeTemps.semaineFeuilleDeTemps);
                if (messageValidation != "")
                    resultatValidationData.Add("jour" + compteurDeJournee.ToString() + " Conges feries", messageValidation);

                compteurDeJournee++;
                }

            foreach (List<Tuple<int, int>> jour in feuilleDeTemps.weekend)
                {
                Dictionary<string, int> tempsJourneeData = calculerTempsJourneeTravail(jour);
                tempsTeleTravail = tempsTeleTravail + tempsJourneeData[TELETRAVAIL];
                tempsTotaleTravail = tempsTotaleTravail + tempsJourneeData[TRAVAILBUREAU] + tempsJourneeData[TELETRAVAIL];
                }

            messageValidation = validerTeleTravailTotal();
            if (messageValidation == "")
                resultatValidationData.Add("MaxTeleTravail", "Aucun probleme");
            else
                resultatValidationData.Add("MaxTeleTravail", messageValidation);

            messageValidation = validerTempsMax();
            if (messageValidation == "")
                resultatValidationData.Add("MaxTotaleTravail", "Aucun probleme");
            else
                resultatValidationData.Add("MaxTotaleTravail", messageValidation);

            messageValidation = validerTravailMinimal();
            if (messageValidation == "")
                resultatValidationData.Add("MinTotaleTravail", "Aucun probleme");
            else
                resultatValidationData.Add("MinTotaleTravail", messageValidation);

            return resultatValidationData;
            }
        private string validerTeleTravailTotal()
            {
            if ((typeEmp == "administration") && tempsTeleTravail > TEMPS_DE_TELETRAVAIL_MAX_EMP_ADMINISTRATION)
                return "Vous avez depasser le nombre d'heurs de teletravail pour un employer en administration";
            return "";
            }
        private string validerTempsMax()
            {
            if (tempsTotaleTravail > TEMPS_DE_TRAVAIL_MAX)
                return "Vous avez depasser le temps maximal qu'on peut travailler dans une semaine";

            return "";
            }

        private string validerTravailBureauJournee(int travailBureau)
            {
            if (typeEmp == "administration" && travailBureau < TEMPS_DE_TRAVAIL_BUREAU_MIN_EMP_ADMINISTRATION)
                return "Vous avez pas fais assez d'heurs bureau dans la journee pour un employee d'administration";
            else if (typeEmp == "production" && travailBureau < TEMPS_DE_TRAVAIL_BUREAU_MIN_EMP_PRODUCTION_EXPLOITATION)
                return "Vous avez pas fais assez d'heurs bureau dans la journee pour un employee de production et d'exploitation";

            return "";
            }

        private string validerTravailMinimal()
            {
            if (typeEmp == "administration" && tempsTotaleTravail < TEMPS_DE_TRAVAIL_MIN_EMP_ADMINISTRATION)
                return "Vous avez pas travailler assez d'heurs pour un employer en  administration";
            else if (typeEmp == "production" && tempsTotaleTravail < TEMPS_DE_TRAVAIL_MIN_EMP_PRODUCTION)
                return "Vous avez pas travailler assez d'heurs pour un employer en production";
            else if (typeEmp == "exploitation" && tempsTotaleTravail < TEMPS_DE_TRAVAIL_MIN_EMP_EXPLOITATION)
                return "Vous avez pas travailler assez d'heurs pour un employer en exploitation";

            return "";
            }

        private void initTypeEmployer(int codeEmp){
            if (codeEmp < ADMINISTRATION_LIMIT_CODE)
                typeEmp = "administration";
            else if (codeEmp < PRODUCTION_LIMIT_CODE)
                typeEmp = "production";
            else
                typeEmp = "exploitation";
        }
        private void initSemaineCongesFeries()
        {
            Calendar cal = CultureInfo.CurrentCulture.Calendar;
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-01-02"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 1 };
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-04-07"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 5 };
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-04-10"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 1 }; 
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-05-22"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 1 };
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-06-23"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 1 };
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-07-03"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 1 };
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-08-07"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 1 };
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-09-04"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 1 };
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-10-02"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 1 };
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-10-09"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 1 };
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-11-13"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 1 };
            SemaineJourneeFeries[cal.GetWeekOfYear(DateTime.Parse("2023-12-25"), CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)] = new List<int>() { 1 ,2 };            
        }
        private bool isCodeCongeMaladie(Tuple<int, int> projet) {
            return (projet.Item1 == 999);         
            }
        private bool isCodeCongeFeries(Tuple<int, int> projet)
        {
            return (projet.Item1 == 998);
        }
        private bool valider24HeursDansUneJournee(int tempsTeleTravailJournee, int tempsTravailBureauJournee){                        
            return ((tempsTeleTravailJournee + tempsTravailBureauJournee) / 60 > 24);
            }

        private string validerCongeeMaladie(Dictionary<string, int> tempsJourneeData,bool isWeekEnd)
        {                        
        if (tempsJourneeData[CONGE_MALADIE] > 0 &&                                //1) Si on a du temps de conges maladie on valide si
                                                                                  //   il respecte les relges.
           (tempsJourneeData[CONGE_MALADIE] != tempsJourneeData[TRAVAILBUREAU] || //2) On dois avoir le
                                                                                  //   meme temps de bueau que
                                                                                  //   le temps maladie sinon
                                                                                  //   cela indique une autre
                                                                                  //   projet que celui de conges maladie
           tempsJourneeData[TELETRAVAIL] > 0 ||                                   // 3) On ne doit pas avoir travaille en teletravail
           isWeekEnd ||                                                           // 4) on ne doit pas avoir conges maladie le weekdend
           tempsJourneeData[CONGE_MALADIE] != 420))                               // 5) on ne loger exactement 420 minutes                               
           
           return "Le congés maladie ne doit pas être dans le weekend et il n'est pas " +
                  "possible de charger plus ou moins de 420 minutes." +
                  "Le weekend ne peux pas etre utiliser pour ce type de conges";

        return "";
        }

        private string validerCongeeFeries(Dictionary<string, int> tempsJourneeData, 
                                           bool isWeekEnd,
                                           int compteurJours,
                                           int semaineFeuilleDeTemps)
        {
            List<int> joursDeSemainFeriesListe;
            bool joursFeriesTrouvee = false;
           
            if (SemaineJourneeFeries.TryGetValue(semaineFeuilleDeTemps, out joursDeSemainFeriesListe)) {
                foreach (int joursDeSemainFeries in joursDeSemainFeriesListe)
                    {
                    if (joursDeSemainFeries == compteurJours)
                        {
                        joursFeriesTrouvee = true;
                        break;
                        }
                    }
            }
          

            
            if (!joursFeriesTrouvee &&
                tempsJourneeData[CONGE_FERIES] > 0 &&
               (tempsJourneeData[CONGE_FERIES] != tempsJourneeData[TRAVAILBUREAU] ||
                tempsJourneeData[CONGE_FERIES] > 0 ||
                isWeekEnd ||
                tempsJourneeData[CONGE_FERIES] != 420))

                return "l'employé doit charger 420 minutes et Il n'est pas permis d'utiliser les congés fériés la fin de semaine." +
                       " Vous devais mettre le conge dans la bonne semaine de travail voici la liste de ces dernier :" +
                       "https://www.canada.ca/fr/agence-revenu/services/impot/jours-feries.html";

            return "";
        }

        private Dictionary<string, int> calculerTempsJourneeTravail(List<Tuple<int, int>> journee)
            {
            int tempsTravailBureauJournee = 0;
            int tempsTeletravailJournee = 0;
            int tempsCongeMaladie = 0;
            int tempsCongeferies = 0;
            foreach (Tuple<int, int> projet in journee)
            {
            if (isCodeCongeMaladie(projet))
                {
                tempsCongeMaladie = projet.Item2;
                tempsTravailBureauJournee = tempsTravailBureauJournee + projet.Item2;
                continue;
                }
            if (isCodeCongeFeries(projet))
                {
                tempsCongeferies = projet.Item2;
                tempsTravailBureauJournee = tempsTravailBureauJournee + projet.Item2;
                continue;
                }

            if (projet.Item1 > TRAVAIL_BUREAU_LIMIT_CODE)
               tempsTeletravailJournee = tempsTeletravailJournee + projet.Item2;
            else
               tempsTravailBureauJournee = tempsTravailBureauJournee + projet.Item2;
            }
            return new Dictionary<string, int>() { { TELETRAVAIL, tempsTeletravailJournee },
                                                   { TRAVAILBUREAU, tempsTravailBureauJournee },
                                                   { CONGE_MALADIE, tempsCongeMaladie },
                                                   { CONGE_FERIES, tempsCongeferies } };
            }
        }
    }