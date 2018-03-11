using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.MvcExample.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class GetDonationsByFundraiserIdHandler : RequestHandler<GetDonationsByFundraiserId, List<Donation>>
    {
        private IDonationRepository _donationRepository;

        public GetDonationsByFundraiserIdHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        protected override List<Donation> HandleCore(GetDonationsByFundraiserId request)
        {
            return _donationRepository.GetByFundraiser(request.FundraiserId).ToList();
        }
    }
}