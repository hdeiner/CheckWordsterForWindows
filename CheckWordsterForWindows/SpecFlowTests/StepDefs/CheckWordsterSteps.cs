using System;
using System.Threading;
using CheckWordsterForWindows.Services;
using TechTalk.SpecFlow;
using Xunit;

namespace CheckWordsterForWindows.SpecFlowTests.StepDefs
{
    [Binding]
    static class CheckWordsterSteps
    {
        private static Mutex _mutex = new Mutex();
        private static CheckWordster _checkWordster;
        private static string _digits;
        private static string _whichServerStarted = "";
        private static CheckWordsterTestClient _checkWordsterTestClient = null;
        private static Boolean _mutexAcquired = false;

        [Given(@"I start the ""(.*)"" server")]
        public static void GivenIStartTheServer(string serverName)
        {
            if (!_whichServerStarted.Equals(serverName))
            {
                while (!_mutex.WaitOne())
                {
                    Thread.Sleep(1000);
                }

                _mutexAcquired = true;
                _checkWordsterTestClient = new CheckWordsterTestClient(serverName);
                _checkWordsterTestClient.StartServer();
                _whichServerStarted = serverName;
            }

        }

        [When(@"I convert ""(.*)"" into words")]
        public static void WhenConvertToWords(string digits)
        {
            _digits = digits;
            if (_whichServerStarted.Equals("no"))
            {
                _checkWordster = new CheckWordster(_digits);
            }
        }

        [When(@"I convert ""(.*)"" into words, an exception ""(.*)"" should be thrown")]
        public static void WhenConvertIntoWordsAnExceptionShouldBeThrown(string digits, string errorMessage)
        {
            var ex = Assert.Throws<Exception>(() => new CheckWordster(digits));
            Assert.Equal(errorMessage, ex.Message);
        }

        [Then(@"it should be ""(.*)""")]
        public static void ThenItShouldBe(String wordsExpected)
        {
            if (_whichServerStarted.Equals("no"))
            {
                Assert.Equal(wordsExpected, _checkWordster.GetWords());
            }
            else
            {
                Assert.Equal(wordsExpected, _checkWordsterTestClient.GetWords(_digits));
            }
            
        }

        [Then(@"I stop the server")]
        public static void ThenStopTheServer()
        {
            if (!_whichServerStarted.Equals(""))
            {
                _checkWordsterTestClient.StopServer();
            }
            _whichServerStarted = "";
            _mutexAcquired = false;
            _mutex.ReleaseMutex();
        }
    }
}
