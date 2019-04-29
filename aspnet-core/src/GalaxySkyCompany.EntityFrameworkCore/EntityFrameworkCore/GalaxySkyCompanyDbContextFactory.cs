using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using GalaxySkyCompany.Configuration;
using GalaxySkyCompany.Web;

namespace GalaxySkyCompany.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class GalaxySkyCompanyDbContextFactory : IDesignTimeDbContextFactory<GalaxySkyCompanyDbContext>
    {
        public GalaxySkyCompanyDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GalaxySkyCompanyDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            GalaxySkyCompanyDbContextConfigurer.Configure(builder, configuration.GetConnectionString(GalaxySkyCompanyConsts.ConnectionStringName));

            return new GalaxySkyCompanyDbContext(builder.Options);
        }
    }
}
