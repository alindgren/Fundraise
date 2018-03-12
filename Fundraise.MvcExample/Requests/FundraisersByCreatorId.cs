using System;
using System.Collections.Generic;
using Fundraise.Core.Entities;
using MediatR;

namespace Fundraise.MvcExample.Requests
{
    public class FundraisersByCreatorId : IRequest<List<Fundraiser>>
    {
        public FundraisersByCreatorId(string creatorId)
        {
            CreatorId = creatorId;
        }

        public string CreatorId { get; set; }
    }
}