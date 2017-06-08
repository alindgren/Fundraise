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
        public void TestMethod()
        {
            Assert.IsTrue(true);
        }


    }
}
