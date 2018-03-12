using System;
using Fundraise.Core.Entities;
using MediatR;

namespace Fundraise.MvcExample.Requests
{
    public class FundraiserId : IRequest<Fundraiser>
    {
        public FundraiserId(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}