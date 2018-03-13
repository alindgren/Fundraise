using MediatR;
using System;

namespace Fundraise.MvcExample.Requests
{
    public class UpdateFundraiser : IRequest<bool>
    {
        public Guid Id { get; set; }
        //public Guid CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}