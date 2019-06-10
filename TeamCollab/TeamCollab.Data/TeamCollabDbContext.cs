using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamCollab.Data.Models;

namespace TeamCollab.Data
{
    public class TeamCollabDbContext : IdentityDbContext<User>
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Board> Boards { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<EventLog> Logs { get; set; }

        public TeamCollabDbContext(DbContextOptions<TeamCollabDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Projects)
                .WithOne(up => up.User)
                .HasForeignKey(up => up.UserId);

            modelBuilder
                .Entity<UserProject>()
                .HasOne(us => us.Project)
                .WithMany(p => p.Workers)
                .HasForeignKey(up => up.ProjectId);

            modelBuilder
                .Entity<UserProject>()
                .HasKey(up => new {up.UserId, up.ProjectId});

            modelBuilder
                .Entity<Project>()
                .HasOne(p => p.Manager)
                .WithMany()
                .HasForeignKey(p => p.ManagerId);

            modelBuilder
                .Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.UserId);

            modelBuilder
                .Entity<Project>()
                .HasMany(p => p.Messages)
                .WithOne(m => m.Project)
                .HasForeignKey(m => m.ProjectId);

            modelBuilder
                .Entity<Board>()
                .HasMany(b => b.Cards)
                .WithOne(c => c.Board)
                .HasForeignKey(c => c.BoardId);

            modelBuilder
                .Entity<Board>()
                .HasOne(b => b.Project)
                .WithMany()
                .HasForeignKey(b => b.ProjectId);

            modelBuilder
                .Entity<Board>()
                .HasOne(b => b.Root)
                .WithOne()
                .HasForeignKey<Board>(b => b.RootCardId);

            modelBuilder
                .Entity<Card>()
                .HasOne(c => c.Next)
                .WithOne()
                .HasForeignKey<Card>(c => c.NextCardId);

            modelBuilder
                .Entity<Card>()
                .HasOne(c => c.Prev)
                .WithOne()
                .HasForeignKey<Card>(c => c.PrevCardId);

            modelBuilder
                .Entity<Card>()
                .HasOne(c => c.LastModifiedBy)
                .WithMany()
                .HasForeignKey(c => c.LastModifiedById);

            modelBuilder
                .Entity<EventLog>()
                .HasOne(l => l.User)
                .WithMany()
                .HasForeignKey(l => l.UserId);

            modelBuilder
                .Entity<EventLog>()
                .HasOne(l => l.Project)
                .WithMany(p => p.History)
                .HasForeignKey(l => l.ProjectId);
        }
    }
}
