using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fundraise.MvcExample.Controllers
{
    public class HomeController : Controller
    {
        private Core.Services.ICampaignRepository _campaignRepository; 

        //public HomeController() { }
        public HomeController(Fundraise.Core.Services.CampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public ActionResult Index()
        {
            var x = _campaignRepository.FindByName("test");

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