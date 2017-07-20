using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Fundraise.MvcExample.Models
{
    public class FundraiserFormViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CampaignId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(512), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }
    }
}