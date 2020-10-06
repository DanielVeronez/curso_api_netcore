using System;
using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);

            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity{
                    Id = Guid.NewGuid(),
                    Name = "Administrador",
                    Email = "testeadm@teste.com",
                    CreatAt = DateTime.MinValue,
                    UpdateAt = DateTime.MinValue,
                    Password = "698dc19d489c4e4db73e28a713eab07b"
                }
            );
        }
    }
}
