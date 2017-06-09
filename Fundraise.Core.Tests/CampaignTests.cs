using System;
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
            Console.WriteLine("campaign.Id: " + campaign.Id);
        }
    }
}
