using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.MvcExample.Models;
using Fundraise.MvcExample.Requests;
using MediatR;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fundraise.MvcExample.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Admin
        public ActionResult Index()
        {
            var campaigns = _mediator.Send(new GetAllCampaigns()).Result;

            var adminViewModel = new AdminViewModel();
            adminViewModel.Campaigns = AutoMapper.Mapper.Map<List<Campaign>, List<CampaignViewModel>>(campaigns);
            foreach (var campaign in adminViewModel.Campaigns)
            {
                var fundraisers = _mediator.Send(new FundraisersByCampaignId(campaign.Id)).Result;
                campaign.Fundraisers = AutoMapper.Mapper.Map<List<Fundraiser>, List<FundraiserViewModel>>(fundraisers);
            }
            return View(adminViewModel);
        }

        // GET: Admin/Details/5
        public ActionResult CampaignDetail(Guid id)
        {
            var campaign = _mediator.Send(new CampaignById(id)).Result;
            var campaignViewModel = AutoMapper.Mapper.Map<Campaign, CampaignFormViewModel>(campaign);
            return View(campaignViewModel);
        }

        // GET: Admin/Create
        public ActionResult CampaignCreate()
        {
            return View(new CampaignFormViewModel());
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult CampaignCreate(CampaignFormViewModel model)
        {
            // todo: validation?
            var request = new CreateCampaign()
            {
                Name = model.Name,
                Description = model.Description,
                DefaultCurrencyCode = model.DefaultCurrencyCode,
                EndDate = model.EndDate,
                IsActive = model.IsActive,
                MoreInfoUrl = model.MoreInfoUrl,
            };
            var ok = _mediator.Send(request).Result;

            return RedirectToAction("Index");
        }

        // GET: Admin/Edit/5
        public ActionResult CampaignEdit(Guid id)
        {
            var campaign = _mediator.Send(new CampaignById(id)).Result;
            var campaignViewModel = AutoMapper.Mapper.Map<Campaign, CampaignFormViewModel>(campaign);
            return View(campaignViewModel);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult CampaignEdit(CampaignFormViewModel model)
        {
            var request = new UpdateCampaign()
            {
                Id = model.Id,
                DefaultCurrencyCode = model.DefaultCurrencyCode,
                Description = model.Description,
                EndDate = model.EndDate,
                IsActive = model.IsActive,
                MoreInfoUrl = model.MoreInfoUrl,
                Name = model.Name
            };
            bool ok = _mediator.Send(request).Result;
            if (ok)
                return RedirectToAction("CampaignDetail", new { id = model.Id });
            else
                return View();
        }

        public ActionResult FundraiserCreate(Guid campaignId)
        {
            return View(new FundraiserFormViewModel() { CampaignId = campaignId });
        }

        [HttpPost]
        public ActionResult FundraiserCreate(FundraiserFormViewModel model)
        {
            var request = new CreateFundraiser()
            {
                Name = model.Name,
                CampaignId = model.CampaignId,
                IsActive = model.IsActive,
                Description = model.Description,
                UserId = User.Identity.GetUserId()
            };
            Guid fundraiserId = _mediator.Send(request).Result;

            return RedirectToAction("Index");
        }

        public ActionResult FundraiserEdit(Guid id)
        {
            var fundraiser = _mediator.Send(new FundraiserId(id)).Result;
            var fundraiserViewModel = AutoMapper.Mapper.Map<Fundraiser, FundraiserFormViewModel>(fundraiser);
            return View(fundraiserViewModel);
        }

        [HttpPost]
        public ActionResult FundraiserEdit(FundraiserFormViewModel model)
        {
            var request = new UpdateFundraiser()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                IsActive = model.IsActive
            };
            bool ok = _mediator.Send(request).Result;
            if (ok)
                return RedirectToAction("Index");
            else
                return View();
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
