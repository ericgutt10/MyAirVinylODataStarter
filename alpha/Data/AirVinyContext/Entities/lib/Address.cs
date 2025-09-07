using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AirVinyContext.Entities.lib
{
    // Address is an owned type (no key) - used to be called complex type in EF.
    [Owned]
    public class Address
    {
        [StringLength(200)]
        public string? Street { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [Required]
        [StringLength(10)]
        public string? PostalCode { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [Required]
        public int RecordStoreId { get; set; }
    }
}