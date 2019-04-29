using System.Threading.Tasks;
using Abp.Application.Services;
using GalaxySkyCompany.Sessions.Dto;

namespace GalaxySkyCompany.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
