using Abp.Domain.Values;
using System.Collections.Generic;

namespace GalaxySkyCompany.Models
{
    public class PilotPlane : ValueObject
    {
        public int PilotId { get; set; }

        public virtual Pilot Pilot { get; set; }

        public int PlaneId { get; set; }

        public virtual Plane Plane { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return PilotId;
            yield return PlaneId;
        }
    }
}
