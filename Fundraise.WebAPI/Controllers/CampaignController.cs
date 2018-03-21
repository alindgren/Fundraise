using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fundraise.Requests.Campaign;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fundraise.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    public class CampaignController : Controller
    {
        private readonly IMediator _mediator;

        public CampaignController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Core.Entities.Campaign> Get()
        {
            var campaigns = _mediator.Send(new GetAll()).Result;
            return campaigns;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Core.Entities.Campaign Get(Guid id)
        {
            var campaign = _mediator.Send(new GetById(id)).Result;
            return campaign;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
