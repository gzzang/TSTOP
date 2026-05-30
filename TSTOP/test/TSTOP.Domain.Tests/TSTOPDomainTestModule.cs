using Volo.Abp.Modularity;

namespace TSTOP;

[DependsOn(
    typeof(TSTOPDomainModule),
    typeof(TSTOPTestBaseModule)
)]
public class TSTOPDomainTestModule : AbpModule
{

}
