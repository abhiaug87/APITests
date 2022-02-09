using NUnit.Framework.Interfaces;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using Testing.Data;

namespace Testing.Stepdefinition
{

    public class RestAPIHelper
    {
        readonly string currentDirectory = Directory.GetParent(NUnit.Framework.TestContext.CurrentContext.TestDirectory).Parent.FullName;
        readonly JSON read = new JSON();
        public RestClient rc;
        public RestRequest rq;
        public IRestResponse lastResponse;
        public IRestResponse Parameters(string content, string status)
        {
            rc = new RestClient(read.JR("TestData.json", "url"));
            rq = new RestRequest(read.JR("TestData.json", "endpoint"), Method.GET);
            rq.AddHeader("Content-Type", content);
            rq.AddParameter(new Parameter("catalogue",status, ParameterType.GetOrPost));
            lastResponse = rc.Execute(rq);
            return lastResponse;
        }

        [AfterScenario]
        public void FailedTestScenario()
        {
            if (NUnit.Framework.TestContext.CurrentContext.Result.Outcome != ResultState.Success)
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
