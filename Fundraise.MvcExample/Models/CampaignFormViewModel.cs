using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fundraise.MvcExample.Models
{
    public class CampaignFormViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, DisplayName("Currency"), DataType("CurrencyCode")]
        public string DefaultCurrencyCode { get; set; }

        [MaxLength(512), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("Is active")]
        public bool IsActive { get; set; }

        [DisplayName("End date")]
        public DateTime? EndDate { get; set; }

        [DisplayName("More Info URL")]
        public string MoreInfoUrl { get; set; } // store in ExtendedData
    }
}