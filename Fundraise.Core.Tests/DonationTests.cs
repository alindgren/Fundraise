using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fundraise.Core.Services;
using Fundraise.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            _testCampaign = campaignRepository.Create("test", "USD", null, null);
        }

        [TestMethod]
        public void CreateDonations()
        {
            var donation = _donationRepository.Create(_testCampaign, DonationStatus.Pledged, 10.00, _testCampaign.DefaultCurrencyCode, 10.00, "Alex", "001");
            Assert.IsTrue(donation.DonorDisplayName == "Alex", "donor name matches");
            Assert.IsTrue(donation.Status == DonationStatus.Pledged, "status is correct (Pledged)");
            Assert.IsTrue(donation.Id != null && donation.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Console.WriteLine("donation.Id: " + donation.Id);

            var donation2 = _donationRepository.Create(_testCampaign, DonationStatus.Completed, 10.00, "NIO", 299.25, "Test", "002");
            Assert.IsTrue(donation2.DonorDisplayName == "Test", "donor name matches");
            Assert.IsTrue(donation2.Status == DonationStatus.Completed, "status is correct (completed)");
            Assert.IsTrue(donation2.Id != null && donation2.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Console.WriteLine("donation2.Id: " + donation2.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateDonationExceptionOnAmountMismatch()
        {
            var donation = _donationRepository.Create(_testCampaign, DonationStatus.Pledged, 10.00, _testCampaign.DefaultCurrencyCode, 20.00, "Alex", "001");
        }

        [TestMethod]
        public void GetAllDonations()
        {
            var donation = _donationRepository.Create(_testCampaign, DonationStatus.Pledged, 10.00, _testCampaign.DefaultCurrencyCode, 10.00, "Alex", "001");
            Console.WriteLine("donation created: " + donation.Id);

            var donation2 = _donationRepository.Create(_testCampaign, DonationStatus.Completed, 10.00, "NIO", 299.25, "Test", "002");
            Console.WriteLine("donation2 created: " + donation2.Id);

            var donations = _donationRepository.GetAll(_testCampaign.Id);
            Assert.IsInstanceOfType(donations, typeof(IEnumerable<Donation>));
            Assert.IsTrue(donations.Count() == 2, "count is 2");
            foreach (var d in donations)
            {
                Console.WriteLine(d.Id);
            }
        }
    }
}
