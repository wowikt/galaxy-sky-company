using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GalaxySkyCompany.Domain.Repositories;
using GalaxySkyCompany.Models;
using GalaxySkyCompany.Services.Planes.Dto;

namespace GalaxySkyCompany.Services.Planes
{
    public class PlaneAppService : AsyncCrudAppService<Plane, PlaneDto, int, PagedAndSortedResultRequestDto, CreatePlaneDto, PlaneDto>, IPlaneAppService
    {
        private readonly List<int> _emptyList = new List<int>();

        public PlaneAppService(
            IPlaneRepository planeRepository) :
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
                    result.PilotIds = new List<int>(p.PilotPlanes?.Select(pp => pp.PilotId) ?? _emptyList);
                    return result;
                }).ToList()
            );
        }

        public override async Task<PlaneDto> Get(EntityDto<int> input)
        {
            var entity = await ((IPlaneRepository)Repository).GetWithPlanePilotsAsync(input.Id);
            var result = MapToEntityDto(entity);
            result.PilotIds = new List<int>(entity.PilotPlanes?.Select(pp => pp.PilotId) ?? _emptyList);
            return result;
        }

        public override async Task<PlaneDto> Update(PlaneDto input)
        {
            var entity = await Repository.GetAsync(input.Id);

            var currentPilotIds = await ((IPlaneRepository)Repository).GetPilotIdsAsync(input.Id);
            var newPilotIds = new List<int>(input.PilotIds ?? _emptyList);

            MapToEntity(input, entity);

            //await CurrentUnitOfWork.SaveChangesAsync();

            // Remove pilots for plane
            await ((IPlaneRepository)Repository).RemovePilotsAsync(input.Id, currentPilotIds.Except(newPilotIds));

            // Add pilots for plane
            await ((IPlaneRepository)Repository).AddPilotsAsync(input.Id, newPilotIds.Except(currentPilotIds));

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }

        public override async Task<PlaneDto> Create(CreatePlaneDto input)
        {
            var entity = MapToEntity(input);
            var newPilotIds = new List<int>(input.PilotIds ?? _emptyList);

            await Repository.InsertAsync(entity);
            //await CurrentUnitOfWork.SaveChangesAsync();

            // Add pilots for plane
            await ((IPlaneRepository)Repository).AddPilotsAsync(entity.Id, newPilotIds);

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(entity);
        }
    }
}
