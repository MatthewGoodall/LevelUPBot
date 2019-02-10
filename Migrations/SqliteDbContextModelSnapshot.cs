﻿// <auto-generated />
using LevelUP.Resources.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LevelUP.Migrations
{
    [DbContext(typeof(SqliteDbContext))]
    partial class SqliteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("LevelUP.Resources.Database.User", b =>
                {
                    b.Property<ulong>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Experience");

                    b.Property<int>("Level");

                    b.Property<int>("Stones");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
