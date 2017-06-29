using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundraise.Core.Entities
{
    public class Campaign
    {
        // https://docs.microsoft.com/en-us/ef/core/modeling/backing-field
        private string _extendedData;

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

        [NotMapped]
        public JObject ExtendedData
        {
            get
            {
                return JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(_extendedData) ? "{}" : _extendedData);
            }
            set
            {
                _extendedData = value.ToString();
            }
        }
    }
}
