using System;
using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.Requests.Fundraiser;
using MediatR;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class CreateFundraiserHandler : RequestHandler<Create, Guid>
    {
        private IFundraiserRepository _fundraiserRepository;

        public CreateFundraiserHandler(IFundraiserRepository fundraiserRepository)
        {
            _fundraiserRepository = fundraiserRepository;
        }

        protected override Guid HandleCore(Create request)
        {
            var fundraiser = _fundraiserRepository.Create(request.Name, request.CampaignId, FundraiserType.Individual, request.UserId);
            return fundraiser.Id;
        }
    }
}