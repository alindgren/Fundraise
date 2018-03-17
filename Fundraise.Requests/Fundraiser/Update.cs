using MediatR;
using System;

namespace Fundraise.Requests.Fundraiser
{
    public class Update : IRequest<bool>
    {
        public Guid Id { get; set; }
        //public Guid CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}