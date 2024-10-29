var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("DatabasePassword").ExcludeFromManifest();

var database = builder.AddPostgres("postgres", password: password)
    //.WithDataVolume("npgsql-issue-repro")
    ;

var databaseOne = database.AddDatabase("one");

var databaseTwo = database.AddDatabase("two");

var databaseThree = database.AddDatabase("three");

builder.AddProject<Projects.SampleWeb>("web")
    .WithReference(databaseOne)
    .WithReference(databaseTwo)
    .WithReference(databaseThree);

builder.Build().Run();
