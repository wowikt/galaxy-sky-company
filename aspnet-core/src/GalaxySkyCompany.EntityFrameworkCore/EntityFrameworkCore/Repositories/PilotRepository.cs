using Abp.EntityFrameworkCore;
using GalaxySkyCompany.Core.Domain.Repositories;
using GalaxySkyCompany.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.EntityFrameworkCore.Repositories
{
    public class PilotRepository : GalaxySkyCompanyRepositoryBase<Pilot>, IPilotRepository
    {
        public PilotRepository(IDbContextProvider<GalaxySkyCompanyDbContext> dbContext) : base(dbContext)
        {
        }

        public async Task<Pilot> GetWithPilotPlanesAsync(int pilotId)
        {
            return await GetAll().Where(p => p.Id == pilotId).Include(p => p.PilotPlanes).FirstOrDefaultAsync();
        }

        public async Task AddPlanesAsync(int pilotId, IEnumerable<int> planeIds)
        {
            if (planeIds == null || planeIds.Count() == 0)
            {
                return;
            }

            var pilotPlaneIds = await Context.PilotPlanes
                .Where(pp => pp.PilotId == pilotId && planeIds.Contains(pp.PlaneId))
                .Select(pp => pp.PlaneId)
                .ToListAsync();
            foreach (var pId in planeIds)
            {
                if (pilotPlaneIds.Contains(pId))
                {
                    continue;
                }

                Context.PilotPlanes.Add(new PilotPlane
                {
                    PilotId = pilotId,
                    PlaneId = pId,
                });
            }
        }

        public async Task RemovePlanesAsync(int pilotId, IEnumerable<int> planeIds)
        {
            if (planeIds == null || planeIds.Count() == 0)
            {
                return;
            }

            var pilotPlanes = await Context.PilotPlanes
                .Where(pp => pp.PilotId == pilotId && planeIds.Contains(pp.PlaneId))
                .ToListAsync();
            foreach (var pp in pilotPlanes)
            {
                Context.PilotPlanes.Remove(pp);
            }
        }

        public async Task<IEnumerable<int>> GetPlaneIdsAsync(int pilotId)
        {
            return await Context.PilotPlanes.Where(pp => pp.PilotId == pilotId).Select(pp => pp.PlaneId).ToListAsync();
        }

    }
}
