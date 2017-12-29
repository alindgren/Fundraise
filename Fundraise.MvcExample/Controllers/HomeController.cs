using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fundraise.Core.Services;

namespace Fundraise.MvcExample.Controllers
{
    public class HomeController : Controller
    {
        private ICampaignRepository _campaignRepository;
        private FundraiserRepository _fundraiserRepository;

        public HomeController(CampaignRepository campaignRepository, FundraiserRepository fundraiserRepository)
        {
            _campaignRepository = campaignRepository;
            _fundraiserRepository = fundraiserRepository;
        }

        public ActionResult Index()
        {
            //var campaign = _campaignRepository.Create("Nicaragua", "USD");
            //_fundraiserRepository.Create("Alex's fundraiser for school supplies", campaign.Id, Core.Entities.FundraiserType.Individual);
            //_fundraiserRepository.Create("Deanna's fundraiser for school supplies", campaign.Id, Core.Entities.FundraiserType.Individual);


            //var x = _campaignRepository.FindByName("test");
            //var z = _fundraiserRepository.GetAll();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}