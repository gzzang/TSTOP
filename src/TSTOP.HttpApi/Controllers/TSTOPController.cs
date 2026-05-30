using TSTOP.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TSTOP.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TSTOPController : AbpControllerBase
{
    protected TSTOPController()
    {
        LocalizationResource = typeof(TSTOPResource);
    }
}
