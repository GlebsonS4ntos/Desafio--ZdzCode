using Desafio.BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace Desafio.BackEnd.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Panelist> Panelists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Panelist)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.PanelistId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
