using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBuy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Data.ConfigModels
{
    public class LogReportConfig : IEntityTypeConfiguration<LogReport>
    {
        public void Configure(EntityTypeBuilder<LogReport> builder)
        {
            builder.Property(l => l.Properties).HasColumnType("XML");
            builder.Property(l => l.Level).HasMaxLength(128);
            builder.Property(l => l.TimeStamp).HasMaxLength(7).IsRequired();
        }
    }
}
