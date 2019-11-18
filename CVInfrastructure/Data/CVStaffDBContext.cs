using CVCore.Entities;
using CVCore.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CVInfrastructure.Data
{
    public class CVStaffDBContext:IdentityDbContext<ApplicationUser>,IRepository
    {
        public CVStaffDBContext(DbContextOptions<CVStaffDBContext> options)
        : base(options)
        {
            
        }

        public DbSet<SPNation> SPNations { get; set; }
        public DbSet<SPEducation> SPEducations { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        public Task<int> SaveChangesAsync()
        {
             return base.SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staffs");
                entity.Property(b => b.Active)
                    .HasDefaultValue(true);

                entity.Property(e => e.BirthDate)
                    .HasColumnName("BirthDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasColumnName("Email")
                    .HasMaxLength(150);               

                entity.Property(e => e.Phone)
                    .HasColumnName("Phone")
                    .HasMaxLength(150);                
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("tbJob");

                entity.HasOne(d => d.Staff)
                   .WithMany(p => p.Jobs)
                   .HasForeignKey(d => d.StaffId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_tbJob_StaffId");
            });
            modelBuilder.Entity<Education>(entity =>
            {
                entity.ToTable("tbEducation");

                entity.HasOne(d => d.Staff)
                   .WithMany(p => p.Educations)
                   .HasForeignKey(d => d.StaffId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_tbEducations_StaffId");
            });
        }
    }
}
