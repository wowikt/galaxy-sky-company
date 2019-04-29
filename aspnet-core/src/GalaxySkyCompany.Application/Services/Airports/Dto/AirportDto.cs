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
        public int Id;

        [Required]
        [StringLength(Airport.CodeLength)]
        public virtual string Code { get; set; }

        [Required]
        [StringLength(Airport.MaxNameLehgth)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(Airport.MaxAddressLength)]
        public virtual string Address { get; set; }

        public ICollection<PlaneDto> Planes { get; set; }
    }
}
