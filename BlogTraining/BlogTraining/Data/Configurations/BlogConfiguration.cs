using BlogTraining.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace BlogTraining.Data.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {             
           builder
                .Property(b => b.Created)
                .ValueGeneratedOnAdd();

            builder
                .Property(b => b.LastUpdated)
                .ValueGeneratedOnUpdate();
        }
    }
}
