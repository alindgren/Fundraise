using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fundraise.MvcExample.Models
{
    public class DonateFormViewModel
    {
        public Guid FundraiserId { get; set; }
        public string StripeToken { get; set; }

        /// <summary>
        /// Donation amount in dollars
        /// </summary>
        public int DonationAmount { get; set; }
    }
}