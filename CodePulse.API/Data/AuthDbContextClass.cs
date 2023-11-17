using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContextClass : IdentityDbContext
    {
        public AuthDbContextClass(DbContextOptions options) : base(options)
        {    
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "a0a2f32a-ecd4-4650-af30-676943f9f497";
            var writerRoleId = "bc622f56-00d5-418a-9d03-1c9d3d8efcf2";

            //Create reader and writer Role
            var roles = new List<IdentityRole>
            { 

                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name  = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole() 
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            //Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);


            //Create an Admin user
            var adminUserId = "2a5bdd2a-147d-4e9f-99ae-195ee6e8a9fb";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin",
                Email = "admin@admin.com",
                NormalizedEmail = "admin@admin.com".ToUpper(),
                NormalizedUserName = "admin@admin.com".ToUpper(),
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "admin123");

            builder.Entity<IdentityUser>().HasData(admin);

            //Give roles to admin
            var adminRoles = new List<IdentityUserRole<string>>()
            { 
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new() 
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
