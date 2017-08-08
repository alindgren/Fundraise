using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.MvcExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stripe;

namespace Fundraise.MvcExample.Controllers
{
    public class FundraiserController : Controller
    {

        private ICampaignRepository _campaignRepository;
        private IFundraiserRepository _fundraiserRepository;

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
                var campaigns = _campaignRepository.GetAll().ToList();

                var model = new FundraisersViewModel();
                model.Campaigns = AutoMapper.Mapper.Map<List<Campaign>, List<CampaignViewModel>>(campaigns);
                foreach (var campaign in model.Campaigns)
                {
                    var fundraisers = _fundraiserRepository.FindByCampaign(campaign.Id).ToList();
                    campaign.Fundraisers = AutoMapper.Mapper.Map<List<Fundraiser>, List<FundraiserViewModel>>(fundraisers);
                }

                return View(model);
            }

            var fundraiser = _fundraiserRepository.FindById(id.Value);
            if (fundraiser == null)
                return HttpNotFound();

            var fundraiserViewModel = AutoMapper.Mapper.Map<Fundraiser, FundraiserFormViewModel>(fundraiser);

            return View("Detail", fundraiserViewModel);
        }

        [HttpGet]
        public ActionResult Donate(Guid id)
        {
            var fundraiser = _fundraiserRepository.FindById(id);
            var fundraiserViewModel = AutoMapper.Mapper.Map<Fundraiser, FundraiserFormViewModel>(fundraiser);

            return View(fundraiserViewModel);
        }

        [HttpPost]
        public ActionResult Donate(DonateFormViewModel model)
        {
            var fundraiser = _fundraiserRepository.FindById(model.FundraiserId);

            if (model.DonationAmount > 0)
            {
                var chargeService = new StripeChargeService();
                chargeService.Create(new StripeChargeCreateOptions()
                {
                    Amount = model.DonationAmount * 100,
                    Currency = "usd",
                    Description = fundraiser.Name,
                    SourceTokenOrExistingSourceId = model.StripeToken
                });
            }
            

            var fundraiserViewModel = AutoMapper.Mapper.Map<Fundraiser, FundraiserFormViewModel>(fundraiser);
            return View(fundraiserViewModel);
        }
    }
}