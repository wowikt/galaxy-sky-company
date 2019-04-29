using Abp.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GalaxySkyCompany.Models
{
    public class Pilot : Entity
    {
        public const int MaxCodeLength = 10;
        public const int MaxNumLength = 20;
        public const int MaxNameLength = 50;
        public const int MaxAddressLength = 200;

        [Required]
        [StringLength(MaxCodeLength)]
        public virtual string Code { get; set; }

        [Required]
        [StringLength(MaxNumLength)]
        public virtual string Num { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(MaxAddressLength)]
        public virtual string Address { get; set; }

        [Required]
        public virtual int AirportId { get; set; }

        public virtual Airport Airport { get; set; }

        public virtual ICollection<PilotPlane> PilotPlanes { get; set; }
    }
}
