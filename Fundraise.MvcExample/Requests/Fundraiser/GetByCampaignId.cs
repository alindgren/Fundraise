using System;
using System.Collections.Generic;
using MediatR;

namespace Fundraise.Requests.Fundraiser
{
    public class GetByCampaignId : IRequest<List<Core.Entities.Fundraiser>>
    {
        public GetByCampaignId(Guid campaignId)
        {
            CampaignId = campaignId;
        }

        public Guid CampaignId { get; set; }
    }
}