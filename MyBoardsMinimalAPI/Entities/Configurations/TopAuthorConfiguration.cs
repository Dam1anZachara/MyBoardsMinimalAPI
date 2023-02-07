using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBoardsMinimalAPI.Entities.ViewModels;
using System.Reflection.Emit;

namespace MyBoardsMinimalAPI.Entities.Configurations
{
    public class TopAuthorConfiguration : IEntityTypeConfiguration<TopAuthor>
    {
        public void Configure(EntityTypeBuilder<TopAuthor> eb)
        {
            eb.ToView("View_TopAuthors");
            eb.HasNoKey();
        }
    }
}
