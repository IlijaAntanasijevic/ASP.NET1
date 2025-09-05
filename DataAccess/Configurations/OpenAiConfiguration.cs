using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }
    }

}
