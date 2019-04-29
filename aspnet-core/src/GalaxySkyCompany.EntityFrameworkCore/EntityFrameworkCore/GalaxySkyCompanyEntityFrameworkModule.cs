using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using GalaxySkyCompany.EntityFrameworkCore.Seed;

namespace GalaxySkyCompany.EntityFrameworkCore
{
    [DependsOn(
        typeof(GalaxySkyCompanyCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class GalaxySkyCompanyEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<GalaxySkyCompanyDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        GalaxySkyCompanyDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        GalaxySkyCompanyDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GalaxySkyCompanyEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
