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
    public class AdminController : Controller
    {
        private ICampaignRepository _campaignRepository;
        private FundraiserRepository _fundraiserRepository;

        public AdminController(CampaignRepository campaignRepository, FundraiserRepository fundraiserRepository)
        {
            _campaignRepository = campaignRepository;
            _fundraiserRepository = fundraiserRepository;
        }

        // GET: Admin
        public ActionResult Index()
        {
            var campaigns = _campaignRepository.GetAll().ToList();
            var adminViewModel = new AdminViewModel();
            adminViewModel.Campaigns = AutoMapper.Mapper.Map<List<Campaign>, List<CampaignViewModel>>(campaigns);
            foreach (var campaign in adminViewModel.Campaigns)
            {
                var fundraisers = _fundraiserRepository.FindByCampaign(campaign.Id).ToList();
                campaign.Fundraisers = AutoMapper.Mapper.Map<List<Fundraiser>, List<FundraiserViewModel>>(fundraisers);
            }
            return View(adminViewModel);
        }

        // GET: Admin/Details/5
        public ActionResult CampaignDetail(Guid id)
        {
            var campaign = _campaignRepository.FindById(id);
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
            try
            {
                var campaign = _campaignRepository.Create(model.Name, model.DefaultCurrencyCode);
                campaign.Description = model.Description;
                _campaignRepository.Update(campaign);

                return RedirectToAction("CampaignDetail", new { id = campaign.Id });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult CampaignEdit(Guid id)
        {
            var campaign = _campaignRepository.FindById(id);
            var campaignViewModel = AutoMapper.Mapper.Map<Campaign, CampaignFormViewModel>(campaign);
            return View(campaignViewModel);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult CampaignEdit(CampaignFormViewModel model)
        {
            try
            {
                var campaign = _campaignRepository.FindById(model.Id);
                campaign.Name = model.Name;
                campaign.Description = model.Description;
                campaign.DefaultCurrencyCode = model.DefaultCurrencyCode;
                _campaignRepository.Update(campaign);

                return RedirectToAction("CampaignDetail", new { id = campaign.Id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult FundraiserCreate(Guid campaignId)
        {
            return View(new FundraiserFormViewModel() { CampaignId = campaignId });
        }

        [HttpPost]
        public ActionResult FundraiserCreate(FundraiserFormViewModel model)
        {
            try
            {
                var fundraiser = _fundraiserRepository.Create(model.Name, model.CampaignId, FundraiserType.Individual);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult FundraiserEdit(Guid id)
        {
            var fundraiser = _fundraiserRepository.FindById(id);
            var fundraiserViewModel = AutoMapper.Mapper.Map<Fundraiser, FundraiserFormViewModel>(fundraiser);
            return View(fundraiserViewModel);
        }

        [HttpPost]
        public ActionResult FundraiserEdit(FundraiserFormViewModel model)
        {
            try
            {
                var fundraiser = _fundraiserRepository.FindById(model.Id);
                fundraiser.Name = model.Name;
                fundraiser.Description = model.Description;
                fundraiser.IsActive = model.IsActive;
                _fundraiserRepository.Update(fundraiser);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
