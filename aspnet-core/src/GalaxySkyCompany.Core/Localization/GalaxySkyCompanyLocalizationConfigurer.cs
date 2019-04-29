using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace GalaxySkyCompany.Localization
{
    public static class GalaxySkyCompanyLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(GalaxySkyCompanyConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(GalaxySkyCompanyLocalizationConfigurer).GetAssembly(),
                        "GalaxySkyCompany.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
