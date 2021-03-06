﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using GalaxySkyCompany.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Planes.Dto
{
    [AutoMap(typeof(Plane))]
    public class PlaneDto : EntityDto
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
