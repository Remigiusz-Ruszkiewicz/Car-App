using Car_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Car_App.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Client> Clients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasKey(x => x.CarId);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Client>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = 1, Name = "Admin" },
                new Role() { Id = 2, Name = "User" }
                );
            modelBuilder.Entity<User>().HasData(
                new User() { Id = Guid.NewGuid(),Name = "Adam",Surname = "Kowalski",RoleId =1},
                new User() { Id = Guid.NewGuid(),Name = "Janusz",Surname = "Marczyk",RoleId =2}
                ) ;
            modelBuilder.Entity<Client>().HasData(
                new User() { Id = Guid.NewGuid(), Name = "Adam", Surname = "Kowalski", RoleId = 1 },
                new User() { Id = Guid.NewGuid(), Name = "Janusz", Surname = "Marczyk", RoleId = 2 }
                );
            modelBuilder.Entity<Car>().HasData(
                new User() { Id = Guid.NewGuid(), Name = "Adam", Surname = "Kowalski", RoleId = 1 },
                new User() { Id = Guid.NewGuid(), Name = "Janusz", Surname = "Marczyk", RoleId = 2 }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
