using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundraise.Core.Entities
{
    public class Donation
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CampaignId { get; set; }

        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }

        public string DonorDisplayName { get; set; }

        public string ReferenceNumber { get; set; }

        [Required]
        public DonationStatus Status { get; set; }
    }
}
