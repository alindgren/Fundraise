using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundraise.Core.Services;
using Fundraise.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fundraise.Core.Tests
{
    [TestClass]
    public class CampaignTests
    {
        private ICampaignRepository _campaignRepository;

        public CampaignTests()
        {
            Init();
        }

        private void Init()
        {
            var builder = new DbContextOptionsBuilder<FundraiseContext>()
            .UseInMemoryDatabase();

            var context = new FundraiseContext(builder.Options);
            _campaignRepository = new CampaignRepository(context);
        }

        [TestMethod]
        public void CreateCampaign()
        {
            var campaign = _campaignRepository.Create("test campaign");
            Assert.IsTrue(campaign.Name == "test campaign", "name matches");
            Assert.IsTrue(campaign.Id != null && campaign.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Assert.IsFalse(campaign.IsActive, "'test campaign' was created but is not active");
            Console.WriteLine("campaign.Id: " + campaign.Id);
        }

        [TestMethod]
        public void CreateCampaignWithEndDate()
        {
            var campaign = _campaignRepository.Create("test campaign with end date", System.DateTime.Now.AddYears(1));
            Assert.IsTrue(campaign.Name == "test campaign with end date", "name matches");
            Assert.IsNotNull(campaign.EndDate);
            Assert.IsTrue(campaign.Id != null && campaign.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Assert.IsFalse(campaign.IsActive, "'test campaign with end date' was created but is not active");
            Console.WriteLine("campaign.Id: " + campaign.Id);
            Console.WriteLine("campaign.EndDate: " + campaign.EndDate.ToLongDateString());
        }

        [TestMethod]
        public void FindCampaignByName()
        {
            var campaigns = _campaignRepository.FindByName("test campaign with end date");
            var campaign = campaigns.FirstOrDefault();
            Assert.IsInstanceOfType(campaign, typeof(Campaign));
        }

        [TestMethod]
        public void Exists()
        {
            Assert.IsTrue(_campaignRepository.Exists("test campaign with end date"));
           // Assert.IsFalse(_campaignRepository.Exists("no match"));
        }
    }
}
