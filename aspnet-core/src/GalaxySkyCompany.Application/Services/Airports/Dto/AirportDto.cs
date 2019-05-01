using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GalaxySkyCompany.Models;
using GalaxySkyCompany.Services.Planes.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GalaxySkyCompany.Services.Airports.Dto
{
    [AutoMap(typeof(Airport))]
    public class AirportDto : EntityDto
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

        public int PlaneCount { get; set; }

        public int PilotCount { get; set; }
    }
}
