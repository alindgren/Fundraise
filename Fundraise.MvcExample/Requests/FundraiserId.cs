using System;
using Fundraise.Core.Entities;
using MediatR;

namespace Fundraise.MvcExample.Requests
{
    public class FundraiserId : IRequest<Fundraiser>
    {
        public Guid Id { get; set; }
    }
}