using ConsoleApp;
using DAL;
using DAL.Database;
using Microsoft.EntityFrameworkCore;

// Game starts here

var connectionString = $"Data Source={FileHelper.BasePath}app.db";

var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
    .UseSqlite(connectionString)
    .EnableDetailedErrors()
    .EnableSensitiveDataLogging()
    .Options;

using var context = new AppDbContext(contextOptions);

var savedGamesCount = context.DbSaveGame.Count();

Console.WriteLine($"Games in db {savedGamesCount}");

var menu = new Menus();
menu.MainMenu.Run();





