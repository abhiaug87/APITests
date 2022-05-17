using NUnit.Framework.Interfaces;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using Testing.Data;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Testing.Stepdefinition
{

    public class RestAPIHelper
    {
        private readonly string currentDirectory;
        private readonly JSON read;
        public RestClient rc;
        public RestRequest rq;
        public RestResponse lastResponse;

        public RestAPIHelper()

        {
            currentDirectory = Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.FullName;
            read = new JSON();
        }
        public async Task<RestResponse> Parameters(string content, string status)
        {
            rc = new RestClient(read.JR("TestData.json", "url"));
            rq = new RestRequest(read.JR("TestData.json", "endpoint"), Method.Get);
            rq.AddHeader("Content-Type", content);
            rq.AddParameter("catalogue", status, ParameterType.RequestBody);
            lastResponse = await rc.ExecuteAsync(rq);
            return lastResponse;
        }

        [AfterScenario]
        public void FailedTestScenario()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {

                var dir = $@"{currentDirectory}/Logs";
                Directory.CreateDirectory(dir);
                string pathfile = Path.Combine(currentDirectory + @"//Logs", "Error" + "_" + DateTime.Now.ToString("(dd_MMMM_hh_mm_ss_ffffff_tt).TXT"));
                using (var writer = new StreamWriter(pathfile))
                {
                    writer.WriteLine("Correlation:" + lastResponse.Headers.Where(x => x.Name == "Xero-Correlation-Id").Select(x => x.Value).FirstOrDefault());
                    writer.Write(lastResponse.Content.ToString());
                }
            }
            else
            {
                Console.WriteLine("Test has passed");
            }
        }

    }
}
