using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.MvcExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fundraise.MvcExample.Controllers
{
    public class FundraiserController : Controller
    {

        private ICampaignRepository _campaignRepository;
        private FundraiserRepository _fundraiserRepository;

        public FundraiserController(CampaignRepository campaignRepository, FundraiserRepository fundraiserRepository)
        {
            _campaignRepository = campaignRepository;
            _fundraiserRepository = fundraiserRepository;
        }

        // GET: Fundraiser
        public ActionResult Index(Guid? id)
        {
            if (!id.HasValue)
            {
                return View();
            }

            var fundraiser = _fundraiserRepository.FindById(id.Value);
            if (fundraiser == null)
                return HttpNotFound();

            var fundraiserViewModel = AutoMapper.Mapper.Map<Fundraiser, FundraiserFormViewModel>(fundraiser);

            return View("Detail", fundraiserViewModel);
        }
    }
}