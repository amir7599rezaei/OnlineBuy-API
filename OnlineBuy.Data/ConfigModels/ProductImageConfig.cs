using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBuy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Data.ConfigModels
{
    public class ProductImageConfig : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(p => p.FormatContent).HasMaxLength(20);
        }
    }
}
