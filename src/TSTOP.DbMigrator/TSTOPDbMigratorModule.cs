using TSTOP.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace TSTOP.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TSTOPEntityFrameworkCoreModule),
    typeof(TSTOPApplicationContractsModule)
)]
public class TSTOPDbMigratorModule : AbpModule
{
}
