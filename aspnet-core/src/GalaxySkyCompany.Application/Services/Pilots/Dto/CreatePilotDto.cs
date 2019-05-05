using Abp.AutoMapper;
using GalaxySkyCompany.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Pilots.Dto
{
    [AutoMapTo(typeof(Pilot))]
    public class CreatePilotDto
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

        public ICollection<int> PlaneIds { get; set; }
    }
}
