using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GalaxySkyCompany.MultiTenancy.Dto;

namespace GalaxySkyCompany.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

