using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace AttendanceSystemDemo.Models
{
    public partial class AttendanceContext : DbContext

    {

        public DbSet<User> User { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Attendance> Attendance { get; set; }


        public AttendanceContext(DbContextOptions<AttendanceContext> options)
          : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Filename=./AttendanceSystemDb.db");
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.UserName).HasColumnName("UserName");
                entity.Property(e => e.Password).HasColumnName("Password");
                entity.Property(e => e.Role).HasColumnName("Role");


                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);




            });
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.StudentName).HasColumnName("StudentName");
                entity.Property(e => e.Gender).HasColumnName("Gender");
                entity.Property(e => e.Dob).HasColumnName("Dob");
                entity.Property(e => e.Grade).HasColumnName("Grade");


                entity.Property(e => e.StudentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grade)
                    .HasMaxLength(50)
                    .IsUnicode(false);


            });
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.TeacherName).HasColumnName("TeacherName");
                entity.Property(e => e.Gender).HasColumnName("Gender");
                entity.Property(e => e.Dob).HasColumnName("Dob");
                entity.Property(e => e.Subject).HasColumnName("Subject");

                entity.Property(e => e.TeacherName)
                    .HasMaxLength(50)
                    .IsUnicode(false);               

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)

                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .HasMaxLength(50)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.UserName).HasColumnName("UserName");
                entity.Property(e => e.Status).HasColumnName("Status");
                entity.Property(e => e.Remarks).HasColumnName("Remarks");

                entity.Property(e => e.Date).HasColumnType("DateTime");

                entity.Property(e => e.Role).HasColumnName("Role");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Role)
                   .HasMaxLength(50)
                   .IsUnicode(false);


            });


        


        }






    }

}
