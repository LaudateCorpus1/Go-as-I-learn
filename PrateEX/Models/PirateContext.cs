namespace PrateEX.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PirateContext : DbContext
    {
        public PirateContext()
            : base("name=PirateContext")
        {
        }

        public virtual DbSet<Crew> Crews { get; set; }
        public virtual DbSet<Pirate> Pirates { get; set; }
        public virtual DbSet<Ship> Ships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crew>()
                .Property(e => e.Booty)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Pirate>()
                .HasMany(e => e.Crews)
                .WithRequired(e => e.Pirate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ship>()
                .HasMany(e => e.Crews)
                .WithRequired(e => e.Ship)
                .WillCascadeOnDelete(false);
        }
    }
}
