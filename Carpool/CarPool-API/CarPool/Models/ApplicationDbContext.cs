using CarPool.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, UserRoles, Guid>
    {

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<RideRequest> RideRequests { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<UserTrip> UserTrips { get; set; }

    

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<RideRequest>()
            .HasOne(s=>s.Trip)
            .WithMany(i=>i.Requests)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<RideRequest>()
            .HasOne(s => s.User)
            .WithMany(i=>i.UserRequests)
            .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<UserTrip>()
           .HasOne(s => s.Trip)
           .WithMany(i=>i.Riders)
           .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<UserTrip>()
            .HasOne(s => s.User)
            .WithMany(i=>i.UserTrips)
            .OnDelete(DeleteBehavior.Cascade);

            

            // Override default AspNet Identity table names
            builder.Entity<User>(entity => { entity.ToTable(name: "Users"); });
            builder.Entity<UserRoles>(entity => { entity.ToTable(name: "Roles"); });
            builder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("UserRoles"); });
            builder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("UserClaims"); });
            builder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("UserLogins"); });
            builder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("UserTokens"); });
            builder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("UserRoleClaims"); });

        }

    }
}
