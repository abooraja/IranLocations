using System;

namespace Entities.Models
{
    /// <summary>
    /// استان
    /// </summary>
    public class State
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public string Title { get; set; }
    }
}
