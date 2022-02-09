using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Testing.Data
{
    public class JSON
    {

        public string JR(string fileName, object itemName)
        {
            string reportPath = NUnit.Framework.TestContext.CurrentContext.TestDirectory + "//Data//" + fileName;

            // read JSON directly from a file
            StreamReader file = File.OpenText(reportPath);

            JsonTextReader reader = new JsonTextReader(file);
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);

                dynamic fileContents = JArray.Parse("[" + o2.ToString() + "]");
                _ = fileContents[0];
                var value = o2[itemName].ToString();
                return value;
            }
        }
    }
}