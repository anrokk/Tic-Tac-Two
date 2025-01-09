using Microsoft.EntityFrameworkCore;
using DAL.Database;
using DAL;
using Domain;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

//"db" for database access, leave empty for JSON access
InitializeDal("db");

SetupServices();

var app = builder.Build();

SetupAndRunApp();

return;



void InitializeDal(string? dbOrJson = null)
{
    if (dbOrJson == "db")
    {
        var databaseInitializer = new DatabaseInitializer();
        databaseInitializer.Initialize();
        builder.Services.AddScoped<IConfigRepository, ConfigRepositoryDb>();
        builder.Services.AddScoped<IGameRepository, GameRepositoryDb>();
        builder.Services.AddSingleton(databaseInitializer);
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(databaseInitializer.GetConnectionString()));
    }
    else
    {
        builder.Services.AddScoped<IConfigRepository, ConfigRepositoryJson>();
        builder.Services.AddScoped<IGameRepository, GameRepositoryJson>();    
    }
}

void SetupServices()
{
    builder.Services.AddScoped<GameService>();
    builder.Services.AddScoped<GameContext>();


    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    builder.Services.AddRazorPages();

    builder.Services.AddDistributedMemoryCache(); // Required for session state
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust timeout as needed
        options.Cookie.HttpOnly = true; // Prevent client-side access to session cookie
        options.Cookie.IsEssential = true; // Ensure the cookie is created even without consent
    });
}

void SetupAndRunApp()
{
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.MapStaticAssets();

    app.UseSession();
    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages().WithStaticAssets();

    app.Run();
}
