using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBuy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Data.ConfigModels
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50);
        }
    }
}
