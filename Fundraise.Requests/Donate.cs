using System;
using MediatR;

namespace Fundraise.Requests
{
    public class Donate : IRequest<bool>
    {
        public string UserId { get; set; }
        public Guid FundraiserId { get; set; }
        public string StripeToken { get; set; }

        /// <summary>
        /// Donation amount in dollars
        /// </summary>
        public int DonationAmount { get; set; }

        public string DonorDisplayName { get; set; }
    }
}