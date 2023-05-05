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
            string cheminOriginFichier = Console.ReadLine().Replace("\"", "\\");
            JObject jsonObjet = mappeur.getDataFromJsonFile(cheminOriginFichier);
            feuilleDeTemps feuilleDeTemps = new feuilleDeTemps(jsonObjet);
            validateurFeuilleDeTemps validateurFeuilleDeTemps = new validateurFeuilleDeTemps();          
            mappeurJson mappeurJson = new mappeurJson();
            JObject outPutJsonObjet = mappeurJson.getJsonObject(validateurFeuilleDeTemps.validerFeuilleDeTemps(feuilleDeTemps));
            Console.WriteLine("Entrez le chemin complet du fichier sortie JSON (sans backslash à la fin):");
            string cheminOriginSortieFichier = Console.ReadLine().Replace("\"", "\\");
            Console.WriteLine("Entrez le nom du fichier sortie JSON (avec .json à la fin) : ");
            string nom = Console.ReadLine();
            mappeur.SetJsonFileFromJObject(cheminOriginSortieFichier, nom, outPutJsonObjet);
            Console.WriteLine(feuilleDeTemps.ToString());
        }
    }
}
