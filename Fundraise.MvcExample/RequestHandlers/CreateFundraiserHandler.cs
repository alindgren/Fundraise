using System;
using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.MvcExample.Requests;
using MediatR;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class CreateFundraiserHandler : RequestHandler<CreateFundraiser, Guid>
    {
        private IFundraiserRepository _fundraiserRepository;

        public CreateFundraiserHandler(IFundraiserRepository fundraiserRepository)
        {
            _fundraiserRepository = fundraiserRepository;
        }

        protected override Guid HandleCore(CreateFundraiser request)
        {
            var fundraiser = _fundraiserRepository.Create(request.Name, request.CampaignId, FundraiserType.Individual, request.UserId);
            return fundraiser.Id;
        }
    }
}