using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanificationEmployee
{
    public class validateurFeuilleDeTemps
    {
        private int tempsTeleTravail;
        private int tempsTotaleTravail;
        private const int TEMPS_DE_TRAVAIL_MAX = 43 * 60;
        private const int TEMPS_DE_TRAVAIL_MIN_EMP_NORMAL = 38 * 60;
        private const int TEMPS_DE_TRAVAIL_MIN_EMP_ADMINISTRATION = 36 * 60;
        private const int TEMPS_DE_TELETRAVAIL_MAX_EMP_ADMINISTRATION = 10 * 60;
        private const int TEMPS_DE_TRAVAIL_BUREAU_MIN_EMP_NORMAL = 6 * 60;
        private const int TEMPS_DE_TRAVAIL_BUREAU_MIN_EMP_ADMINISTRATION = 4 * 60;
        private const int TRAVAIL_BUREAU_LIMIT_CODE = 900;
        private const int ADMINISTRATION_LIMIT_CODE = 1000;
        private const string TELETRAVAIL = "TeleTravail";
        private const string TRAVAILBUREAU = "TravailBureau";
        private string typeEmp;

        private Dictionary<string, string> resultatValidationData = new Dictionary<string, string>();
        public JObject validerFeuilleDeTemps(feuilleDeTemps feuilleDeTemps)
        {
            initTypeEmployer(feuilleDeTemps.numeroEmploye);
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

                tempsTeleTravail = tempsTeleTravail + tempsJourneeData[TRAVAILBUREAU] + tempsJourneeData[TELETRAVAIL];
                tempsTotaleTravail = tempsTotaleTravail + tempsJourneeData[TELETRAVAIL];
                compteurDeJournee++;
            }


            foreach (List<Tuple<int, int>> jour in feuilleDeTemps.weekend)
            {
                Dictionary<string, int> tempsJourneeData = calculerTempsJourneeTravail(jour);
                tempsTeleTravail = tempsTeleTravail + tempsJourneeData[TRAVAILBUREAU] + tempsJourneeData[TELETRAVAIL];
                tempsTotaleTravail = tempsTotaleTravail + tempsJourneeData[TELETRAVAIL];
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

            mappeurJson mappeurJson = new mappeurJson();
            return mappeurJson.getOutPutJson(resultatValidationData);
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
            else if (typeEmp == "normal" && travailBureau < TEMPS_DE_TRAVAIL_BUREAU_MIN_EMP_NORMAL)
                return "Vous avez pas fais assez d'heurs bureau dans la journee pour un employee normal";

            return "";
        }
        private string validerTravailMinimal()
        {
            if (typeEmp == "administration" && tempsTotaleTravail < TEMPS_DE_TRAVAIL_MIN_EMP_ADMINISTRATION)
                return "Vous avez pas travailler assez d'heurs pour un employer en  administration";
            else if (typeEmp == "normal" && tempsTotaleTravail < TEMPS_DE_TRAVAIL_MIN_EMP_NORMAL)
                return "Vous avez pas travailler assez d'heurs pour un employer en  normal";

            return "";
        }
        private void initTypeEmployer(int codeEmp)
        {
            typeEmp = (codeEmp < ADMINISTRATION_LIMIT_CODE) ? "administration" : "normal";
        }

        private Dictionary<string, int> calculerTempsJourneeTravail(List<Tuple<int, int>> journee)
        {
            int tempsTravailBureauJournee = 0;
            int tempsTeletravailJournee = 0;
            foreach (Tuple<int, int> projet in journee)
            {
                if (projet.Item1 > TRAVAIL_BUREAU_LIMIT_CODE)
                    tempsTeletravailJournee = tempsTeletravailJournee + projet.Item2;
                else
                    tempsTravailBureauJournee = tempsTravailBureauJournee + projet.Item2;
            }
            return new Dictionary<string, int>() { { TELETRAVAIL, tempsTeletravailJournee },
                                                   { TRAVAILBUREAU, tempsTravailBureauJournee } };
        }
    }
}
