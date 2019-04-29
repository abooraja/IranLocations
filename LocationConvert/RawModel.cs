using System;
using System.Text.RegularExpressions;

namespace LocationConvert
{
    class RawModel
    {
        public RawModel()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Ostan { get; set; }
        public string OstanName { get; set; }
        public string Shahrestan { get; set; }
        public string ShahrestName { get; set; }
        public string Bakhsh { get; set; }
        public string BakhshName { get; set; }
        public string Dehestan { get; set; }
        public string DehestanName { get; set; }
        public string City { get; set; }

        public string CityClean
        {
            get
            {
                return Regex.Replace(CityName, @"[\d-]", string.Empty).Trim(); // remove integer from text
            }
        }

        public string CityName { get; set; }
        public string Abadi { get; set; }
        public string AbadiName { get; set; }
        public string Coderec { get; set; }
        public string Diag { get; set; }
    }
}