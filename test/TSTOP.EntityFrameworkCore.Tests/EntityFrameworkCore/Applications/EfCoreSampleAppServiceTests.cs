using TSTOP.Samples;
using Xunit;

namespace TSTOP.EntityFrameworkCore.Applications;

[Collection(TSTOPTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TSTOPEntityFrameworkCoreTestModule>
{

}
