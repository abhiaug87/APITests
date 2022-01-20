using NUnit.Framework.Interfaces;
using RestSharp;
using System;
using System.IO;
using TechTalk.SpecFlow;
using Testing.Data;

namespace Testing.Stepdefinition
{

    public class RestAPIHelper
    {
        JSON read = new JSON();
        public RestClient rc;
        public RestRequest rq;

        public RestRequest parameters(string content, string status)
        {
            rc = new RestClient(read.jr("TestData.json", "url"));
            rq = new RestRequest(read.jr("TestData.json", "endpoint"), Method.GET);
            rq.AddHeader("Content-Type", content);
            rq.AddParameter(new Parameter("catalogue",status, ParameterType.GetOrPost));
            return rq;
        }

        public IRestResponse GetResponse()
        {
            var response = rc.Execute(rq);
            return response;
        }

        [AfterScenario]
        public void failedTestScenario()
        {
            if (NUnit.Framework.TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {

                string p = Directory.GetParent(NUnit.Framework.TestContext.CurrentContext.TestDirectory).Parent.FullName;
                var dir = $@"{p}/Logs";
                Directory.CreateDirectory(dir);
                string pathfile = Path.Combine(p + @"\\Logs", "Error.TXT" + "_" + DateTime.Now.ToString("(dd_MMMM_hh_mm_ss_ffffff_tt)"));
                StreamWriter writer = new StreamWriter(pathfile);
                var response = rc.Execute(rq);
                writer.Write(response.Content.ToString());
                writer.Close();
            }
        }

    }
}
