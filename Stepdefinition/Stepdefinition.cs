using System.Net;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Testing.Data;

namespace Testing.Stepdefinition
{
    [Binding]
    public class Stepdefinition : RestAPIHelper
    {
        JSON read = new JSON();

        [When(@"I pass headers for (.*) and (.*)")]
        public void WhenIPassHeadersForFalseAndApplicationJson(string content, string status)
        {
            Parameters(content, status);
        }


        [Then(@"I am able to see the category name with headers (.*) and (.*)")]
        public void ThenIAmAbleToSeeTheCategoryNameWithHeadersAnd(string content, string status)
        {
            var response = Parameters(content, status);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "The response has failed");
            Assert.That(response.ContentType, Is.EqualTo(content));
            JObject result = JObject.Parse(response.Content);
            string value = result[read.jr("TestData.json", "AC1")].Value<string>();
            Assert.AreEqual(read.jr("TestData.json", "V1"), value, "The value is not as expected");
        }

        [Then(@"I am able to see the canrelist status with headers (.*) and (.*)")]
        public void ThenIAmAbleToSeeTheCanrelistStatusWithHeadersAnd(string content, string status)
        {
            var response = Parameters(content, status);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "The response has failed");
            Assert.That(response.ContentType, Is.EqualTo(content));
            JObject result = JObject.Parse(response.Content);
            string value = result[read.jr("TestData.json", "AC2")].Value<string>();
            Assert.AreEqual(read.jr("TestData.json", "V2"), value, "The value is not as expected");
        }

        [Then(@"I am able to see the promotions name with headers (.*) and (.*)")]
        public void ThenIAmAbleToSeeThePromotionsNameWithHeadersAnd(string content, string status)
        {
            var response = Parameters(content, status);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "The response has failed");
            Assert.That(response.ContentType, Is.EqualTo(content));
            JObject result = JObject.Parse(response.Content);
            string value = result[read.jr("TestData.json", "AC3")][1][read.jr("TestData.json", "AC1")].Value<string>();
            Assert.AreEqual(read.jr("TestData.json", "gallery"), value, "The value is not as expected");
            string desc = result[read.jr("TestData.json", "AC3")][1][read.jr("TestData.json", "V3")].Value<string>();
            Assert.IsTrue(desc.Equals(read.jr("TestData.json", "V4")), "The values does not match as expected", desc);
        }
    }
}
