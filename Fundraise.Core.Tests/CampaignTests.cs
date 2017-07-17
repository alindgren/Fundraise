using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundraise.Core.Services;
using Fundraise.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fundraise.Core.Tests
{
    [TestClass]
    public class CampaignTests
    {
        private ICampaignRepository _campaignRepository;
        private Currency usd;

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

            var currencyRepository = new CurrencyRepository(context);
            usd = currencyRepository.FindByCode("USD");
            if (usd == null)
                usd = currencyRepository.Create("USD", "$", "US Dollar");
        }

        [TestMethod]
        public void CreateCampaign()
        {
            var campaign = _campaignRepository.Create("test campaign", usd.Code);
            Assert.IsTrue(campaign.Name == "test campaign", "name matches");
            Assert.IsTrue(campaign.Id != null && campaign.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Assert.IsFalse(campaign.IsActive, "'test campaign' was created but is not active");
            Console.WriteLine("campaign.Id: " + campaign.Id);
        }

        [TestMethod]
        public void CreateCampaignWithEndDate()
        {
            var campaign = _campaignRepository.Create("test campaign with end date", usd.Code, null, System.DateTime.Now.AddYears(1));
            Assert.IsTrue(campaign.Name == "test campaign with end date", "name matches");
            Assert.IsNotNull(campaign.EndDate);
            Assert.IsTrue(campaign.Id != null && campaign.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Assert.IsFalse(campaign.IsActive, "'test campaign with end date' was created but is not active");
            Console.WriteLine("campaign.Id: " + campaign.Id);
            Console.WriteLine("campaign.EndDate: " + campaign.EndDate.Value.ToLongDateString());
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
            _campaignRepository.Create("test campaign exists", usd.Code);
            Assert.IsTrue(_campaignRepository.Exists("test campaign exists"));
            Assert.IsFalse(_campaignRepository.Exists("no match"));
        }

        [TestMethod]
        public void UpdateCampaignName()
        {
            var origCampaign =_campaignRepository.Create("test campaign for updating", usd.Code);
            origCampaign.Name = "new campaign name";
            _campaignRepository.Update(origCampaign);
            var updatedCampaign = _campaignRepository.FindById(origCampaign.Id);
            Assert.AreEqual<string>("new campaign name", updatedCampaign.Name);
        }

        [TestMethod]
        public void CreateAndCloseCampaign()
        {
            var campaign = _campaignRepository.Create("test to close campaign", usd.Code);
            _campaignRepository.Close(campaign.Id);

            var campaign2 = _campaignRepository.FindById(campaign.Id);
            Assert.IsFalse(campaign2.IsActive, "'test to close campaign' IsActive should be false");
        }

        [TestMethod]
        public void FindCampaignByIdReturnNull()
        {
            var campaign = _campaignRepository.FindById(Guid.NewGuid());
            Assert.IsNull(campaign);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CloseCampaignThrowExceptionDoesNotExist()
        {
            _campaignRepository.Close(Guid.NewGuid());
        }

        [TestMethod]
        public void CreateCampaignWithExtendedData()
        {
            var json = JsonConvert.DeserializeObject<JObject>(@"{""test"":""Hello World"",""test2"":123}");
            var campaign = _campaignRepository.Create("campaign with extended data", usd.Code, json);

            var campaign2 = _campaignRepository.FindById(campaign.Id);
            Assert.IsTrue(campaign2.ExtendedData.HasValues);
            Console.WriteLine(campaign2.ExtendedData["test"].Value<string>());
            Assert.IsTrue(campaign2.ExtendedData.Value<string>("test") == "Hello World");
        }

        [TestMethod]
        public void SetCampaignExtendedDataNull()
        {
            var json = JsonConvert.DeserializeObject<JObject>(@"{""test"":""Hello World"",""test2"":123}");
            var campaign = _campaignRepository.Create("campaign with extended data", usd.Code, json);

            var campaign2 = _campaignRepository.FindById(campaign.Id);
            campaign2.ExtendedData = null;
            _campaignRepository.Update(campaign2);

            var campaign3 = _campaignRepository.FindById(campaign.Id);
            Assert.IsFalse(campaign3.ExtendedData.HasValues);
        }
    }
}
