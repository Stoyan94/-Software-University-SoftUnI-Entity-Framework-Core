﻿// <auto-generated />
using System;
using BlogTraining.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlogTraining.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20241020122537_Tags_Added")]
    partial class Tags_Added
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BlogTraining.Data.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"), 1L, 1);

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("BlogId")
                        .HasColumnType("int");

                    b.HasKey("AuthorId");

                    b.HasIndex("BlogId");

                    b.ToTable("Authors", "blg");
                });

            modelBuilder.Entity("BlogTraining.Data.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogId"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("NVARCHAR(500)");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("BlogName");

                    b.HasKey("BlogId");

                    b.HasIndex("AuthorId");

                    b.HasIndex(new[] { "Name" }, "IX_Blogs_Name_Unique")
                        .IsUnique();

                    b.ToTable("Blogs", "blg");
                });

            modelBuilder.Entity("BlogTraining.Data.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"), 1L, 1);

                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts", "blg");
                });

            modelBuilder.Entity("BlogTraining.Data.Models.PostTag", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTags", "blg");
                });

            modelBuilder.Entity("BlogTraining.Data.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"), 1L, 1);

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("TagId");

                    b.ToTable("Tags", "blg");
                });

            modelBuilder.Entity("BlogTraining.Data.Models.Author", b =>
                {
                    b.HasOne("BlogTraining.Data.Models.Blog", "Blog")
                        .WithMany()
                        .HasForeignKey("BlogId");

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("BlogTraining.Data.Models.Blog", b =>
                {
                    b.HasOne("BlogTraining.Data.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("BlogTraining.Data.Models.Post", b =>
                {
                    b.HasOne("BlogTraining.Data.Models.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("BlogTraining.Data.Models.PostTag", b =>
                {
                    b.HasOne("BlogTraining.Data.Models.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogTraining.Data.Models.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("BlogTraining.Data.Models.Blog", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("BlogTraining.Data.Models.Post", b =>
                {
                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("BlogTraining.Data.Models.Tag", b =>
                {
                    b.Navigation("PostTags");
                });
#pragma warning restore 612, 618
        }
    }
}
