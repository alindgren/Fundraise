using System;
using MediatR;

namespace Fundraise.Requests.Campaign
{
    public class GetById : IRequest<Core.Entities.Campaign>
    {
        public GetById(Guid id)
        {
            CampaignId = id;
        }
        public Guid CampaignId { get; set; }
    }
}