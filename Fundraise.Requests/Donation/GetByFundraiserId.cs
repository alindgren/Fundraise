using MediatR;
using System;
using System.Collections.Generic;

namespace Fundraise.Requests.Donation
{
    public class GetByFundraiserId : IRequest<List<Core.Entities.Donation>>
    {
        public GetByFundraiserId(Guid fundraiserId)
        {
            FundraiserId = fundraiserId;
        }
        public Guid FundraiserId { get; set; }
    }
}