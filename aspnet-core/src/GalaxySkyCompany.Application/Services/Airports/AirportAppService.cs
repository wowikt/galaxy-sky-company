using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GalaxySkyCompany.Models;
using GalaxySkyCompany.Services.Airports.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Airports
{
    public class AirportAppService : AsyncCrudAppService<Airport, AirportDto, int, PagedAndSortedResultRequestDto, CreateAirportDto, AirportDto>, IAirportAppService
    {
        public AirportAppService(IRepository<Airport> repository) :
            base(repository)
        {

        }
    }
}
