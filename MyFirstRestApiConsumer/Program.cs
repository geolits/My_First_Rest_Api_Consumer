using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyFirstRestApiConsumer
{
    class Program
    {

        static async Task Main(string[] args)
        {
            string url = @"https://jsonmock.hackerrank.com/api/countries";

           

            
             /*using (var httpClient = new HttpClient())
             {
                 //httpClient.DefaultRequestHeaders.Add(RequestConstants.UserAgent, RequestConstants.UserAgentValue);
                 var response = httpClient.GetStringAsync(new Uri(url)).Result;

                 Console.WriteLine(response);

                 Console.ReadLine();
             }*/

            // Call asynchronous network methods in a try/catch block to handle exceptions.


            using (var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // Above three lines can be replaced with new helper method below
                    // string responseBody = await client.GetStringAsync(uri);

                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                }
            }


            Console.ReadLine();
        }
    }
}
