using System.Collections.Generic;
using MediatR;

namespace Fundraise.Requests.Campaign
{
    public class GetAll : IRequest<List<Core.Entities.Campaign>>
    {
    }
}