using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fundraise.MvcExample.Models
{
    public class AdminViewModel
    {
        public IList<CampaignViewModel> Campaigns { get; set; }
    }

    public class CampaignViewModel
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public IList<FundraiserViewModel> Fundraisers { get; set; }
    }

    public class FundraiserViewModel
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}