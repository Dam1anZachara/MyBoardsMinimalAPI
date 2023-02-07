using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MyBoardsMinimalAPI.Entities.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> eb)
        {
            eb.Property(x => x.CreatedDate).HasDefaultValueSql("getutcdate()");
            eb.Property(x => x.UpdatedDate).ValueGeneratedOnUpdate();

            //rel One to many (User - Comments)
            eb.HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
