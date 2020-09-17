using Microsoft.EntityFrameworkCore;
using NexTech_Assessment_API.Data;

namespace NexTech_Assessment_API.UnitTests
{
    public static class DbContextMocker
    {
        public static DatabaseContext GetTestDatabaseContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var dbContext = new DatabaseContext(options);
            dbContext.Seed();
            return dbContext;
        }
    }
}