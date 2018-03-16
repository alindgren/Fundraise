using System;
using MediatR;

namespace Fundraise.Requests.Fundraiser
{
    public class GetById : IRequest<Core.Entities.Fundraiser>
    {
        public GetById(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}