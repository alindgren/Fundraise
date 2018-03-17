using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.Requests.Donation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class GetDonationsByFundraiserIdHandler : RequestHandler<GetByFundraiserId, List<Donation>>
    {
        private IDonationRepository _donationRepository;

        public GetDonationsByFundraiserIdHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        protected override List<Donation> HandleCore(GetByFundraiserId request)
        {
            return _donationRepository.GetByFundraiser(request.FundraiserId).ToList();
        }
    }
}