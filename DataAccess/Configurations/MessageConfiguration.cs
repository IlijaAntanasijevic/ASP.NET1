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
    public class MessageConfiguration : IEntityTypeConfiguration<ChatMessages>
    {
        public void Configure(EntityTypeBuilder<ChatMessages> builder)
        {

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Sender)
                      .WithMany()
                      .HasForeignKey(x => x.SenderId)
                      .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Receiver)
                      .WithMany()
                      .HasForeignKey(x => x.ReceiverId)
                      .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
