using Fundraise.Core.Services;
using Fundraise.Requests.Fundraiser;
using MediatR;
using System;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class UpdateFundraiserHandler : RequestHandler<Update, bool>
    {
        private IFundraiserRepository _fundraiserRepository;

        public UpdateFundraiserHandler(IFundraiserRepository fundraiserRepository)
        {
            _fundraiserRepository = fundraiserRepository;
        }

        protected override bool HandleCore(Update request)
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