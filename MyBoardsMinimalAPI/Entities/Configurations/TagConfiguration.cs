using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MyBoardsMinimalAPI.Entities.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            //Seed data
            builder.HasData(new Tag() { Id = 1, Value = "Web" },
                new Tag() { Id = 2, Value = "UI" },
                new Tag() { Id = 3, Value = "Desktop" });
        }
    }
}
