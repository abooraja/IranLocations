using System;

namespace Entities.Models
{
    /// <summary>
    /// محدوده/بخش
    /// </summary>
    public class District 
    {
        public Guid Id { get; set; }
        public Guid ProvinceId { get; set; }
        public string Title { get; set; }
    }
}