﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Reception.Server.File.Model;

namespace Reception.Server.File.Repository
{
    public class FileContext : DbContext
    {
        public FileContext()
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<FileData> FileDatas { get; set; }
        public DbSet<FileVersion> FileVersions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = $"{nameof(Reception)}.{nameof(Server)}.{nameof(File)}.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder
                .UseLoggerFactory(Startup.ReceptionLoggerFactory)
                .UseSqlite(connection);
        }
    }
}