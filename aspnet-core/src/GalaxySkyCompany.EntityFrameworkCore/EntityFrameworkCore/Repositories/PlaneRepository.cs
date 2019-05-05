using Abp.EntityFrameworkCore;
using GalaxySkyCompany.Domain.Repositories;
using GalaxySkyCompany.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxySkyCompany.EntityFrameworkCore.Repositories
{
    public class PlaneRepository : GalaxySkyCompanyRepositoryBase<Plane>, IPlaneRepository
    {
        public PlaneRepository(IDbContextProvider<GalaxySkyCompanyDbContext> dbContext) : base(dbContext)
        {
        }

        public async Task<Plane> GetWithPlanePilotsAsync(int planeId)
        {
            return await GetAll().Where(p => p.Id == planeId).Include(p => p.PilotPlanes).FirstOrDefaultAsync();
        }

        public async Task AddPilotsAsync(int planeId, IEnumerable<int> pilotIds)
        {
            if (pilotIds == null || pilotIds.Count() == 0)
            {
                return;
            }

            var pilotPlaneIds = await Context.PilotPlanes
                .Where(pp => pp.PilotId == planeId && pilotIds.Contains(pp.PlaneId))
                .Select(pp => pp.PlaneId)
                .ToListAsync();
            foreach (var pId in pilotIds)
            {
                if (pilotPlaneIds.Contains(pId))
                {
                    continue;
                }

                Context.PilotPlanes.Add(new PilotPlane
                {
                    PilotId = pId,
                    PlaneId = planeId,
                });
            }

            //Context.SaveChanges();
        }

        public async Task RemovePilotsAsync(int planeId, IEnumerable<int> pilotIds)
        {
            if (pilotIds == null || pilotIds.Count() == 0)
            {
                return;
            }

            var planePilots = await Context.PilotPlanes
                .Where(pp => pp.PlaneId == planeId && pilotIds.Contains(pp.PilotId))
                .ToListAsync();
            foreach (var pp in planePilots)
            {
                Context.PilotPlanes.Remove(pp);
            }

            //Context.SaveChanges();
        }

        public async Task<IEnumerable<int>> GetPilotIdsAsync(int planeId)
        {
            return await Context.PilotPlanes.Where(pp => pp.PlaneId == planeId).Select(pp => pp.PilotId).ToListAsync();
        }
    }
}
