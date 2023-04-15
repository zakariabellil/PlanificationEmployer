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
    }
}
