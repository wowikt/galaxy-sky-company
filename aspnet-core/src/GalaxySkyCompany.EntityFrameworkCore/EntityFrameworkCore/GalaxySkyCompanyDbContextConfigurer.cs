using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace GalaxySkyCompany.EntityFrameworkCore
{
    public static class GalaxySkyCompanyDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<GalaxySkyCompanyDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<GalaxySkyCompanyDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
