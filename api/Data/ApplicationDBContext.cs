using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions) 
        { 

        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }


        // Defining relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Each Event can have multiple bookings (one to many)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventId);

            // // Each User can have multiple bookings
            // modelBuilder.Entity<Booking>()
            //     .HasOne(b => b.User)
            //     .WithMany(u => u.Bookings)
            //     .HasForeignKey(b => b.UserId);

            // Each EventCategory can have multiple Events
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}