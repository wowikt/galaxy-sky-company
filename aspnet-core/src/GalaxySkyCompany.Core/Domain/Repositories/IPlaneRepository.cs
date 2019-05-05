using Abp.Domain.Repositories;
using GalaxySkyCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Domain.Repositories
{
    public interface IPlaneRepository : IRepository<Plane>
    {
        Task<Plane> GetWithPlanePilotsAsync(int planeId);

        Task<IEnumerable<int>> GetPilotIdsAsync(int planeId);

        Task AddPilotsAsync(int planeId, IEnumerable<int> pilotIds);

        Task RemovePilotsAsync(int planeId, IEnumerable<int> pilotIds);
    }
}
