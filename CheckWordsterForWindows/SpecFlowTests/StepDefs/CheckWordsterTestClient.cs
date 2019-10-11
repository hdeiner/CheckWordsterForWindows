using System;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json;
using RestSharp;

namespace CheckWordsterForWindows.SpecFlowTests.StepDefs
{
    class CheckWordsterJSON
    {
        public string numberInDigits { get; set; }
        public string numberInWords { get; set; }
    }
    class CheckWordsterTestClient
    {
        private static string _numberInWords;
        private static string _whichServer;
        private static Process _process;

        public CheckWordsterTestClient(String whichServer) 
        {
            _whichServer = whichServer;
        }

        public void StartServer() 
        {
            if (_whichServer.Equals("local"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.Arguments = "";
                start.FileName = "CheckWordsterForWindows.exe";
                start.WindowStyle = ProcessWindowStyle.Normal;
                start.CreateNoWindow = true;
                start.UseShellExecute = true;
                _process = Process.Start(start);
                Thread.Sleep(5000);
            }

            if (_whichServer.Equals("wiremock-container"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.Arguments = "up";
                start.FileName = "docker-compose";
                start.WindowStyle = ProcessWindowStyle.Normal;
                start.CreateNoWindow = true;
                start.UseShellExecute = true;
                _process = Process.Start(start);
                Thread.Sleep(10000);
            }

        }

        public void StopServer()
        {
            if (_whichServer.Equals("local"))
            {
                _process.CloseMainWindow();
                _process.Close();
            }

            if (_whichServer.Equals("wiremock-container"))
            {
                ProcessStartInfo start = new ProcessStartInfo();
                start.Arguments = "down";
                start.FileName = "docker-compose";
                start.WindowStyle = ProcessWindowStyle.Normal;
                start.CreateNoWindow = true;
                start.UseShellExecute = true;
                _process = Process.Start(start);
                Thread.Sleep(10000);
            }

        }

        public string GetWords(string numberInDigitsParameter)
        {
            if (_whichServer.Equals("local"))
            {
                var client = new RestClient("http://localhost:9090");
                var request = new RestRequest("/checkwordster", Method.POST);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                request.AddParameter("numberInDigits", numberInDigitsParameter);
                IRestResponse response = client.Execute(request);
                CheckWordsterJSON responseObject = JsonConvert.DeserializeObject<CheckWordsterJSON>(response.Content);
                _numberInWords = responseObject.numberInWords;
            }

            if (_whichServer.Equals("wiremock-container"))
            {
                var client = new RestClient("http://localhost:9999");
                var request = new RestRequest("/checkWordster", Method.POST);
                request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
                request.AddHeader("Content-Type", "text/plain");
                request.AddJsonBody("{" + "\"numberInDigits\": " + "\"" + numberInDigitsParameter + "\"}");
                IRestResponse response = client.Execute(request);
                CheckWordsterJSON responseObject = JsonConvert.DeserializeObject<CheckWordsterJSON>(response.Content);
                _numberInWords = responseObject.numberInWords;
            }

            return _numberInWords;
        }
    }
}
