using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GalaxySkyCompany.Models;
using GalaxySkyCompany.Services.Pilots.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Planes.Dto
{
    [AutoMapFrom(typeof(Plane))]
    public class PlaneDetailsDto : EntityDto
    {
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

        [Required]
        [StringLength(Plane.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        public int AirportId { get; set; }

        public ICollection<PilotDto> Pilots { get; set; }
    }
}
