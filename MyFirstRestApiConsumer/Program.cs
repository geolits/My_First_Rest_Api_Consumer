using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace MyFirstRestApiConsumer
{
    class Program
    {

        static async Task Main(string[] args)
        {
            string substr1 = "en";

            long MinPopulation = 10000000;

            int currentPage = 1;

            string url = @"https://jsonmock.hackerrank.com/api/countries/search?name="+substr1+@"&page="+currentPage;

            Console.WriteLine(url);


            int countCountries = 0;

            
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

                    //var JsonObj = JsonSerializer.Deserialize<dynamic>(responseBody);

                    var JsonObj = JsonConvert.DeserializeObject<dynamic>(responseBody);

                    int totalPages = JsonObj.total_pages;

                    while(currentPage <= totalPages)
                    {
                        foreach(var country in JsonObj.data)
                        {
                            if (country.population >= MinPopulation)
                            {
                                countCountries++;
                                Console.WriteLine(country.name);
                                Console.WriteLine(currentPage);
                            }
                        }

                        //Next page

                        currentPage++;
                        url = @"https://jsonmock.hackerrank.com/api/countries/search?name=" + substr1 + @"&page=" + currentPage;

                        response = await client.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        responseBody = await response.Content.ReadAsStringAsync();
                        JsonObj = JsonConvert.DeserializeObject<dynamic>(responseBody);

                    }                    

                    
                    Console.WriteLine($"The total countries whose name contains the string: {substr1} and which have population more than: {MinPopulation} are: {countCountries}");
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
