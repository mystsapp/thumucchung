namespace thumucchung.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class qlthumucDbContext : DbContext
    {
        public qlthumucDbContext()
            : base("name=qlthumucDbContext")
        {
        }

        public virtual DbSet<dmchinhanh> dmchinhanhs { get; set; }
        public virtual DbSet<dmphong> dmphongs { get; set; }
        public virtual DbSet<phanquyen> phanquyens { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<dmchinhanh>()
                .Property(e => e.chinhanh)
                .IsUnicode(false);

            modelBuilder.Entity<dmphong>()
                .Property(e => e.phongban)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.phongban)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.chinhanh)
                .IsUnicode(false);
        }
    }
}
