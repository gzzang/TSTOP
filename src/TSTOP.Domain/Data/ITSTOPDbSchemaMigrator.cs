using System.Threading.Tasks;

namespace TSTOP.Data;

public interface ITSTOPDbSchemaMigrator
{
    Task MigrateAsync();
}
