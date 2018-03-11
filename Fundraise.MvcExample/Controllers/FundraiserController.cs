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
        private readonly IMediator _mediator;

        public FundraiserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Fundraiser
        public ActionResult Index(Guid? id)
        {
            if (!id.HasValue)
            {
                var campaigns = _mediator.Send<List<Campaign>>(new GetAllCampaigns()).Result;

                var model = new FundraisersViewModel();
                model.Campaigns = AutoMapper.Mapper.Map<List<Campaign>, List<CampaignViewModel>>(campaigns);
                foreach (var campaign in model.Campaigns)
                {
                    var fundraisers = _mediator.Send<List<Fundraiser>>(new FundraisersByCampaignId(campaign.Id)).Result;
                    campaign.Fundraisers = AutoMapper.Mapper.Map<List<Fundraiser>, List<FundraiserViewModel>>(fundraisers);
                }

                return View(model);
            }

            var request = new FundraiserId()
            {
                Id = id.Value
            };

            var fundraiser = _mediator.Send<Fundraiser>(request).Result;
            if (fundraiser == null)
                return HttpNotFound();

            var fundraiserViewModel = AutoMapper.Mapper.Map<Fundraiser, FundraiserViewModel>(fundraiser);
            var donations = _mediator.Send<List<Donation>>(new GetDonationsByFundraiserId(request.Id)).Result;
            fundraiserViewModel.Donations = AutoMapper.Mapper.Map<List<Donation>, List<DonationViewModel>>(donations);

            return View("Detail", fundraiserViewModel);
        }

        [HttpGet]
        public ActionResult Donate(Guid id)
        {
            var request = new FundraiserId()
            {
                Id = id
            };

            var fundraiser = _mediator.Send<Fundraiser>(request).Result;
            var fundraiserViewModel = AutoMapper.Mapper.Map<Fundraiser, FundraiserFormViewModel>(fundraiser);

            return View(fundraiserViewModel);
        }

        [HttpPost]
        public ActionResult Donate(DonateFormViewModel model)
        {
            Donate request = new Donate()
            {
                DonationAmount = model.DonationAmount,
                FundraiserId = model.FundraiserId,
                DonorDisplayName = model.DonorDisplayName,
                StripeToken = model.StripeToken
            };
            request.UserId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : string.Empty;
            bool success = _mediator.Send<bool>(request).Result;

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
            var campaigns = _mediator.Send<List<Campaign>>(new GetAllCampaigns()).Result;

            ViewBag.CampaignDropDown = new SelectList(campaigns, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FundraiserFormViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                var request = new CreateFundraiser()
                {
                    Name = model.Name,
                    CampaignId = model.CampaignId,
                    UserId = User.Identity.GetUserId()
                };
                Guid fundraiserId = _mediator.Send<Guid>(request).Result;
                return RedirectToAction("Index", new { id = fundraiserId });
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
    }
}