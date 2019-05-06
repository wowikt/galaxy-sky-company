using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Home
{
    public interface IHomeAppService : IApplicationService
    {
        Task GenerateRandomDataAsync();
    }
}
