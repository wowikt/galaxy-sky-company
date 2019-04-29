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
    public class PlaneDto
    {
        [Required]
        public int Id;

        [StringLength(Plane.MaxCodeLength)]
        public virtual string Code { get; set; }

        [Required]
        [StringLength(Plane.MaxTypeLength)]
        public virtual string Type { get; set; }

        [Required]
        [StringLength(Plane.MaxTailNumberLength)]
        public virtual string TailNumber { get; set; }

        [Required]
        [StringLength(Plane.MaxBrandLength)]
        public virtual string Brand { get; set; }

        [Required]
        [StringLength(Plane.MaxModelLength)]
        public virtual string Model { get; set; }

        [Required]
        [StringLength(Plane.MaxNameLength)]
        public virtual string Name { get; set; }

        [Required]
        public virtual int AirportId { get; set; }
    }
}
