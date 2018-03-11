using Fundraise.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace Fundraise.MvcExample.Requests
{
    public class GetDonationsByFundraiserId : IRequest<List<Donation>>
    {
        public GetDonationsByFundraiserId(Guid fundraiserId)
        {
            FundraiserId = fundraiserId;
        }
        public Guid FundraiserId { get; set; }
    }
}