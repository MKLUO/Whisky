using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;

namespace TestClient {
    class Program {
        static void Main(string[] args) {

            Console.WriteLine("Scan a barcode... (POST {BarTender Server}/api/test/{Scanned code})");

            string code = "";
            while (!string.IsNullOrEmpty(code = Console.ReadLine())) {
                var client = new RestClient("http://localhost:8080/api/test");
                var request = new RestRequest().AddJsonBody(new { Name = code });
                Console.WriteLine(client.Post(request));
            }
        }
    }
}
