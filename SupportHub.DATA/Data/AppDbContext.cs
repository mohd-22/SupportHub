using Microsoft.EntityFrameworkCore;
using SupportHub.CORE.Models;

namespace SupportHub.DATA.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductModule> ProductModules { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Attachment> Attachments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Ticket>(b =>
        {
            b.HasOne(t => t.Client)
             .WithMany(u => u.TicketsCreated)
             .HasForeignKey(t => t.ClientId)
                  .OnDelete(DeleteBehavior.Restrict);


            b.HasOne(t => t.AssignedUser)
             .WithMany(u => u.TicketsAssigned)
             .HasForeignKey(t => t.AssignedTo)
                  .OnDelete(DeleteBehavior.Restrict);


        });

        modelBuilder.Entity<Comment>(b =>
        {
            b.Property(c => c.CommentText).IsRequired();
            b.HasOne(c => c.Creator)
             .WithMany(u => u.Comments)
             .HasForeignKey(c => c.CreatedBy);
        });
    }
}
