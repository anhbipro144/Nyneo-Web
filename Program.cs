using MongoDB.Bson;
using Nyneo_Web.DataAccess;
using Nyneo_Web.Models;
using Nyneo_Web.Services;
using Nyneo_Web.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.WebHost.UseUrls("http://0.0.0.0:" + Environment.GetEnvironmentVariable("PORT"));


builder.Services.Configure<DiaryDatabaseSettings>(
    builder.Configuration.GetSection("DiaryDatabase"));

builder.Services.AddSingleton<IDiaryRepository, DiaryRepositoryService>();
builder.Services.AddSingleton<IGoogleCloudService, GoogleCloudService>();



var myVar = builder.Configuration.GetValue<string>("DiaryDatabase:ConnectionString");

builder.Services.AddIdentity<User, Role>(opts =>
    {
        opts.Password.RequiredLength = 8;
        opts.Password.RequiredUniqueChars = 1;
        opts.Password.RequireNonAlphanumeric = false;
    })
    .AddMongoDbStores<User, Role, ObjectId>(
      myVar,
        builder.Configuration.GetValue<string>("DiaryDatabase:DatabaseName"));

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
