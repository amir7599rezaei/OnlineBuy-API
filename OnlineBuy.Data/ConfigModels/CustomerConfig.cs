using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBuy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Data.ConfigModels
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(20);
            builder.Property(c => c.Family).HasMaxLength(40);
            builder.Property(c => c.Mobile).HasMaxLength(15);
            builder.Property(c => c.Tel).HasMaxLength(15);
            builder.Property(c => c.UserName).HasMaxLength(50);
            builder.Property(c => c.Email).HasMaxLength(50);
            builder.Property(c => c.PostalCode).HasMaxLength(15);
        }
    }
}
