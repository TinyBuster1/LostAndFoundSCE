namespace Mr_MissSeeks.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Tables : DbContext
    {
        public Tables()
            : base("name=Tables")
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Item>().ToTable("Item");
        }
    }
}