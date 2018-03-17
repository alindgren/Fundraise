using System;
using MediatR;

namespace Fundraise.Requests.Fundraiser
{
    public class Create : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
    }
}