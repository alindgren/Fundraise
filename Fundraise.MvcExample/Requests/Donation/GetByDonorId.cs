using System.Collections.Generic;
using MediatR;

namespace Fundraise.Requests.Donation
{
    public class GetByDonorId : IRequest<List<Core.Entities.Donation>>
    {
        public GetByDonorId(string donorId)
        {
            DonorId = donorId;
        }

        public string DonorId { get; set; }
    }
}