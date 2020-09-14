using Microsoft.EntityFrameworkCore;
using NexTech_Assessment_API.Data;

namespace NexTech_Assessment_API.UnitTests
{
    public static class DbContextMocker
    {
        public static DatabaseContext GetTestDatabaseContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "NextTechAssessmentTestDb")
                .Options;

            // Create instance of DbContext
            var dbContext = new DatabaseContext(options);

            // Add entities in memory
            dbContext.Seed();
            return dbContext;
        }
    }
}