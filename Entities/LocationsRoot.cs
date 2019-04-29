using System.Collections.Generic;
using Entities.Models;

namespace Entities
{
    public class LocationsRoot
    {
        public LocationsRoot()
        {
            Countries= new List<Country>();
            States= new List<State>();
            Provinces= new List<Province>();
            Districts= new List<District>();
            Cities= new List<City>();
            Villages= new List<Village>();
        }
        public List<Country> Countries { get; set; }
        public List<State> States { get; set; }
        public List<Province> Provinces { get; set; }
        public List<District> Districts { get; set; }
        public List<City> Cities { get; set; }
        public List<Village> Villages { get; set; }
    }
}
