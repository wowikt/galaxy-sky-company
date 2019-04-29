using Abp.AutoMapper;
using GalaxySkyCompany.Models;
using System.ComponentModel.DataAnnotations;

namespace GalaxySkyCompany.Services.Airports.Dto
{
    [AutoMapTo(typeof(Airport))]
    public class CreateAirportDto
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
