using System;
using System.Collections.Generic;
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
        public IList<FundraiserViewModel> Fundraisers { get; set; }
    }

    public class FundraiserViewModel
    {

    }
}