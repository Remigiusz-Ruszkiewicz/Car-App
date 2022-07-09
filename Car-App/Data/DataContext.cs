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
            Guid user1 = Guid.Parse("5f2d4ef2-3c0e-46a4-ade8-748c91153b8c");
            Guid user2 = Guid.Parse("7544783e-0339-4d39-be3f-d1c9c6217f21");
            Guid client1 = Guid.Parse("5f2d4ef2-3c0e-46a4-ade8-748c91323b8c");
            Guid client2 = Guid.Parse("7544783e-0339-4d39-be3f-d1c9c1317f21");
            Guid roleAdmin = Guid.Parse("b4bcc107-4cf5-493f-8810-48349ab05828");
            Guid roleUser = Guid.Parse("f63a2990-440a-4dc5-b5fb-c3ca93ad6e39");
            modelBuilder.Entity<Car>().HasKey(x => x.CarId);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Client>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().HasData(
                new Role() { Id = roleAdmin, Name = "Admin" },
                new Role() { Id = roleUser, Name = "User" }
                );
            modelBuilder.Entity<User>().HasData(
                new User() { Id = user1, Login = "admin",Password = "KRPB7a0uN9UmAuGj8s7ChQ==", RoleId = roleAdmin },
                new User() { Id = user2, Login = "janusz",Password = "KRPB7a0uN9UmAuGj8s7ChQ==", RoleId = roleUser }
                ) ;
            modelBuilder.Entity<Client>().HasData(
                new Client() { Id = Guid.NewGuid(), Name = "Jan", Surname = "Marek"},
                new Client() { Id = Guid.NewGuid(), Name = "Arkadiusz", Surname = "Kwiatkowski"}
                );
            modelBuilder.Entity<Car>().HasData(
                new Car() { CarId = Guid.NewGuid(), RegistrationNumber = "KMYSH70", Model = "A6C5", Brand = "Audi",VIN="12345678900987654",UserId= user1,ClientId = client1 },
                new Car() { CarId = Guid.NewGuid(), RegistrationNumber = "KMYSH72", Model = "A6C6", Brand = "Audi",VIN="12345678900987612",UserId= user1 ,ClientId = client2 },
                new Car() { CarId = Guid.NewGuid(), RegistrationNumber = "KR45550", Model = "Panamera", Brand = "Porshe", VIN = "09876543211234567", UserId = user2 }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
