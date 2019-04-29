using System;

namespace Entities.Models
{
    /// <summary>
    /// شهر
    /// </summary>
    public class City
    {
        public Guid Id { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid DistrictId { get; set; }
        public string Title { get; set; }
    }
}
