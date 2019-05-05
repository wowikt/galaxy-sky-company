using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GalaxySkyCompany.Core.Domain.Repositories;
using GalaxySkyCompany.Models;
using GalaxySkyCompany.Services.Pilots.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Pilots
{
    public class PilotAppService : AsyncCrudAppService<Pilot, PilotDto, int, PagedAndSortedResultRequestDto, CreatePilotDto, PilotDto>, IPilotAppService
    {
        private readonly List<int> _emptyList = new List<int>();

        public PilotAppService(
            IPilotRepository pilotRepository) :
            base(pilotRepository)
        {

        }

        public async Task<ListResultDto<PilotDto>> GetAllPilots()
        {
            var entities = await Repository.GetAllListAsync();
            return new ListResultDto<PilotDto>(entities.Select(MapToEntityDto).ToList());
        }

        public override async Task<PagedResultDto<PilotDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<PilotDto>(
                totalCount,
                entities.Select(p =>
                {
                    var result = MapToEntityDto(p);
                    result.PlaneIds = new List<int>(p.PilotPlanes?.Select(pp => pp.PlaneId) ?? _emptyList);
                    return result;
                }).ToList()
            );
        }

        public override async Task<PilotDto> Get(EntityDto<int> input)
        {
            var entity = await ((IPilotRepository)Repository).GetWithPilotPlanesAsync(input.Id);
            var result = MapToEntityDto(entity);
            result.PlaneIds = new List<int>(entity.PilotPlanes?.Select(pp => pp.PlaneId) ?? _emptyList);
            return result;
        }

        public override async Task<PilotDto> Update(PilotDto input)
        {
            var entity = await Repository.GetAsync(input.Id);
            var currentPlaneIds = await ((IPilotRepository)Repository).GetPlaneIdsAsync(input.Id);
            var newPlaneIds = new List<int>(input.PlaneIds ?? _emptyList);

            MapToEntity(input, entity);

            await CurrentUnitOfWork.SaveChangesAsync();

            // Remove planes for pilot
            await ((IPilotRepository)Repository).RemovePlanesAsync(input.Id, currentPlaneIds.Except(newPlaneIds));

            // Add planes for pilot
            await ((IPilotRepository)Repository).AddPlanesAsync(input.Id, newPlaneIds.Except(currentPlaneIds));

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task<PilotDto> Create(CreatePilotDto input)
        {
            var entity = MapToEntity(input);
            var newPlaneIds = new List<int>(input.PlaneIds ?? _emptyList);

            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            // Add pilots for plane
            await ((IPilotRepository)Repository).AddPlanesAsync(entity.Id, newPlaneIds);

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }
    }
}
