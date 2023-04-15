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
            JObject jsonObjet = mappeur.getDataFromJsonFile("C:\\Users\\15142\\Downloads\\json_1\\FeuilleTempsExemple.json");
            feuilleDeTemps feuilleDeTemps = new feuilleDeTemps(jsonObjet);
            Console.WriteLine("Hello, world!");
        }
    }
}
