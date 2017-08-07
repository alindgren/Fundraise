using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fundraise.MvcExample.Models
{
    public class FundraisersViewModel // same as AdminViewModel? 
    {
        public IList<CampaignViewModel> Campaigns { get; set; }
    }
}