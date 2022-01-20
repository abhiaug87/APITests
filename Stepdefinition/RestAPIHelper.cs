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

        public RestClient Url()
        {
            var url = read.jr("TestData.json", "url");
            return rc = new RestClient(url);
        }

        public RestRequest parameters(string content, string corr, string reqid, string token, string encoding, string conn)
        {
            rc = new RestClient(read.jr("TestData.json", "url"));
            rq = new RestRequest(read.jr("TestData.json", "endpoint"), Method.GET);
            rq.AddHeader("Content-Type", content);
            rq.AddParameter(new Parameter("X-Correlation-ID", corr, ParameterType.GetOrPost));
            rq.AddParameter(new Parameter("X-Request-ID", reqid, ParameterType.GetOrPost));
            rq.AddParameter(new Parameter("Postman-Token", token, ParameterType.GetOrPost));
            rq.AddParameter(new Parameter("Content-Encoding", encoding, ParameterType.GetOrPost));
            rq.AddParameter(new Parameter("Connection", conn, ParameterType.GetOrPost));
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
