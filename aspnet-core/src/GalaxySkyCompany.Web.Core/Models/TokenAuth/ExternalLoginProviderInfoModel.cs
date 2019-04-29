using Abp.AutoMapper;
using GalaxySkyCompany.Authentication.External;

namespace GalaxySkyCompany.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
