using Volo.Abp.Modularity;

namespace TSTOP;

/* Inherit from this class for your domain layer tests. */
public abstract class TSTOPDomainTestBase<TStartupModule> : TSTOPTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
