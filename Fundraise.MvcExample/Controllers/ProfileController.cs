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
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
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
            var donations = _mediator.Send(new DonationsByDonorId(userId)).Result;
            model.Donations = AutoMapper.Mapper.Map<List<Donation>, List<DonationViewModel>>(donations);

            // a better way? this is not efficient
            foreach (var donation in model.Donations)
            {
                var fundraiser = _mediator.Send(new FundraiserId(donation.FundraiserId)).Result;

                if (fundraiser != null)
                {
                    donation.FundraiserName = fundraiser.Name;
                }
                var campaign = _mediator.Send<Campaign>(new CampaignById(donation.CampaignId)).Result;
                if (campaign != null)
                {
                    donation.CampaignName = campaign.Name;
                }
            }

            var fundraisers = _mediator.Send(new FundraisersByCreatorId(userId)).Result;
            model.Fundraisers = AutoMapper.Mapper.Map<List<Fundraiser>, List<FundraiserViewModel>>(fundraisers);

            return View(model);
            
        }
    }
}