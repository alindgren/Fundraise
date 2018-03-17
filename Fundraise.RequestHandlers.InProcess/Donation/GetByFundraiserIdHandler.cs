using Fundraise.Core.Services;
using Fundraise.Requests.Donation;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Fundraise.RequestHandlers.InProcess.Donation
{
    public class GetByFundraiserIdHandler : RequestHandler<GetByFundraiserId, List<Core.Entities.Donation>>
    {
        private IDonationRepository _donationRepository;

        public GetByFundraiserIdHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        protected override List<Core.Entities.Donation> HandleCore(GetByFundraiserId request)
        {
            return _donationRepository.GetByFundraiser(request.FundraiserId).ToList();
        }
    }
}