using MediatR;
using System;

namespace Fundraise.Requests.Campaign
{
    public class Create : IRequest<bool>
    {
        public string Name { get; set; }

        public string DefaultCurrencyCode { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime? EndDate { get; set; }

        public string MoreInfoUrl { get; set; }
    }
}