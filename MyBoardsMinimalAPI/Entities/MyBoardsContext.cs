﻿using Microsoft.EntityFrameworkCore;

namespace MyBoardsMinimalAPI.Entities
{
    public class MyBoardsContext : DbContext
    {
        public MyBoardsContext(DbContextOptions<MyBoardsContext> options) : base(options)
        {

        }
        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkItem>(eb =>
            {
                eb.Property(wi => wi.State).IsRequired();
                eb.Property(wi => wi.Area).HasColumnType("varchar(200)");
                eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
                eb.Property(wi => wi.Efford).HasColumnType("decimal(5,2)");
                eb.Property(wi => wi.EndDate).HasPrecision(3);
                eb.Property(wi => wi.Activity).HasMaxLength(200);
                eb.Property(wi => wi.RemainingWork).HasPrecision(14, 2);
                eb.Property(wi => wi.Priority).HasDefaultValue(1);
                // relation one to many !Can be define start from Comment
                eb.HasMany(w => w.Comments)
                .WithOne(c => c.WorkItem)
                .HasForeignKey(c => c.WorkItemId);
                // relation one to many !One User has many WorkItems
                eb.HasOne(w => w.Author)
                .WithMany(u => u.WorkItems)
                .HasForeignKey(a => a.AuthorId);

                // relation many to many after .Net 5
                eb.HasMany(w => w.Tags)
                .WithMany(t => t.WorkItems)
                // Jeśli chcemy wykorzystać tabelę pośredniczącą z dodatkowymi property np data połączenia encji
                .UsingEntity<WorkItemTag>(
                    w => w.HasOne(wit => wit.Tag)
                    .WithMany()
                    .HasForeignKey(wit => wit.TagId),

                    w => w.HasOne(wit => wit.WorkItem)
                    .WithMany()
                    .HasForeignKey(wit => wit.WorkItemId),

                    wit =>
                    {
                        wit.HasKey(x => new { x.TagId, x.WorkItemId });
                        wit.Property(x => x.PublicationDate).HasDefaultValueSql("getutcdate()");
                    }
                    );
            });

            modelBuilder.Entity<Comment>(eb =>
            {
                eb.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
                eb.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate(); 
            });

            //config relation 1 to 1 (User - Address)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);
            //
            //config relation many to many before .Net 5
            //modelBuilder.Entity<WorkItemTag>()
            //    .HasKey(c => new { c.TagId, c.WorkItemId });

            modelBuilder.Entity<State>(eb =>
            {
                eb.Property(x => x.Name).IsRequired();
                eb.Property(x => x.Name).HasMaxLength(50);

                //relation one State has many WorkItems
                eb.HasMany(w => w.WorkItems)
                .WithOne(s => s.State)
                .HasForeignKey(s => s.StateId);
            });
        }
    }
}