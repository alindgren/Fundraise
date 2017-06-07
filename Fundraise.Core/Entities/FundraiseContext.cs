using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fundraise.Core.Entities
{
    public class FundraiseContext : DbContext
    {
        public FundraiseContext(DbContextOptions<FundraiseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Fundraiser> Fundraisers { get; set; }
    }
}
