using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.Requests.Fundraiser;
using MediatR;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class GetFundraiserHandler : RequestHandler<GetById, Fundraiser>
    {
        private IFundraiserRepository _fundraiserRepository;

        public GetFundraiserHandler(IFundraiserRepository fundraiserRepository)
        {
            _fundraiserRepository = fundraiserRepository;
        }

        protected override Fundraiser HandleCore(GetById request)
        {
            return _fundraiserRepository.FindById(request.Id);
        }
    }
}