using Abp.AutoMapper;
using GalaxySkyCompany.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GalaxySkyCompany.Services.Planes.Dto
{
    [AutoMapTo(typeof(Plane))]
    public class CreatePlaneDto
    {
        [Required]
        [StringLength(Plane.MaxCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(Plane.MaxTypeLength)]
        public string Type { get; set; }

        [Required]
        [StringLength(Plane.MaxTailNumberLength)]
        public string TailNumber { get; set; }

        [Required]
        [StringLength(Plane.MaxBrandLength)]
        public string Brand { get; set; }

        [Required]
        [StringLength(Plane.MaxModelLength)]
        public string Model { get; set; }

        [StringLength(Plane.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public int AirportId { get; set; }

        public ICollection<int> PilotIds { get; set; }
    }
}
