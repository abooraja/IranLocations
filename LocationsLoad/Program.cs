using System;
using System.IO;
using Entities;
using Newtonsoft.Json;

namespace LocationsLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("loading data from file ...");
            var jsonString = File.ReadAllText("locations.json");
            Console.WriteLine("converting json to model ...");
            var rootModel = JsonConvert.DeserializeObject<LocationsRoot>(jsonString);
            Console.WriteLine($"States: {rootModel.States.Count} - Provinces: {rootModel.Provinces.Count} - Districts: {rootModel.Districts.Count} - Cities: {rootModel.Cities.Count} - Villages: {rootModel.Villages.Count}");
            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
