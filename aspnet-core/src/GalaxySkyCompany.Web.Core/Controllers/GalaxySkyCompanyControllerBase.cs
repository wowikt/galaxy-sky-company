using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace GalaxySkyCompany.Controllers
{
    public abstract class GalaxySkyCompanyControllerBase: AbpController
    {
        protected GalaxySkyCompanyControllerBase()
        {
            LocalizationSourceName = GalaxySkyCompanyConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
