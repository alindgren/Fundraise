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
        private Fundraiser _testFundraiser;

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

            var fundraiserRepository = new FundraiserRepository(context);
            _testFundraiser = fundraiserRepository.Create("test", _testCampaign.Id, FundraiserType.Individual);
        }

        [TestMethod]
        public void CreateDonations()
        {
            var donation = _donationRepository.Create(_testCampaign, null, DonationStatus.Pledged, 10.00, _testCampaign.DefaultCurrencyCode, 10.00, "001", "Alex", "001");
            Assert.IsTrue(donation.DonorDisplayName == "Alex", "donor name matches");
            Assert.IsTrue(donation.Status == DonationStatus.Pledged, "status is correct (Pledged)");
            Assert.IsTrue(donation.Id != null && donation.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Console.WriteLine("donation.Id: " + donation.Id);

            var donation2 = _donationRepository.Create(_testCampaign, null, DonationStatus.Completed, 10.00, "NIO", 299.25, "002", "Test", "002");
            Assert.IsTrue(donation2.DonorDisplayName == "Test", "donor name matches");
            Assert.IsTrue(donation2.Status == DonationStatus.Completed, "status is correct (completed)");
            Assert.IsTrue(donation2.Id != null && donation2.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Console.WriteLine("donation2.Id: " + donation2.Id);
        }

        [TestMethod]
        public void CreateDonationForFundraiser()
        {
            var donation = _donationRepository.Create(_testCampaign, _testFundraiser, DonationStatus.Pledged, 10.00, _testCampaign.DefaultCurrencyCode, 10.00, null, "Alex", "001");
            Assert.IsTrue(donation.Id != null && donation.Id.ToString() != "00000000-0000-0000-0000-000000000000", "id is set");
            Assert.IsTrue(donation.Fundraiser.Id == _testFundraiser.Id, "fundraiser id matches");
            Console.WriteLine("donation.Id: " + donation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateDonationExceptionOnAmountMismatch()
        {
            var donation = _donationRepository.Create(_testCampaign, null, DonationStatus.Pledged, 10.00, _testCampaign.DefaultCurrencyCode, 20.00, "Alex", "001");
        }

        [TestMethod]
        public void GetDonationsByCampaign()
        {
            var donation = _donationRepository.Create(_testCampaign, null, DonationStatus.Pledged, 10.00, _testCampaign.DefaultCurrencyCode, 10.00, "Alex", "001");
            Console.WriteLine("donation created: " + donation.Id);

            var donation2 = _donationRepository.Create(_testCampaign, null, DonationStatus.Completed, 10.00, "NIO", 299.25, "Test", "002");
            Console.WriteLine("donation2 created: " + donation2.Id);

            var donations = _donationRepository.GetByCampaign(_testCampaign.Id);
            Assert.IsInstanceOfType(donations, typeof(IEnumerable<Donation>));
            Assert.IsTrue(donations.Count() == 2, "count is 2");
            foreach (var d in donations)
            {
                Console.WriteLine(d.Id);
            }
        }

        [TestMethod]
        public void GetDonationsByFundraiser()
        {
            var donation = _donationRepository.Create(_testCampaign, _testFundraiser, DonationStatus.Pledged, 10.00, _testCampaign.DefaultCurrencyCode, 10.00, "Alex", "001");
            Console.WriteLine("donation created: " + donation.Id);

            var donation2 = _donationRepository.Create(_testCampaign, null, DonationStatus.Completed, 10.00, "NIO", 299.25, "Test", "002");
            Console.WriteLine("donation2 created: " + donation2.Id);

            var donations = _donationRepository.GetByCampaign(_testCampaign.Id);
            Assert.IsInstanceOfType(donations, typeof(IEnumerable<Donation>));
            Assert.IsTrue(donations.Count() == 2, "count is 2");
            foreach (var d in donations)
            {
                Console.WriteLine(d.Id);
            }
        }


        [TestMethod]
        public void GetDonationsByUser()
        {
            var donation = _donationRepository.Create(_testCampaign, _testFundraiser, DonationStatus.Pledged, 10.00, _testCampaign.DefaultCurrencyCode,  10.00, "user-1", "Alex", "001");
            Console.WriteLine("donation created: " + donation.Id);

            var donation2 = _donationRepository.Create(_testCampaign, null, DonationStatus.Completed, 10.00, "NIO", 299.25, "user-1", "Test", "002");
            Console.WriteLine("donation2 created: " + donation2.Id);

            var donations = _donationRepository.GetByDonor("user-1");
            Assert.IsInstanceOfType(donations, typeof(IEnumerable<Donation>));
            Assert.IsTrue(donations.Count() == 2, "count is 2");
            foreach (var d in donations)
            {
                Console.WriteLine(d.Id);
            }
        }
    }
}
