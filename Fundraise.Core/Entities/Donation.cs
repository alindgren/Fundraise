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

        /// <summary>
        /// The amount of the donation.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// The currency of the donation
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// The amount of the donation converted into the default currency
        /// </summary>
        public double AmountInDefaultCurrency { get; set; }
    }
}
