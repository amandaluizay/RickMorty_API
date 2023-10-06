

using Interview_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseStore.Data.Mappings
{
    public class LogMapping : IEntityTypeConfiguration<LogModel>
    {
        public void Configure(EntityTypeBuilder<LogModel> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.FilePath)
                .HasColumnType("varchar(100)");

            builder.Property(c => c.CreatedTimeStamp)
                .HasColumnType("DATETIME");

            builder.ToTable("Log");
        }
    }
}