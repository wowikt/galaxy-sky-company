using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GalaxySkyCompany.Models;
using GalaxySkyCompany.Services.Pilots.Dto;
using GalaxySkyCompany.Services.Planes.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GalaxySkyCompany.Services.Airports.Dto
{
    [AutoMapFrom(typeof(Airport))]
    public class AirportDetailsDto : EntityDto
    {
        [Required]
        [StringLength(Airport.CodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(Airport.MaxNameLehgth)]
        public string Name { get; set; }

        [Required]
        [StringLength(Airport.MaxAddressLength)]
        public string Address { get; set; }

        public ICollection<PlaneDto> Planes { get; set; }

        public ICollection<PilotDto> Pilots { get; set; }
    }
}
