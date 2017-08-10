using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.MvcExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stripe;
using Microsoft.AspNet.Identity;


namespace Fundraise.MvcExample.Controllers
{
    public class FundraiserController : Controller
    {

        private ICampaignRepository _campaignRepository;
        private IFundraiserRepository _fundraiserRepository;
        private IDonationRepository _donationRepository;

        public FundraiserController(CampaignRepository campaignRepository, FundraiserRepository fundraiserRepository, IDonationRepository donationRepository)
        {
            _campaignRepository = campaignRepository;
            _fundraiserRepository = fundraiserRepository;
            _donationRepository = donationRepository;
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
            var campaign = _campaignRepository.FindById(fundraiser.CampaignId);

            if (model.DonationAmount > 0)
            {
                var chargeService = new StripeChargeService();
                var charge = chargeService.Create(new StripeChargeCreateOptions()
                {
                    Amount = model.DonationAmount * 100,
                    Currency = "usd",
                    Description = fundraiser.Name,
                    SourceTokenOrExistingSourceId = model.StripeToken
                });
                string userid = null;
                if (User.Identity.IsAuthenticated)
                {
                    userid = User.Identity.GetUserId();
                }
                _donationRepository.Create(campaign, fundraiser, DonationStatus.Completed, model.DonationAmount, "usd", model.DonationAmount, userid, "Alex L.", charge.Id);
            }

            var fundraiserViewModel = AutoMapper.Mapper.Map<Fundraiser, FundraiserFormViewModel>(fundraiser);
            return View("Thanks", fundraiserViewModel);
        }
    }
}