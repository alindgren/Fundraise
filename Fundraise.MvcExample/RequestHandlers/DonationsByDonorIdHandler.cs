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
    public class DonationsByDonorIdHandler : RequestHandler<DonationsByDonorId, List<Donation>>
    {
        private IDonationRepository _donationRepository;

        public DonationsByDonorIdHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        protected override List<Donation> HandleCore(DonationsByDonorId request)
        {
            return _donationRepository.GetByDonor(request.DonorId).ToList();
        }
    }
}