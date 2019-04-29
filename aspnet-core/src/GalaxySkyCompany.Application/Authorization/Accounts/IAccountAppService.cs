using System.Threading.Tasks;
using Abp.Application.Services;
using GalaxySkyCompany.Authorization.Accounts.Dto;

namespace GalaxySkyCompany.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
