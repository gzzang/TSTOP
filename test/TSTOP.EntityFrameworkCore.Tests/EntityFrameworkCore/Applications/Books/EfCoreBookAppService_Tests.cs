using TSTOP.Books;
using Xunit;

namespace TSTOP.EntityFrameworkCore.Applications.Books;

[Collection(TSTOPTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<TSTOPEntityFrameworkCoreTestModule>
{

}