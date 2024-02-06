using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System.IO;
using TechTalk.SpecFlow;

namespace APITests.Utilities
{
    [Binding]
    internal class Hooks
    {
        private readonly ScenarioContext scenarioContext;
        private static ExtentTest feature, scenario;
        private static ExtentReports htmlreport;
        private Hooks(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;

        }

        [BeforeTestRun]
        public static void InitializeReports()
        {
            var reports = new ExtentHtmlReporter(Path.Combine(Directory.GetParent(TestContext.CurrentContext.TestDirectory).Parent.Parent + @"//Reports", "index.html"));
            reports.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            htmlreport = new ExtentReports();
            htmlreport.AttachReporter(reports);

        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            feature = htmlreport.CreateTest<Feature>(context.FeatureInfo.Title);
        }

        [BeforeScenario]
        private void BeforeScenario()
        {
            scenario = feature.CreateNode<ScenarioOutline>(scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        private void AfterScenario()
        {
            htmlreport.Flush();
        }
    }
}
