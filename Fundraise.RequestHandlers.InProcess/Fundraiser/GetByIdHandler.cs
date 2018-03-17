using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.Requests.Fundraiser;
using MediatR;

namespace Fundraise.RequestHandlers.InProcess.Fundraiser
{
    public class GetByIdHandler : RequestHandler<GetById, Core.Entities.Fundraiser>
    {
        private IFundraiserRepository _fundraiserRepository;

        public GetByIdHandler(IFundraiserRepository fundraiserRepository)
        {
            _fundraiserRepository = fundraiserRepository;
        }

        protected override Core.Entities.Fundraiser HandleCore(GetById request)
        {
            return _fundraiserRepository.FindById(request.Id);
        }
    }
}