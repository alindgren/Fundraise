using System.Collections.Generic;
using MediatR;

namespace Fundraise.Requests.Fundraiser
{
    public class GetByCreatorId : IRequest<List<Core.Entities.Fundraiser>>
    {
        public GetByCreatorId(string creatorId)
        {
            CreatorId = creatorId;
        }

        public string CreatorId { get; set; }
    }
}