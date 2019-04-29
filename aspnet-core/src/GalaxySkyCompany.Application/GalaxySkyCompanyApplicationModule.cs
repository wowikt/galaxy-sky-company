using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GalaxySkyCompany.Authorization;

namespace GalaxySkyCompany
{
    [DependsOn(
        typeof(GalaxySkyCompanyCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class GalaxySkyCompanyApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<GalaxySkyCompanyAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(GalaxySkyCompanyApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
