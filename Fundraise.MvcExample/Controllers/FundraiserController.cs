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
using MediatR;
using Fundraise.MvcExample.Requests;
using System.Threading.Tasks;

namespace Fundraise.MvcExample.Controllers
{
    public class FundraiserController : Controller
    {

        private ICampaignRepository _campaignRepository;
        private IFundraiserRepository _fundraiserRepository;
        private IDonationRepository _donationRepository;
        private readonly IMediator _mediator;

        public FundraiserController(IMediator mediator, CampaignRepository campaignRepository, FundraiserRepository fundraiserRepository, IDonationRepository donationRepository)
        {
            _mediator = mediator;
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

            var fundraiserViewModel = AutoMapper.Mapper.Map<Fundraiser, FundraiserViewModel>(fundraiser);
            var donations = _donationRepository.GetByFundraiser(fundraiser.Id).ToList();
            fundraiserViewModel.Donations = AutoMapper.Mapper.Map<List<Donation>, List<DonationViewModel>>(donations);

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
        public async Task<ActionResult> DonateAsync(DonateFormViewModel model)
        {
            Donate request = new Donate()
            {
                DonationAmount = model.DonationAmount,
                FundraiserId = model.FundraiserId,
                DonorDisplayName = model.DonorDisplayName,
                StripeToken = model.StripeToken
            };
            request.UserId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : string.Empty;
            bool success = await _mediator.Send(request);

            var fundraiserViewModel = new FundraiserFormViewModel()
            {
                Name = "Need to get Fundraiser name?" // todo
            };
            // AutoMapper.Mapper.Map<Fundraiser, FundraiserFormViewModel>(fundraiser);
            return View("Thanks", fundraiserViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.CampaignDropDown = new SelectList(_campaignRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FundraiserFormViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var fundraiser = _fundraiserRepository.Create(model.Name, model.CampaignId, FundraiserType.Individual, User.Identity.GetUserId());
                return RedirectToAction("Index", new { id = fundraiser.Id });
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
    }
}