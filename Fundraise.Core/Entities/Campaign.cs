using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundraise.Core.Entities
{
    public class Campaign
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string DefaultCurrencyCode { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
        public DateTime EndDate { get; internal set; }

        public List<Donation> Donations { get; set; }
    }
}
