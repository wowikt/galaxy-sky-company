using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GalaxySkyCompany.Models;
using System.ComponentModel.DataAnnotations;

namespace GalaxySkyCompany.Services.Airports.Dto
{
    [AutoMapFrom(typeof(Airport))]
    public class AirportDto : EntityDto
    {
        [Required]
        [StringLength(Airport.CodeLength)]
        public virtual string Code { get; set; }

        [Required]
        [StringLength(Airport.MaxNameLehgth)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(Airport.MaxAddressLength)]
        public virtual string Address { get; set; }
    }
}
