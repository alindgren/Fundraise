using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.Requests.Donation;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class DonationsByDonorIdHandler : RequestHandler<GetByDonorId, List<Donation>>
    {
        private IDonationRepository _donationRepository;

        public DonationsByDonorIdHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        protected override List<Donation> HandleCore(GetByDonorId request)
        {
            return _donationRepository.GetByDonor(request.DonorId).ToList();
        }
    }
}