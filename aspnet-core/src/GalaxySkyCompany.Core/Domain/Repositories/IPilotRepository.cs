using Abp.Domain.Repositories;
using GalaxySkyCompany.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalaxySkyCompany.Core.Domain.Repositories
{
    public interface IPilotRepository : IRepository<Pilot>
    {
        Task<Pilot> GetWithPilotPlanesAsync(int pilotId);

        Task<IEnumerable<int>> GetPlaneIdsAsync(int pilotId);

        Task AddPlanesAsync(int pilotId, IEnumerable<int> planeIds);

        Task RemovePlanesAsync(int pilotId, IEnumerable<int> planeIds);
    }
}
