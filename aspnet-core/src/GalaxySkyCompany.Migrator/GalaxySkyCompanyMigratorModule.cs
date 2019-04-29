using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GalaxySkyCompany.Configuration;
using GalaxySkyCompany.EntityFrameworkCore;
using GalaxySkyCompany.Migrator.DependencyInjection;

namespace GalaxySkyCompany.Migrator
{
    [DependsOn(typeof(GalaxySkyCompanyEntityFrameworkModule))]
    public class GalaxySkyCompanyMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public GalaxySkyCompanyMigratorModule(GalaxySkyCompanyEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(GalaxySkyCompanyMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                GalaxySkyCompanyConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GalaxySkyCompanyMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
