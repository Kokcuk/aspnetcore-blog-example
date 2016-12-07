using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogExampleStatic.Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogExampleStatic.Web.Data
{
    public class BlogExampleDbContext: IdentityDbContext<ApplicationUserEntity, ApplicationRoleEntity, long>
    {
        public BlogExampleDbContext(DbContextOptions<BlogExampleDbContext> options):base(options)
        {
            
        }

        public DbSet<PostEntity> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PostEntity>().ToTable("Posts");

            base.OnModelCreating(builder);
        }
    }
}
