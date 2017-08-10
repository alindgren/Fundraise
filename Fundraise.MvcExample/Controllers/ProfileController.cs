using Fundraise.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Fundraise.MvcExample.Controllers
{
    public class ProfileController : Controller
    {


        private ICampaignRepository _campaignRepository;
        private IFundraiserRepository _fundraiserRepository;
        private IDonationRepository _donationRepository;

        public ProfileController(CampaignRepository campaignRepository, FundraiserRepository fundraiserRepository, IDonationRepository donationRepository)
        {
            _campaignRepository = campaignRepository;
            _fundraiserRepository = fundraiserRepository;
            _donationRepository = donationRepository;
        }

        // GET: Profile
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
            
        }
    }
}