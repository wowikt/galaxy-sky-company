﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GalaxySkyCompany.Models;
using GalaxySkyCompany.Services.Planes.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Pilots.Dto
{
    [AutoMapFrom(typeof(Pilot))]
    public class PilotDetailsDto : EntityDto
    {
        [Required]
        [StringLength(Pilot.MaxCodeLength)]
        public string Code { get; set; }

        [Required]
        [StringLength(Pilot.MaxNumLength)]
        public string Num { get; set; }

        [Required]
        [StringLength(Pilot.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(Pilot.MaxAddressLength)]
        public string Address { get; set; }

        [Required]
        public int AirportId { get; set; }

        public ICollection<PlaneDto> Planes { get; set; }
    }
}
