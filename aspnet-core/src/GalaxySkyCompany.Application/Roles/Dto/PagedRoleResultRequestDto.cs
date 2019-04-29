using Abp.Application.Services.Dto;

namespace GalaxySkyCompany.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

