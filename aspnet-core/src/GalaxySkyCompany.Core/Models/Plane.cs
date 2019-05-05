using Abp.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GalaxySkyCompany.Models
{
    public class Plane : Entity
    {
        public const int MaxCodeLength = 10;
        public const int MaxTypeLength = 20;
        public const int MaxTailNumberLength = 20;
        public const int MaxBrandLength = 20;
        public const int MaxModelLength = 20;
        public const int MaxNameLength = 200;

        [Required]
        [StringLength(MaxCodeLength)]
        public virtual string Code { get; set; }

        [Required]
        [StringLength(MaxTypeLength)]
        public virtual string Type { get; set; }

        [Required]
        [StringLength(MaxTailNumberLength)]
        public virtual string TailNumber { get; set; }

        [Required]
        [StringLength(MaxBrandLength)]
        public virtual string Brand { get; set; }

        [Required]
        [StringLength(MaxModelLength)]
        public virtual string Model { get; set; }

        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        [Required]
        public virtual int AirportId { get; set; }

        public virtual Airport Airport { get; set; }

        public virtual ICollection<PilotPlane> PilotPlanes { get; set; }
    }
}
