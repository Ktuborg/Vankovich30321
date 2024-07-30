﻿using GR30321.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GR30321.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //для лб 7

        //public AppDbContext()
        //{ }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlite("");
        //}

        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}