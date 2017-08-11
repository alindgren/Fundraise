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

        [Required]
        [ForeignKey("CampaignId")]
        public Campaign Campaign { get; set; }

        public Guid FundraiserId { get; set; }

        [ForeignKey("FundraiserId")]
        public Fundraiser Fundraiser { get; set; }

        [MaxLength(128)]
        public string DonorUserId { get; set; }

        public string DonorDisplayName { get; set; }

        public string ReferenceNumber { get; set; }

        [Required]
        public DonationStatus Status { get; set; }

        /// <summary>
        /// The amount of the donation.
        /// </summary>
        [Required]
        public double Amount { get; set; }

        /// <summary>
        /// The currency code for the donation amount
        /// </summary>
        [Required]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// The amount of the donation converted into the default currency
        /// </summary>
        [Required]
        public double AmountInDefaultCurrency { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateLastUpdated { get; set; }
    }
}
