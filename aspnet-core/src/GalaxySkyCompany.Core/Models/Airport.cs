using Abp.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GalaxySkyCompany.Models
{
    public class Airport : Entity
    {
        public const int CodeLength = 3;
        public const int MaxNameLehgth = 50;
        public const int MaxAddressLength = 200;

        [Required]
        [StringLength(CodeLength)]
        public virtual string Code { get; set; }

        [Required]
        [StringLength(MaxNameLehgth)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(MaxAddressLength)]
        public virtual string Address { get; set; }

        public virtual ICollection<Pilot> Pilots { get; set; }

        public virtual ICollection<Plane> Planes { get; set; }
    }
}
