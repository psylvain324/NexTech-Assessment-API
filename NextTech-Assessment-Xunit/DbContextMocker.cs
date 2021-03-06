﻿using Microsoft.EntityFrameworkCore;
using NexTechAssessmentAPI.Data;

namespace NexTechAssessmentAPI.UnitTests
{
    public static class DbContextMocker
    {
        public static DatabaseContext GetTestDatabaseContext(string dbName)
        {
            DbContextOptions<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            DatabaseContext dbContext = new DatabaseContext(options);
            dbContext.Seed();
            return dbContext;
        }
    }
}