using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GalaxySkyCompany.Models;
using GalaxySkyCompany.Services.Planes.Dto;

namespace GalaxySkyCompany.Services.Planes
{
    public class PlaneAppService : AsyncCrudAppService<Plane, PlaneDto, int, PagedAndSortedResultRequestDto, CreatePlaneDto, PlaneDto>, IPlaneAppService
    {
        public PlaneAppService(
            IRepository<Plane> planeRepository) :
            base(planeRepository)
        {

        }

        public async Task<ListResultDto<PlaneDto>> GetAllPlanes()
        {
            var entities = await Repository.GetAllListAsync();
            return new ListResultDto<PlaneDto>(entities.Select(MapToEntityDto).ToList());
        }

        public override async Task<PagedResultDto<PlaneDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<PlaneDto>(
                totalCount,
                entities.Select(p =>
                {
                    var result = MapToEntityDto(p);
                    result.PilotIds = new List<int>(p.PilotPlanes?.Select(pp => pp.PilotId) ?? new List<int>());
                    return result;
                }).ToList()
            );
        }

        public override async Task<PlaneDto> Get(EntityDto<int> input)
        {
            var entity = await Repository.GetAsync(input.Id);
            var result = MapToEntityDto(entity);
            result.PilotIds = new List<int>(entity.PilotPlanes.Select(pp => pp.PilotId));
            return result;
        }

        public override async Task<PlaneDto> Update(PlaneDto input)
        {
            var entity = await GetEntityByIdAsync(input.Id);

            var currentPilotIds = new List<int>(entity.PilotPlanes.Select(pp => pp.PilotId));
            var newPilotIds = new List<int>(input.PilotIds);

            MapToEntity(input, entity);

            // Remove pilots for plane
            foreach (var pilotId in currentPilotIds.Except(newPilotIds))
            {
                var record = entity.PilotPlanes.Single(pp => pp.PilotId == pilotId);
                entity.PilotPlanes.Remove(record);
            }

            // Add pilots for plane
            foreach (var pilotId in newPilotIds.Except(currentPilotIds))
            {
                entity.PilotPlanes.Add(new PilotPlane
                {
                    PilotId = pilotId,
                    PlaneId = input.Id
                });
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task<PlaneDto> Create(CreatePlaneDto input)
        {
            var entity = MapToEntity(input);
            var newPilotIds = new List<int>(input.PilotIds ?? new List<int>());

            await Repository.InsertAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();

            // Add pilots for plane
            foreach (var pilotId in newPilotIds)
            {
                entity.PilotPlanes.Add(new PilotPlane
                {
                    PilotId = pilotId,
                    PlaneId = entity.Id
                });
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }
    }
}
