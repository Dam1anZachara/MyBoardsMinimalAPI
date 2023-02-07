using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MyBoardsMinimalAPI.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //config relation 1 to 1 (User - Address)
            builder.HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<Address>(a => a.UserId);
            //
            //config relation many to many before .Net 5
            //modelBuilder.Entity<WorkItemTag>()
            //    .HasKey(c => new { c.TagId, c.WorkItemId });
        }
    }
}
