using System;
using System.ComponentModel.DataAnnotations;

namespace Fundraise.Core.Entities
{
    public class Fundraiser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
