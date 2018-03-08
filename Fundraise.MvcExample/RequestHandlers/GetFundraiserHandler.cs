using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.MvcExample.Requests;
using MediatR;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class GetFundraiserHandler : RequestHandler<FundraiserId, Fundraiser>
    {
        private IFundraiserRepository _fundraiserRepository;

        public GetFundraiserHandler(IFundraiserRepository fundraiserRepository)
        {
            _fundraiserRepository = fundraiserRepository;
        }

        protected override Fundraiser HandleCore(FundraiserId request)
        {
            return _fundraiserRepository.FindById(request.Id);
        }
    }
}