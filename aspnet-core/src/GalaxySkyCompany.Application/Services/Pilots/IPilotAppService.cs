using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GalaxySkyCompany.Services.Pilots.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Pilots
{
    public interface IPilotAppService : IAsyncCrudAppService<PilotDto, int, PagedAndSortedResultRequestDto, CreatePilotDto, PilotDto>
    {
        Task<ListResultDto<PilotDto>> GetAllPilots();
    }
}
