using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Domain.Services
{
    public interface IRandomFillDataService : IDomainService
    {
        Task FillRandomData();
    }
}
