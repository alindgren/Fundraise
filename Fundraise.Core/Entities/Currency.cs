using System.ComponentModel.DataAnnotations;

namespace Fundraise.Core.Entities
{
    public class Currency
    {
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
