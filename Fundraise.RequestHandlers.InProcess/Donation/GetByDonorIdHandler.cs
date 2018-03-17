using Fundraise.Core.Services;
using Fundraise.Requests.Donation;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Fundraise.RequestHandlers.InProcess.Donation
{
    public class GetByDonorIdHandler : RequestHandler<GetByDonorId, List<Core.Entities.Donation>>
    {
        private IDonationRepository _donationRepository;

        public GetByDonorIdHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        protected override List<Core.Entities.Donation> HandleCore(GetByDonorId request)
        {
            return _donationRepository.GetByDonor(request.DonorId).ToList();
        }
    }
}