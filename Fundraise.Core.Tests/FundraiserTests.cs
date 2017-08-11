using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundraise.Core.Services;
using Fundraise.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fundraise.Core.Tests
{
    [TestClass]
    public class FundraiserTests
    {
        private IFundraiserRepository _fundraiserRepository;
        private Campaign testCampaign;

        public FundraiserTests()
        {
            Init();
        }

        private void Init()
        {
            var builder = new DbContextOptionsBuilder<FundraiseContext>()
            .UseInMemoryDatabase();

            var context = new FundraiseContext(builder.Options);
            _fundraiserRepository = new FundraiserRepository(context);

            var campaignRepository = new CampaignRepository(context);
            testCampaign = campaignRepository.Create("test campaign", "USD", null, DateTime.Now.AddMonths(2));
        }

        [TestMethod]
        public void CreateFundraiser()
        {
            var fundraiser = _fundraiserRepository.Create("test fundraiser", testCampaign.Id, FundraiserType.Individual, "test");
            Assert.IsTrue(fundraiser.Name == "test fundraiser", "name matches");
            Assert.IsTrue(fundraiser.Id != null && fundraiser.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Assert.IsTrue(fundraiser.CampaignId == testCampaign.Id, "campaign id is set");
            //Assert.IsFalse(campaign.IsActive, "'test campaign' was created but is not active");
            Console.WriteLine("fundraiser.Id: " + fundraiser.Id);

            var fundraiser2 = _fundraiserRepository.FindById(fundraiser.Id);
            Assert.IsInstanceOfType(fundraiser2, typeof(Fundraiser));
            Assert.IsTrue(fundraiser.Name == fundraiser2.Name);
            Assert.IsTrue(fundraiser2.CampaignId == testCampaign.Id);
            Console.WriteLine("type: " + fundraiser2.FundraiserType);
        }

        [TestMethod]
        public void CreateFundraiserWithExtendedData()
        {
            var json = JsonConvert.DeserializeObject<JObject>(@"{""test"":""Hello World"",""test2"":123}");
            var fundraiser = _fundraiserRepository.Create("test fundraiser with extended data", testCampaign.Id, FundraiserType.Individual, "test", json);

            var fundraiser2 = _fundraiserRepository.FindById(fundraiser.Id);
            Assert.IsTrue(fundraiser2.ExtendedData.HasValues);
            Console.WriteLine(fundraiser2.ExtendedData["test"].Value<string>());
            Assert.IsTrue(fundraiser2.ExtendedData.Value<string>("test") == "Hello World");
        }

        [TestMethod]
        public void UpdateFundraiserName()
        {
            var origFundraiser = _fundraiserRepository.Create("test fundraiser for updating", testCampaign.Id, FundraiserType.Individual, "test");
            origFundraiser.Name = "new fundraiser name";
            _fundraiserRepository.Update(origFundraiser);
            var updatedCampaign = _fundraiserRepository.FindById(origFundraiser.Id);
            Assert.AreEqual<string>("new fundraiser name", updatedCampaign.Name);
        }
    }
}
