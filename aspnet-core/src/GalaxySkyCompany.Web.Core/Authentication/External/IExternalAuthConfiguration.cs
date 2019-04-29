using System.Collections.Generic;

namespace GalaxySkyCompany.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
