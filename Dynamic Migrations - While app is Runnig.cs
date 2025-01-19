//Every time the application starts, it will check if the database is up to date with the latest migrations
//and apply them if necessary.
//This is useful when deploying the application to a new environment, where the database may not exist yet.
//It will create the database and apply the migrations automatically.
//This is done by calling the MigrateAsync method on the database context.
//This method will apply any pending migrations to the database.

IServiceScope serviceScope = app.Services.CreateScope();
EventMiDbContext context = serviceScope.ServiceProvider.GetRequiredService<EventMiDbContext>();
await context.Database.MigrateAsync();