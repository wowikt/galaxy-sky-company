using Abp.MultiTenancy;
using GalaxySkyCompany.Authorization.Users;

namespace GalaxySkyCompany.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
