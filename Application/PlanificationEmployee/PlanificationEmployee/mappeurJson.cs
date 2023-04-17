using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PlanificationEmployee
{
public class mappeurJson
    {
    public JObject getDataFromJsonFile(string inputFilePath) {
        string jsonString = File.ReadAllText(inputFilePath);
        JObject jsonObjet = JsonConvert.DeserializeObject<JObject>(jsonString);
        return jsonObjet;
        
        }
  public JObject getOutPutJson(Dictionary<string, string> data) {        
            return JObject.FromObject(data);
        }
    }
}
