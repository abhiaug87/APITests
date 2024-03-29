﻿using System.Net;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using APITests.Data;

namespace APITests.Stepdefinition
{
    [Binding]
    internal class Stepdefinition : RestAPIHelper
    {
        private readonly JSON read;
        public Stepdefinition() {
            read = new JSON();
        }
        

        [When(@"I pass headers for (.*) and (.*)")]
        private void WhenIPassHeadersForFalseAndApplicationJson(string content, string status)
        {
            Parameters(content, status);
        }


        [Then(@"I am able to see the category name with headers (.*) and (.*)")]
        private void ThenIAmAbleToSeeTheCategoryNameWithHeadersAnd(string content, string status)
        {
            var response = Parameters(content, status);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "The response has failed");
            Assert.That(response.ContentType, Is.EqualTo(content));
            JObject result = JObject.Parse(response.Content);
            string value = result[read.jr("TestData.json", "AC1")].Value<string>();
            Assert.That(value.Equals(read.jr("TestData.json", "V1")), value, "The value is not as expected");
        }

        [Then(@"I am able to see the canrelist status with headers (.*) and (.*)")]
        private void ThenIAmAbleToSeeTheCanrelistStatusWithHeadersAnd(string content, string status)
        {
            var response = Parameters(content, status);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "The response has failed");
            Assert.That(response.ContentType, Is.EqualTo(content));
            JObject result = JObject.Parse(response.Content);
            string value = result[read.jr("TestData.json", "AC2")].Value<string>();
            Assert.That(value.Equals(read.jr("TestData.json", "V2")), value, "The value is not as expected");
        }

        [Then(@"I am able to see the promotions name with headers (.*) and (.*)")]
        private void ThenIAmAbleToSeeThePromotionsNameWithHeadersAnd(string content, string status)
        {
            var response = Parameters(content, status);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "The response has failed");
            Assert.That(response.ContentType, Is.EqualTo(content));
            JObject result = JObject.Parse(response.Content);
            string value = result[read.jr("TestData.json", "AC3")][1][read.jr("TestData.json", "AC1")].Value<string>();
            Assert.That(value.Equals(read.jr("TestData.json", "gallery")), value, "The value is not as expected");
            string desc = result[read.jr("TestData.json", "AC3")][1][read.jr("TestData.json", "V3")].Value<string>();
            Assert.That(desc.Equals(read.jr("TestData.json", "V4")), "The values does not match as expected", desc);
        }
    }
}
