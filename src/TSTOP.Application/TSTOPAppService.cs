using TSTOP.Localization;
using Volo.Abp.Application.Services;

namespace TSTOP;

/* Inherit your application services from this class.
 */
public abstract class TSTOPAppService : ApplicationService
{
    protected TSTOPAppService()
    {
        LocalizationResource = typeof(TSTOPResource);
    }
}
