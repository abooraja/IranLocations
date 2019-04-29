using System;

namespace Entities.Models
{
    /// <summary>
    /// شهرستان
    /// </summary>
    public class Province
    {
        public Guid Id { get; set; }
        public Guid StateId { get; set; }
        public string Title { get; set; }
    }
}
