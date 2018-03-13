using Fundraise.Core.Services;
using Fundraise.MvcExample.Requests;
using MediatR;
using System;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class UpdateFundraiserHandler : RequestHandler<UpdateFundraiser, bool>
    {
        private IFundraiserRepository _fundraiserRepository;

        public UpdateFundraiserHandler(IFundraiserRepository fundraiserRepository)
        {
            _fundraiserRepository = fundraiserRepository;
        }

        protected override bool HandleCore(UpdateFundraiser request)
        {
            try
            {
                var fundraiser = _fundraiserRepository.FindById(request.Id);
                fundraiser.Name = request.Name;
                fundraiser.Description = request.Description;
                fundraiser.IsActive = request.IsActive;
                _fundraiserRepository.Update(fundraiser);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}