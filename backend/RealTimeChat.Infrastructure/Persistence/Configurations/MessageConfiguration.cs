using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeChat.Domain.Entities;

namespace RealTimeChat.Infrastructure.Persistence.Configurations;

internal class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.UserId).IsRequired();

        builder.Property(m => m.Text).IsRequired();

        builder.Property(m => m.SentimentType)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(m => m.Created).IsRequired();
        
        builder.HasOne(m => m.User)
               .WithMany(u => u.Messages)
               .HasForeignKey(m => m.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
