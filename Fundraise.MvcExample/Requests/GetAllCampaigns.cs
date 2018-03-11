using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MediatR;


namespace Fundraise.MvcExample.Requests
{
    public class GetAllCampaigns : IRequest<List<Fundraise.Core.Entities.Campaign>>
    {
    }
}