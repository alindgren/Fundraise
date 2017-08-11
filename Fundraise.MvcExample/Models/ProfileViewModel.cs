using Fundraise.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fundraise.MvcExample.Models
{
    public class ProfileViewModel
    {
        public List<DonationViewModel> Donations { get; set; }
        public List<FundraiserViewModel> Fundraisers { get; set; }
    }

    public class DonationViewModel
    {
        public Guid Id { get; set; }
        public Guid CampaignId { get; set; }
        public string CampaignName { get; set; }
        public Guid FundraiserId { get; set; }
        public string FundraiserName { get; set; }
        public string DonorUserId { get; set; }
        public string DonorDisplayName { get; set; }
        public string ReferenceNumber { get; set; }
        public DonationStatus Status { get; set; }
        public double Amount { get; set; }
    }
}