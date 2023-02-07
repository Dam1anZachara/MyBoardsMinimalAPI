using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MyBoardsMinimalAPI.Entities.Configurations
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> eb)
        {
            //eb.Property(wi => wi.State).IsRequired();
            eb.Property(wi => wi.Area).HasColumnType("varchar(200)");
            eb.Property(wi => wi.IterationPath).HasColumnName("Iteration_Path");
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
            // relation one State has many WorkItems
            eb.HasOne(s => s.State)
            .WithMany(w => w.WorkItems)
            .HasForeignKey(w => w.StateId);
        }
    }
}
