﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestProject2.Models;

namespace TestProject2;

public class TestsBase
{
    protected IDbContextFactory<MyDbContext> _dbFactory;
    protected IServiceProvider _serviceProvider;
    protected IServiceScope _serviceScope;

    [SetUp]
    public void Setup()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                // Konfiguration aus appsettings.json laden
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                // Registrierung der DbContextFactory
                services.AddDbContextFactory<MyDbContext>(options =>
                {
                    options.LogTo(_ => { }, LogLevel.None);
                    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"));
                });
            })
            .Build();

        _serviceScope = host.Services.CreateScope();
        _serviceProvider = _serviceScope.ServiceProvider;
        _dbFactory = _serviceProvider.GetRequiredService<IDbContextFactory<MyDbContext>>();

        var context = _dbFactory.CreateDbContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    [TearDown]
    public void TearDown()
    {
        _serviceScope.Dispose();
    }
}