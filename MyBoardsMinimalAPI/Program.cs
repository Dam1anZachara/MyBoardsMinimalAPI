using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using MyBoardsMinimalAPI.Entities;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

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
    //var authorsCommentCounts = await db.Comments
    //.GroupBy(c => c.UserId).Select(g => new { g.Key, Count = g.Count() }).ToListAsync();
    //var topAuthor = authorsCommentCounts
    //.First(a => a.Count == authorsCommentCounts.Max(acc => acc.Count));
    //var userDetails = db.Users.First(u => u.Id == topAuthor.Key);
    //return new { userDetails, commentCount = topAuthor.Count };

    //var user = await db.Users
    //.Include(u => u.Comments).ThenInclude(w => w.WorkItem)
    //.Include(a => a.Address)
    //.FirstAsync(u => u.Id == Guid.Parse("68366DBE-0809-490F-CC1D-08DA10AB0E61"));
    //var userComments = await db.Comments.Where(c => c.UserId == user.Id).ToListAsync();

    //var user = await db.Users
    //.FirstAsync(u => u.Id == Guid.Parse("D00D8059-8977-4E5F-CBD2-08DA10AB0E61"));
    //var entries1 = db.ChangeTracker.Entries();
    //user.Email = "test@test.com";
    //var entries2 = db.ChangeTracker.Entries();
    //db.SaveChanges();
    //return user;

    var states = db.States
    .AsNoTracking()
    .ToList();
    var entries = db.ChangeTracker.Entries();

    return states;
});

app.MapPost("update", async (MyBoardsContext db) =>
{
    Epic epic = await db.Epics.FirstAsync(epic => epic.Id == 1);

    var rejectedState = await db.States.FirstAsync(a => a.Name == "Rejected");
    //epic.Area = "Updateed area";
    //epic.Priority = 1;
    //epic.StartDate = DateTime.Now;
    epic.State = rejectedState;

    await db.SaveChangesAsync();

    return epic;
});

app.MapPost("create", async (MyBoardsContext db) =>
{
    //Tag mvcTag = new Tag()
    //{
    //    Value = "MVC"
    //};
    //Tag aspTag = new Tag()
    //{
    //    Value = "ASP"
    //};
    //var tags = new List<Tag>() { mvcTag, aspTag };
    //await db.Tags.AddRangeAsync(tags);
    //await db.SaveChangesAsync();

    //return tags;

    var address = new Address()
    {
        Id = Guid.Parse("b323dd7c-776a-4cfc-a92a-12df154b4a2c"),
        City = "Kraków",
        Country = "Poland",
        Street = "D³uga"
    };

    var user = new User()
    {
        Email = "user@test.com",
        FullName = "Test User",
        Address = address,
    };

    db.Users.Add(user);
    await db.SaveChangesAsync();

    return user;
});

app.MapDelete("delete", async (MyBoardsContext db) =>
{
    //var workItemTags = await db.WorkItemTag.Where(c => c.WorkItemId == 12).ToListAsync();
    //db.WorkItemTag.RemoveRange(workItemTags);
    //var workItem = await db.WorkItems.FirstAsync(c => c.Id == 16);
    //db.RemoveRange(workItem);

    //var user = await db.Users
    //.Include(u => u.Comments)
    //.FirstAsync(u => u.Id == Guid.Parse("4EBB526D-2196-41E1-CBDA-08DA10AB0E61"));

    //var userComments = db.Comments.Where(c => c.UserId == user.Id).ToList();
    //db.RemoveRange(userComments);
    //await db.SaveChangesAsync();
    //db.Users.Remove(user);
    //await db.SaveChangesAsync();

    var workItem = new Epic()
    {
        Id = 2
    };
    var entry = db.Attach(workItem);
    entry.State = EntityState.Deleted;
    db.SaveChanges();

    return workItem;
});

app.Run();