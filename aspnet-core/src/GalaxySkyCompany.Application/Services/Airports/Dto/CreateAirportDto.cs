﻿using Abp.AutoMapper;
using GalaxySkyCompany.Models;
using System.ComponentModel.DataAnnotations;

namespace GalaxySkyCompany.Services.Airports.Dto
{
    [AutoMapTo(typeof(Airport))]
    public class CreateAirportDto
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
    }
}
