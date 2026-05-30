using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TSTOP.Data;
using Volo.Abp.DependencyInjection;

namespace TSTOP.EntityFrameworkCore;

public class EntityFrameworkCoreTSTOPDbSchemaMigrator
    : ITSTOPDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreTSTOPDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the TSTOPDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<TSTOPDbContext>()
            .Database
            .MigrateAsync();
    }
}
