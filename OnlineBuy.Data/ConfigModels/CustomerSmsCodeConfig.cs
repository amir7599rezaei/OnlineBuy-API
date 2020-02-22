using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBuy.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBuy.Data.ConfigModels
{
    public class CustomerSmsCodeConfig : IEntityTypeConfiguration<CustomerSmsCode>
    {
        public void Configure(EntityTypeBuilder<CustomerSmsCode> builder)
        {
        }
    }
}
