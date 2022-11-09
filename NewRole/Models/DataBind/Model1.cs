using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace NewRole.Models.DataBind
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Depertment> Depertments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<multiProduct> multiProducts { get; set; }
        public virtual DbSet<supplier> suppliers { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }
        public virtual DbSet<TblUserRole> TblUserRoles { get; set; }
        public virtual DbSet<Traineertblvm> Traineertblvms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Depertment>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Depertment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Designation>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Designation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<supplier>()
                .Property(e => e.mobile)
                .HasPrecision(18, 0);

            modelBuilder.Entity<supplier>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<TblUser>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<TblUser>()
                .Property(e => e.UserPass)
                .IsUnicode(false);

            modelBuilder.Entity<TblUser>()
                .Property(e => e.UserType)
                .IsUnicode(false);

            modelBuilder.Entity<TblUserRole>()
                .Property(e => e.PageName)
                .IsUnicode(false);

            modelBuilder.Entity<Traineertblvm>()
                .Property(e => e.TraineerName)
                .IsUnicode(false);

            modelBuilder.Entity<Traineertblvm>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Traineertblvm>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Traineertblvm>()
                .Property(e => e.Contact)
                .IsUnicode(false);
        }
    }
}
