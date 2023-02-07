using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MyBoardsMinimalAPI.Entities.Configurations
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            //seed data
            builder.HasData(new State() { Id = 1, Name = "To Do" },
            new State() { Id = 2, Name = "Doing" },
            new State() { Id = 3, Name = "Done" });

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(60);

            //relation one State has many WorkItems
            //eb.HasMany(w => w.WorkItems)
            //.WithOne(s => s.State)
            //.HasForeignKey(s => s.StateId);
        }
    }
}
