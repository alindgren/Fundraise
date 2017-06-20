using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundraise.Core.Services;
using Fundraise.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fundraise.Core.Tests
{
    [TestClass]
    public class DonationTests
    {
        private IDonationRepository _donationRepository;
        private Campaign _testCampaign;

        public DonationTests()
        {
            Init();
        }

        private void Init()
        {
            var builder = new DbContextOptionsBuilder<FundraiseContext>()
            .UseInMemoryDatabase();

            var context = new FundraiseContext(builder.Options);
            _donationRepository = new DonationRepository(context);

            var campaignRepository = new CampaignRepository(context);
            _testCampaign = campaignRepository.Create("test", "USD", null);
        }

        [TestMethod]
        public void CreateDonations()
        {
            var donation = _donationRepository.Create(_testCampaign, DonationStatus.Pledged, "Alex", "001");
            Assert.IsTrue(donation.DonorDisplayName == "Alex", "donor name matches");
            Assert.IsTrue(donation.Status == DonationStatus.Pledged, "status is correct (Pledged)");
            Assert.IsTrue(donation.Id != null && donation.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Console.WriteLine("donation.Id: " + donation.Id);

            var donation2 = _donationRepository.Create(_testCampaign, DonationStatus.Completed, "Test", "002");
            Assert.IsTrue(donation2.DonorDisplayName == "Test", "donor name matches");
            Assert.IsTrue(donation2.Status == DonationStatus.Completed, "status is correct (completed)");
            Assert.IsTrue(donation2.Id != null && donation2.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Console.WriteLine("donation2.Id: " + donation2.Id);
        }
    }
}
