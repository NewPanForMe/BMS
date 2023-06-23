﻿using BMS_Models.DbModels;

namespace BMS_Db.EfContext;
using Microsoft.EntityFrameworkCore;
public class BmsV1DbContext : DbContext
{
    public DbSet<User> User { get; set; }

    public BmsV1DbContext(DbContextOptions<BmsV1DbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}