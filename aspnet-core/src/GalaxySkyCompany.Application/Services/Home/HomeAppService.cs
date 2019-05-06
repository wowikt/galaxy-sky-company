using GalaxySkyCompany.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Services.Home
{
    public class HomeAppService : IHomeAppService
    {
        private readonly IRandomFillDataService _randomFillDataService;

        public HomeAppService(IRandomFillDataService randomFillDataService)
        {
            _randomFillDataService = randomFillDataService;
        }

        public async Task GenerateRandomDataAsync()
        {
            await _randomFillDataService.FillRandomData();
        }
    }
}
