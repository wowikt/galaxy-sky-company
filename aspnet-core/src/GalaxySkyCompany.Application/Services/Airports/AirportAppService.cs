using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GalaxySkyCompany.Models;
using GalaxySkyCompany.Services.Airports.Dto;
using GalaxySkyCompany.Services.Planes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Airports
{
    public class AirportAppService : AsyncCrudAppService<Airport, AirportDto, int, PagedAndSortedResultRequestDto, CreateAirportDto, AirportDto>, IAirportAppService
    {
        private readonly IRepository<Plane> _planeRepository;

        public AirportAppService(IRepository<Airport> repository,
            IRepository<Plane> planeRepository) :
            base(repository)
        {
            _planeRepository = planeRepository;
        }

        public override async Task<PagedResultDto<AirportDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            var result = await base.GetAll(input);

            foreach (var airport in result.Items)
            {
                var planes = await _planeRepository.GetAllListAsync(p => p.AirportId == airport.Id);
                airport.Planes = ObjectMapper.Map<List<PlaneDto>>(planes);
            }

            return result;
        }

        public override async Task<AirportDto> Get(EntityDto<int> input)
        {
            var result = await base.Get(input);

            var planes = await _planeRepository.GetAllListAsync(p => p.AirportId == result.Id);
            result.Planes = ObjectMapper.Map<List<PlaneDto>>(planes);

            return result;
        }
    }
}
