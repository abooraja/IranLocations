using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Entities;
using Entities.Models;
using Newtonsoft.Json;

namespace LocationConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("reading...");
                var rawModels = ReadRawModels();

                var states = rawModels.Where(w => w.Shahrestan == "").ToList();
                var provinces = rawModels.Where(w => w.Shahrestan != "" && w.Bakhsh == "").ToList();
                var districts = rawModels.Where(w => w.Shahrestan != "" && w.Bakhsh != "" && w.City == "" && w.Dehestan == "").ToList();
                var cities = rawModels.Where(w => w.Shahrestan != "" && w.Bakhsh != "" && w.City != "").ToList();
                var villages = rawModels.Where(w => w.Shahrestan != "" && w.Bakhsh != "" && w.AbadiName != "").ToList();

                Console.WriteLine("Processing...");

                var iranCountry = new Country() { Id = Guid.NewGuid(), Title = "جمهوری اسلامی ایران" };
                var countryList = new List<Country>() { iranCountry };
                var stateList = new List<State>();
                var provinceList = new List<Province>();
                var districtList = new List<District>();
                var cityList = new List<City>();
                var villageList = new List<Village>();

                foreach (var rawModel in states)
                {
                    var stateEntity = new State()
                    {
                        Id = rawModel.Id,
                        CountryId = iranCountry.Id,
                        Title = rawModel.OstanName
                    };
                    stateList.Add(stateEntity);
                }

                foreach (var rawModel in provinces)
                {
                    var stateId = states.First(q => q.OstanName == rawModel.OstanName).Id;
                    var provinceEntity = new Province()
                    {
                        Id = rawModel.Id,
                        StateId = stateId,
                        Title = rawModel.ShahrestName
                    };
                    provinceList.Add(provinceEntity);
                }

                foreach (var rawModel in districts)
                {
                    var provinceId = provinces.First(q => q.ShahrestName == rawModel.ShahrestName && q.OstanName == rawModel.OstanName).Id;
                    var districtEntity = new District()
                    {
                        Id = rawModel.Id,
                        ProvinceId = provinceId,
                        Title = rawModel.BakhshName
                    };
                    districtList.Add(districtEntity);
                }

                foreach (var rawModel in cities)
                {
                    var provinceId = provinces.First(q => q.ShahrestName == rawModel.ShahrestName && q.OstanName == rawModel.OstanName).Id;
                    var districtId = districts.First(q => q.ShahrestName == rawModel.ShahrestName && q.BakhshName == rawModel.BakhshName).Id;
                    if (cityList.Any(q => q.DistrictId == districtId && q.ProvinceId == provinceId && q.Title == rawModel.CityClean)) continue;

                    var cityEntity = new City()
                    {
                        Id = rawModel.Id,
                        ProvinceId = provinceId,
                        DistrictId = districtId,
                        Title = rawModel.CityClean
                    };
                    cityList.Add(cityEntity);
                }

                foreach (var rawModel in villages)
                {
                    var districtId = districts.First(q => q.ShahrestName == rawModel.ShahrestName && q.BakhshName == rawModel.BakhshName).Id;
                    var provinceId = provinces.First(q => q.ShahrestName == rawModel.ShahrestName && q.OstanName == rawModel.OstanName).Id;
                    var citiesInProvince = cityList.Where(q => q.ProvinceId == provinceId && q.DistrictId == districtId).ToList();

                    Guid mainCity = Guid.Empty;
                    if (citiesInProvince.Any())
                    {
                        mainCity = citiesInProvince.First().Id;
                    }

                    var villageEntity = new Village()
                    {
                        Id = rawModel.Id,
                        CityId = mainCity,
                        DistrictId = districtId,
                        Title = rawModel.AbadiName
                    };
                    villageList.Add(villageEntity);
                }

                Console.WriteLine("saving...");
                SaveToJsonFile(countryList, stateList, provinceList, districtList, cityList, villageList);

                Console.WriteLine($"States: {stateList.Count} - Provinces: {provinceList.Count} - Districts: {districtList.Count} - Cities: {cityList.Count} - Villages: {villageList.Count}");
                Console.WriteLine("Finish");
                Console.ReadKey();


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void SaveToJsonFile(List<Country> countryList, List<State> stateList, List<Province> provinceList, List<District> districtList, List<City> cityList, List<Village> villageList)
        {
            var root = new LocationsRoot
            {
                Countries = countryList,
                States = stateList,
                Provinces = provinceList,
                Districts = districtList,
                Cities = cityList,
                Villages = villageList
            };

            var locationsString = JsonConvert.SerializeObject(root, Formatting.Indented);
            File.WriteAllText("locations.json", locationsString, Encoding.UTF8);
        }

        private static List<RawModel> ReadRawModels()
        {
            var lines = File.ReadAllLines("GEO96Xlsx.csv", Encoding.UTF8);
            var raws = lines.Select(line => line.Split(','))
                .Select(arr => new RawModel()
                {
                    Ostan = arr[0].Trim(),
                    OstanName = arr[1].Trim(),
                    Shahrestan = arr[2].Trim(),
                    ShahrestName = arr[3].Trim(),
                    Bakhsh = arr[4].Trim(),
                    BakhshName = arr[5].Trim(),
                    Dehestan = arr[6].Trim(),
                    DehestanName = arr[7].Trim(),
                    City = arr[8].Trim(),
                    CityName = arr[9].Trim(),
                    Abadi = arr[10].Trim(),
                    AbadiName = arr[11].Trim(),
                    Coderec = arr[12].Trim(),
                    Diag = arr[13].Trim(),
                })
                .ToList();
            return raws;
        }
    }
}
