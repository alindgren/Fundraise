using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.MvcExample.Requests;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class FundraisersByCreatorIdHandler : RequestHandler<FundraisersByCreatorId, List<Fundraiser>>
    {
        private IFundraiserRepository _fundraiserRepository;

        public FundraisersByCreatorIdHandler(IFundraiserRepository fundraiserRepository)
        {
            _fundraiserRepository = fundraiserRepository;
        }

        protected override List<Fundraiser> HandleCore(FundraisersByCreatorId request)
        {
            return _fundraiserRepository.FindByCreator(request.CreatorId).ToList();
        }
    }
}