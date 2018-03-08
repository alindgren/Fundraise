using System;
using System.Collections.Generic;
using Fundraise.Core.Entities;
using MediatR;

namespace Fundraise.MvcExample.Requests
{
    public class FundraisersByCampaignId : IRequest<List<Fundraiser>>
    {
        public FundraisersByCampaignId(Guid campaignId)
        {
            CampaignId = campaignId;
        }

        public Guid CampaignId { get; set; }
    }
}