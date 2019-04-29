using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using GalaxySkyCompany.Configuration.Dto;

namespace GalaxySkyCompany.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : GalaxySkyCompanyAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
