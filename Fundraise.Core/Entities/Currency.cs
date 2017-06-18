using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fundraise.Core.Entities
{
    public class Currency
    {
        public Currency()
        {
        }

        public Currency(string code, string symbol, string name)
        {
            Code = code;
            Symbol = symbol;
            Name = name;
        }

        [Key]
        public string Code { get; set; }

        [Required]
        [MaxLength(512)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; }
    }
}
