using Microsoft.Extensions.Localization;
using TSTOP.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace TSTOP;

[Dependency(ReplaceServices = true)]
public class TSTOPBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<TSTOPResource> _localizer;

    public TSTOPBrandingProvider(IStringLocalizer<TSTOPResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
