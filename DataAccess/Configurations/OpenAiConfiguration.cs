using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class OpenAiConfiguration : IEntityTypeConfiguration<OpenAiConversation>
    {
        public void Configure(EntityTypeBuilder<OpenAiConversation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(150);

        }
    }

    public class OpenAiMessagesConfiguration : IEntityTypeConfiguration<OpenAiMessages>
    {
        public void Configure(EntityTypeBuilder<OpenAiMessages> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Conversation)
                    .WithMany(x => x.Messages)
                    .HasForeignKey(x => x.ConversationId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.User)
             .WithMany(x => x.OpenAiMessages)
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class OpenAiSetupConfiguration : EntityConfiguration<OpenAiSetup>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<OpenAiSetup> builder)
        {

            builder.HasKey(e => e.Id);

            builder.HasMany(s => s.Conversations)
                      .WithOne(c => c.Setup)
                      .HasForeignKey(c => c.SetupId)
                      .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Model).HasColumnType("nvarchar(100)");
            builder.Property(e => e.DefaultPromt).HasColumnType("nvarchar(max)");

        }
    }


}
