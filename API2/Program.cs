//David Peikrishvili
//Assignment 5

using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
    class Anime
    {
        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("director")]
        public string director { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("release_date")]
        public string release_date { get; set; }

        [JsonProperty("original_title_romanised")]
        public string original_title_romanised { get; set; }
    }



    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }
        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter Name of Title: ");
                    var title = Console.ReadLine();
                    if (string.IsNullOrEmpty(title))
                    {
                        break;
                    }
                    var result = await client.GetAsync("https://ghibliapi.herokuapp.com/films/?title=" + title);
                    var resultRead = await result.Content.ReadAsStringAsync();
                    var anime = JsonConvert.DeserializeObject<List<Anime>>(resultRead)[0];
                    Console.WriteLine("--------");
                    Console.WriteLine("Anime Name : " + anime.title);
                    Console.WriteLine("Original Title Romanised: " + anime.original_title_romanised);
                    Console.WriteLine("Director: " + anime.director);
                    Console.WriteLine("Anime Description: \n" + anime.description);
                    Console.WriteLine("Anime Release Date: " + anime.release_date);
                    Console.WriteLine("--------");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error, PLease enter a valid title!!");
                }
            }
        }
    }

}