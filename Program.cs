using MongoDB.Bson;
using Nyneo_Web.DataAccess;
using Nyneo_Web.Models;
using Nyneo_Web.Services;
using Nyneo_Web.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);


if (builder.Environment.IsDevelopment())
{
    Console.WriteLine(builder.Configuration.GetValue<string>("ConnectionString"));

}
else
{
    builder.WebHost.UseUrls("http://0.0.0.0:" + Environment.GetEnvironmentVariable("PORT"));

}

// Add services to the container.
builder.Services.AddControllersWithViews();
// builder.Configuration.AddEnvironmentVariables();


// Config for Railway


// Connect MongoDb

var Appdb = builder.Configuration.GetSection("DiaryDatabase");

builder.Services.Configure<DiaryDatabaseSettings>(Appdb);
builder.Services.AddSingleton<IDiaryRepository, DiaryRepositoryService>();
builder.Services.AddSingleton<IGoogleCloudService, GoogleCloudService>();


// Identity
var ConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
var ConnectionString2 = builder.Configuration.GetValue<string>("ConnectionString");

builder.Services.AddIdentity<User, Role>(opts =>
    {
        opts.Password.RequiredLength = 8;
        opts.Password.RequiredUniqueChars = 1;
        opts.Password.RequireNonAlphanumeric = false;
    })
    .AddMongoDbStores<User, Role, ObjectId>(
       ConnectionString2,
        builder.Configuration.GetValue<string>("DiaryDatabase:DatabaseName"));


// Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

    options.LoginPath = "/Operations/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// Authorize Policies

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        policyBuilder => policyBuilder.RequireRole("Admin"));
    options.AddPolicy("User",
        policyBuilder => policyBuilder.RequireRole("User"));
    options.AddPolicy("RequireAuthenticated",
        policyBuilder => policyBuilder.RequireAuthenticatedUser());
    // options.AddPolicy("RequireAuthenticated",
    //     policyBuilder => policyBuilder.AddRequirements(new ));
});



// App
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
