using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GalaxySkyCompany.Roles.Dto;
using GalaxySkyCompany.Users.Dto;

namespace GalaxySkyCompany.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
