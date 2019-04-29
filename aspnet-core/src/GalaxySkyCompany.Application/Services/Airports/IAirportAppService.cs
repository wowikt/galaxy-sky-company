using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GalaxySkyCompany.Services.Airports.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Airports
{
    public interface IAirportAppService : IAsyncCrudAppService<AirportDto, int, PagedAndSortedResultRequestDto, CreateAirportDto, AirportDto>
    {
    }
}
