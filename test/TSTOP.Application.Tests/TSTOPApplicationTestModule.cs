using Volo.Abp.Modularity;

namespace TSTOP;

[DependsOn(
    typeof(TSTOPApplicationModule),
    typeof(TSTOPDomainTestModule)
)]
public class TSTOPApplicationTestModule : AbpModule
{

}
