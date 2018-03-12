using System;
using System.Collections.Generic;
using Fundraise.Core.Entities;
using MediatR;

namespace Fundraise.MvcExample.Requests
{
    public class DonationsByDonorId : IRequest<List<Donation>>
    {
        public DonationsByDonorId(string donorId)
        {
            DonorId = donorId;
        }

        public string DonorId { get; set; }
    }
}