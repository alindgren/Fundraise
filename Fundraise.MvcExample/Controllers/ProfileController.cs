using Fundraise.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Fundraise.MvcExample.Models;
using Fundraise.Core.Entities;
using MediatR;
using Fundraise.MvcExample.Requests;

namespace Fundraise.MvcExample.Controllers
{
    public class ProfileController : Controller
    {
        private ICampaignRepository _campaignRepository;
        private IDonationRepository _donationRepository;
        private readonly IMediator _mediator;

        public ProfileController(CampaignRepository campaignRepository, IDonationRepository donationRepository, IMediator mediator)
        {
            _campaignRepository = campaignRepository;
            _donationRepository = donationRepository;
            _mediator = mediator;
        }

        // GET: Profile
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            string userId = User.Identity.GetUserId();
            var model = new ProfileViewModel();
            var donations =_donationRepository.GetByDonor(userId).ToList();
            model.Donations = AutoMapper.Mapper.Map<List<Donation>, List<DonationViewModel>>(donations);

            // a better way? this is not efficient
            foreach (var donation in model.Donations)
            {
                var fundraiser = _mediator.Send<Fundraiser>(new FundraiserId(donation.FundraiserId)).Result;

                if (fundraiser != null)
                {
                    donation.FundraiserName = fundraiser.Name;
                }
                var campaign = _campaignRepository.FindById(donation.CampaignId);
                if (campaign != null)
                {
                    donation.CampaignName = campaign.Name;
                }
            }

            var fundraisers = _mediator.Send<List<Fundraiser>>(new FundraisersByCreatorId(userId)).Result;
            model.Fundraisers = AutoMapper.Mapper.Map<List<Fundraiser>, List<FundraiserViewModel>>(fundraisers);

            return View(model);
            
        }
    }
}