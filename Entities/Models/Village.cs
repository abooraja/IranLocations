using System;

namespace Entities.Models
{
    /// <summary>
    /// روستا
    /// </summary>
    public class Village
    {
        public Guid Id { get; set; }
        public Guid? CityId { get; set; }
        public Guid DistrictId { get; set; }
        public string Title { get; set; }
    }
}
