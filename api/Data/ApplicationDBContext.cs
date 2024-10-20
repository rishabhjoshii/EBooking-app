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
        public DbSet<Image> Images { get; set; }
        public DbSet<UserProfileImage> UserProfileImages { get; set; }

        public DbSet<TicketType> TicketTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Add Event -> Category relationship
            builder.Entity<Event>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            //Event -> ApplicationUser relationship
            builder.Entity<Event>()
                .HasOne(e => e.ApplicationUser)
                .WithMany()
                .HasForeignKey(e => e.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // booking -> Event relationship
            builder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            // booking -> ApplicationUser relationship
            builder.Entity<Booking>()
                .HasOne(b => b.ApplicationUser)
                .WithMany()
                .HasForeignKey(b => b.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Event -> Image relationship (One-to-Many)
            builder.Entity<Image>()
                .HasOne(img => img.Event)
                .WithMany(e => e.Images) // An event can have many images
                .HasForeignKey(img => img.EventId) // Foreign key
                .OnDelete(DeleteBehavior.Cascade); // Images should be deleted when the event is deleted

            // Event -> TicketType relationship (One-to-Many)
            builder.Entity<Event>()
                .HasMany(e => e.TicketTypes)
                .WithOne(tt => tt.Event)
                .HasForeignKey(tt => tt.EventId)
                .OnDelete(DeleteBehavior.Cascade); // Ensures that TicketTypes are deleted when the event is deleted

        }
    }
}

 