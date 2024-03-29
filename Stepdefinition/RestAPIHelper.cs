﻿using NUnit.Framework.Interfaces;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using APITests.Data;

namespace APITests.Stepdefinition
{

    internal class RestAPIHelper
    {
        private readonly string currentDirectory = Directory.GetParent(NUnit.Framework.TestContext.CurrentContext.TestDirectory).Parent.FullName;
        private readonly JSON read;
        private RestClient rc;
        private RestRequest rq;
        private IRestResponse lastResponse;
        public RestAPIHelper()
        {
            read = new JSON();
        }
        private protected IRestResponse Parameters(string content, string status)
        {
            rc = new RestClient(read.jr("TestData.json", "url"));
            rq = new RestRequest(read.jr("TestData.json", "endpoint"), Method.GET);
            rq.AddHeader("Content-Type", content);
            rq.AddParameter(new Parameter("catalogue",status, ParameterType.GetOrPost));
            lastResponse = rc.Execute(rq);
            return lastResponse;
        }

        [AfterScenario]
        private protected void FailedTestScenario()
        {
            if (NUnit.Framework.TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {

                var dir = $@"{currentDirectory}/Logs";
                Directory.CreateDirectory(dir);
                string pathfile = Path.Combine(currentDirectory + @"//Logs", "Error" + "_" + DateTime.Now.ToString("(dd_MMMM_hh_mm_ss_ffffff_tt).TXT"));
                using (var writer = new StreamWriter(pathfile))
                {
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
