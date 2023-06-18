﻿// <auto-generated />
using DropboxLike.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DropboxLike.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230606133528_AddNewDb")]
    partial class AddNewDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FileModel", b =>
                {
                    b.Property<string>("FileKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeStamp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FileKey");

                    b.ToTable("FileModels");
                });
#pragma warning restore 612, 618
        }
    }
}