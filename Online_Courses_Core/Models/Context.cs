using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Courses_Core.Models
{
    public class Context : IdentityDbContext<User, ApplicationRole, string>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=Online_Courses_Core;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCourse>()
                .HasKey(o => new { o.UserID, o.CourseID });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseFile> CourseFiles { get; set; }
        public DbSet<CompanyContactInfo> CompanyContactInfos { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageStatus> MessageStatuses { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<CourseLanguage> CourseLanguages { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }


    }
}
