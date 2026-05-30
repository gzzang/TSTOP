using TSTOP.Samples;
using Xunit;

namespace TSTOP.EntityFrameworkCore.Domains;

[Collection(TSTOPTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TSTOPEntityFrameworkCoreTestModule>
{

}
