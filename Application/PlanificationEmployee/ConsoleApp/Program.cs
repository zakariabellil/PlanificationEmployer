using System;
using Newtonsoft.Json.Linq;
using PlanificationEmployee;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            mappeurJson mappeur = new mappeurJson();
            Console.WriteLine("Entrez le chemin complet du fichier JSON :");
            string cheminOriginFichier = Console.ReadLine();
            string cheminModifieFichier = cheminOriginFichier.Replace("\\", "\\\\");
            string cheminModifieFichier2 = cheminModifieFichier.Replace("\"", "");
            JObject jsonObjet = mappeur.getDataFromJsonFile(cheminModifieFichier2);
            feuilleDeTemps feuilleDeTemps = new feuilleDeTemps(jsonObjet);
            validateurFeuilleDeTemps validateurFeuilleDeTemps = new validateurFeuilleDeTemps();
            JObject outPutJsonObjet = validateurFeuilleDeTemps.validerFeuilleDeTemps(feuilleDeTemps);
            mappeur.getTxtFromJson("C:\\Users\\15142\\Desktop\\test\\PlanificationEmployer\\Application\\PlanificationEmployee\\ConsoleApp", "test.json", outPutJsonObjet);
            Console.WriteLine(feuilleDeTemps.ToString());
        }
    }
}
