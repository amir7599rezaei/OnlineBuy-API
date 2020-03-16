using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBuy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Data.ConfigModels
{
    public class CustomerOrderConfig : IEntityTypeConfiguration<CustomerOrder>
    {
        public void Configure(EntityTypeBuilder<CustomerOrder> builder)
        {            
            builder.Property(c => c.StatusText).HasMaxLength(50);
        }
    }
}
