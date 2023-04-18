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
            Console.WriteLine(feuilleDeTemps.ToString());
        }
    }
}
