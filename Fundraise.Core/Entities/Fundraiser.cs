using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundraise.Core.Entities
{
    public class Fundraiser
    {
        private string _extendedData;

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

        [NotMapped]
        public JObject ExtendedData
        {
            get
            {
                return JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(_extendedData) ? "{}" : _extendedData);
            }
            set
            {
                if (value == null)
                {
                    _extendedData = null;
                }
                else
                {
                    _extendedData = value.ToString();
                }
            }
        }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public DateTime DateLastUpdated { get; set; }
    }
}
