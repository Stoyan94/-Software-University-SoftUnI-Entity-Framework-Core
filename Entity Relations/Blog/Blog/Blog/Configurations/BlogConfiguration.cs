using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace BlogDemo.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        //builder
        //.HasKey(b => b.BlogId);

        //builder
        //    .ToTable("Blogs", "blg");

        //builder
        //    .Property(b => b.Name)
        //    .HasColumnName("BlogName")
        //    .HasColumnType("NVARCHAR")
        //    .HasMaxLength(50)
        //.IsRequired();

        //builder
        //    .Property(b => b.Description)
        //    .HasColumnName("Description")
        //    .HasColumnType("NVARCHAR")
        //.HasMaxLength(500);

        builder
            .Property(b => b.Created)
            .ValueGeneratedOnAdd();

        builder
            .Property(b => b.LastUpdated)
            .ValueGeneratedOnAddOrUpdate();
       
    }
}

