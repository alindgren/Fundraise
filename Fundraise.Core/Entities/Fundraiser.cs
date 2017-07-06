using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundraise.Core.Entities
{
    public class Fundraiser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CampaignId { get; set; }

        [Required]
        public FundraiserType FundraiserType { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public List<Donation> Donations { get; set; }
    }
}
