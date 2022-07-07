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
            Guid user1 = Guid.NewGuid();
            Guid user2 = Guid.NewGuid();
            Guid role1 = Guid.NewGuid();
            Guid role2 = Guid.NewGuid();
            modelBuilder.Entity<Car>().HasKey(x => x.CarId);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Client>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = role1, Name = "Admin" },
                new Role() { Id = role2, Name = "User" }
                );
            modelBuilder.Entity<User>().HasData(
                new User() { Id = user1, Name = "Adam",Surname = "Kowalski",RoleId = role1 },
                new User() { Id = user2, Name = "Janusz",Surname = "Marczyk",RoleId = role2 }
                ) ;
            modelBuilder.Entity<Client>().HasData(
                new Client() { Id = Guid.NewGuid(), Name = "Jan", Surname = "Marek"},
                new Client() { Id = Guid.NewGuid(), Name = "Arkadiusz", Surname = "Kwiatkowski"}
                );
            modelBuilder.Entity<Car>().HasData(
                new Car() { CarId = Guid.NewGuid(), RegistrationNumber = "KMYSH70", Model = "A6C5", Brand = "Audi",VIN="12345678900987654",UserId= user1 },
                new Car() { CarId = Guid.NewGuid(), RegistrationNumber = "KMYSH72", Model = "A6C6", Brand = "Audi",VIN="12345678900987612",UserId= user1 },
                new Car() { CarId = Guid.NewGuid(), RegistrationNumber = "KR45550", Model = "Panamera", Brand = "Porshe", VIN = "09876543211234567", UserId = user2 }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
