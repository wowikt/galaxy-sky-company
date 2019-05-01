using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GalaxySkyCompany.Services.Planes.Dto;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Planes
{
    public interface IPlaneAppService : IAsyncCrudAppService<PlaneDto, int, PagedAndSortedResultRequestDto, CreatePlaneDto, PlaneDto>
    {
        Task<ListResultDto<PlaneDto>> GetAllPlanes();
    }
}
