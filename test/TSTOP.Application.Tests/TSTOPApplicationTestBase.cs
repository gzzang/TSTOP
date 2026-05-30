using Volo.Abp.Modularity;

namespace TSTOP;

public abstract class TSTOPApplicationTestBase<TStartupModule> : TSTOPTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
