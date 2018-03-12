using System;
using Fundraise.Core.Entities;
using MediatR;

namespace Fundraise.MvcExample.Requests
{
    public class CampaignById : IRequest<Campaign>
    {
        public CampaignById(Guid id)
        {
            CampaignId = id;
        }
        public Guid CampaignId { get; set; }
    }
}