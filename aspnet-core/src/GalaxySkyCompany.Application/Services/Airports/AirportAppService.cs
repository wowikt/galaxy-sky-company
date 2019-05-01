using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GalaxySkyCompany.Models;
using GalaxySkyCompany.Services.Airports.Dto;
using GalaxySkyCompany.Services.Pilots.Dto;
using GalaxySkyCompany.Services.Planes.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Airports
{
    public class AirportAppService : 
        AsyncCrudAppService<Airport, AirportDto, int, PagedAndSortedResultRequestDto, CreateAirportDto, AirportDto>, IAirportAppService
    {
        private readonly IRepository<Plane> _planeRepository;
        private readonly IRepository<Pilot> _pilotRepository;

        public AirportAppService(
            IRepository<Airport> airportRepository,
            IRepository<Plane> planeRepository,
            IRepository<Pilot> pilotRepository) :
            base(airportRepository)
        {
            _planeRepository = planeRepository;
            _pilotRepository = pilotRepository;
        }

        public override async Task<PagedResultDto<AirportDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            var result = await base.GetAll(input);

            foreach (var airport in result.Items)
            {
                airport.PlaneCount = (await _planeRepository.GetAllListAsync(p => p.AirportId == airport.Id)).Count;
                airport.PilotCount = (await _pilotRepository.GetAllListAsync(p => p.AirportId == airport.Id)).Count;
            }

            return result;
        }

        public override async Task<AirportDto> Get(EntityDto<int> input)
        {
            var result = await base.Get(input);

            result.PlaneCount = (await _planeRepository.GetAllListAsync(p => p.AirportId == result.Id)).Count;
            result.PilotCount = (await _pilotRepository.GetAllListAsync(p => p.AirportId == result.Id)).Count;

            return result;
        }

        public async Task<AirportDetailsDto> GetAirportDetails(EntityDto<int> input)
        {
            var dataItem = await Repository.GetAsync(input.Id);
            var result = ObjectMapper.Map<AirportDetailsDto>(dataItem);

            result.Planes = ObjectMapper.Map<List<PlaneDto>>(await _planeRepository.GetAllListAsync(p => p.AirportId == result.Id));
            result.Pilots = ObjectMapper.Map<List<PilotDto>>(await _pilotRepository.GetAllListAsync(p => p.AirportId == result.Id));

            return result;
        }

        public async Task<ListResultDto<AirportDto>> GetAllAirports()
        {
            var entities = await Repository.GetAllListAsync();
            return new ListResultDto<AirportDto>(entities.Select(MapToEntityDto).ToList());
        }
    }
}
