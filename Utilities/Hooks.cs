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

        [AfterStep]
        public void AfterStep(ScenarioContext context)
        {
            var step = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (ScenarioContext.Current.TestError == null)
            {

                if (step == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (step == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else if (step == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                }
                else
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                }
            }
            else

            {
                if (step == "Given")
                {
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.StackTrace);
                }
                else if (step == "When")
                {
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.StackTrace);
                }
                else if (step == "Then")
                {
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.StackTrace);
                }
                else
                {
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.StackTrace);
                }
            }
        }

        [AfterScenario]
        private void AfterScenario()
        {
            htmlreport.Flush();
        }
    }
}
