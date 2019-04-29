using System.Threading.Tasks;
using GalaxySkyCompany.Configuration.Dto;

namespace GalaxySkyCompany.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
