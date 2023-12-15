using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using wleepay.Core.Models;

namespace wleepay.Data.Configurations
{
    public class SecurityQuestionsConfiguration: IEntityTypeConfiguration<SecurityQuestions>
    {
        public void Configure(EntityTypeBuilder<SecurityQuestions> builder)
        {
            builder.HasData(new SecurityQuestions
            {
                Id = 1,
                Question= "What is your mother’s maiden name"

            });

            builder.HasData(new SecurityQuestions
            {
                Id = 2,
                Question = "Name of primary school attended"

            });

            builder.HasData(new SecurityQuestions
            {
                Id = 3,
                Question = "Who is your favorite musician"

            });
        }
    }
}
