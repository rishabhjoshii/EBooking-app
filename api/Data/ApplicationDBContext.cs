using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        { 

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }


        // Defining relationships
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // User configuration
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserName).HasColumnName("username").IsRequired();
                entity.Property(e => e.Email).HasColumnName("email").IsRequired();
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number").IsRequired();
                entity.Property(e => e.PasswordHash).HasColumnName("password").IsRequired();
            });

            // // Event -> User relation (Event created by User)
            // builder.Entity<Event>()
            //     .HasOne(e => e.ApplicationUser)  // Event created by a User
            //     .WithMany()  // Do not define inverse navigation property
            //     .HasForeignKey(e => e.ApplicationUserId)
            //     .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete

            // // Booking -> User relation (User makes a Booking)
            // builder.Entity<Booking>()
            //     .HasOne(b => b.ApplicationUser)  // Booking made by a User
            //     .WithMany()  // Do not define inverse navigation property
            //     .HasForeignKey(b => b.ApplicationUserId)
            //     .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete

            // // Booking -> Event relation
            // builder.Entity<Booking>()
            //     .HasOne(b => b.Event)  // Booking related to an Event
            //     .WithMany(e => e.Bookings)  // Event can have multiple bookings
            //     .HasForeignKey(b => b.EventId)
            //     .OnDelete(DeleteBehavior.Cascade);  // Optional, can be Cascade or Restrict

            // // Event -> EventCategory relation
            // builder.Entity<Event>()
            //     .HasOne(e => e.Category)  // Event belongs to a Category
            //     .WithMany(c => c.Events)  // Category can have multiple events
            //     .HasForeignKey(e => e.CategoryId)
            //     .OnDelete(DeleteBehavior.Restrict);  // Prevent cascade delete

            // // Seed initial roles for authorization
            // List<IdentityRole> roles = new List<IdentityRole>{
            //     new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            //     new IdentityRole { Name = "User", NormalizedName = "USER" }
            // };
            // builder.Entity<IdentityRole>().HasData(roles);
            
        }

    }
}
 