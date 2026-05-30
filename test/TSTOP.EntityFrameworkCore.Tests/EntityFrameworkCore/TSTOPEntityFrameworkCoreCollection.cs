using Xunit;

namespace TSTOP.EntityFrameworkCore;

[CollectionDefinition(TSTOPTestConsts.CollectionDefinitionName)]
public class TSTOPEntityFrameworkCoreCollection : ICollectionFixture<TSTOPEntityFrameworkCoreFixture>
{

}
