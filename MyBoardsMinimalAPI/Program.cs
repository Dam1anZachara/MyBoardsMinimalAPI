using Microsoft.EntityFrameworkCore;
using MyBoardsMinimalAPI.Entities;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyBoardsContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnectionString"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<MyBoardsContext>();

var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

// customowa logika seedowania
var users = dbContext.Users.ToList();
if (!users.Any())
{
    var user1 = new User()
    {
        Email = "user1@test.com",
        FullName = "User One",
        Address = new Address()
        {
            City = "Warszawa",
            Street = "Szeroka"
        }
    };

    var user2 = new User()
    {
        Email = "user2@test.com",
        FullName = "User Two",
        Address = new Address()
        {
            City = "Kraków",
            Street = "D³uga"
        }
    };

    dbContext.Users.AddRange(user1, user2);
    dbContext.SaveChanges();
}

var tags = dbContext.Tags.ToList();
if (!tags.Where(t => t.Value == "Service").Any())
{
    var tag1 = new Tag()
    {
        Value = "Service"
    };

    dbContext.Tags.Add(tag1);
    dbContext.SaveChanges();    
}

app.MapGet("data", async (MyBoardsContext db) =>
{
    //var tags = db.Tags.ToList();
    //return tags;

    //var epic = db.Epics.First();
    //var user = db.Users.First(u => u.FullName == "User One");
    //return new { epic, user };

    //var toDoWorkItems = db.WorkItems.Where(w => w.StateId == 1).ToList();
    //return toDoWorkItems;

    //var newComments = await db.Comments.Where(c => c.CreatedDate > new DateTime(2022, 7, 23))
    //.ToListAsync();
    //return newComments;

    //var top5NewestComments = await db.Comments
    //.OrderByDescending(c => c.CreatedDate)
    //.Take(5)
    //.ToListAsync();
    //return top5NewestComments;

    //var statesCount = await db.WorkItems
    //.GroupBy(x => x.StateId)
    //.Select(g => new { stateId = g.Key, count = g.Count() })
    //.ToListAsync();
    //return statesCount;

    // ZADANIA
    //1
    //var epicsOnHold = await db.Epics.Where(e => e.StateId == 4)
    //.OrderBy(p => p.Priority).ToListAsync();
    //return epicsOnHold;
    //2
    //var userTopComments = await db.Users.OrderByDescending(c => c.Comments.Count()).
    //FirstOrDefaultAsync();
    //return userTopComments;
    //lub
    var authorsCommentCounts = await db.Comments
    .GroupBy(c => c.UserId).Select(g => new { g.Key, Count = g.Count() }).ToListAsync();
    var topAuthor = authorsCommentCounts
    .First(a => a.Count == authorsCommentCounts.Max(acc => acc.Count));
    var userDetails = db.Users.First(u => u.Id == topAuthor.Key);
    return new { userDetails, commentCount = topAuthor.Count };
});

app.MapPost("update", async (MyBoardsContext db) =>
{
    Epic epic = await db.Epics.FirstAsync(epic => epic.Id == 1);

    var onHoldState = await db.States.FirstAsync(a => a.Name == "On Hold");
    //epic.Area = "Updateed area";
    //epic.Priority = 1;
    //epic.StartDate = DateTime.Now;
    epic.StateId = onHoldState.Id;

    await db.SaveChangesAsync();

    return epic;
});

app.Run();