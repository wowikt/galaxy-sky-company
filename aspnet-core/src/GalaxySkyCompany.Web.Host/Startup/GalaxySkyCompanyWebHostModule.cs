using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using GalaxySkyCompany.Configuration;

namespace GalaxySkyCompany.Web.Host.Startup
{
    [DependsOn(
       typeof(GalaxySkyCompanyWebCoreModule))]
    public class GalaxySkyCompanyWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public GalaxySkyCompanyWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GalaxySkyCompanyWebHostModule).GetAssembly());
        }
    }
}
