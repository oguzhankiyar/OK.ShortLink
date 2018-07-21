﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OK.ShortLink.DataAccess.EntityFramework.DataContexts;

namespace OK.ShortLink.DataAccess.Migrations
{
    [DbContext(typeof(ShortLinkDataContext))]
    partial class ShortLinkDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OK.ShortLink.Common.Entities.LinkEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OriginalUrl")
                        .IsRequired();

                    b.Property<string>("ShortUrl")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("OK.ShortLink.Common.Entities.LogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Channel")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Data");

                    b.Property<string>("Exception");

                    b.Property<string>("IPAddress");

                    b.Property<string>("Level")
                        .IsRequired();

                    b.Property<string>("Message")
                        .IsRequired();

                    b.Property<string>("RequestId")
                        .IsRequired();

                    b.Property<string>("Source")
                        .IsRequired();

                    b.Property<int>("Thread");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserAgent");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("OK.ShortLink.Common.Entities.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OK.ShortLink.Common.Entities.VisitorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrowserInfo")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("DeviceInfo")
                        .IsRequired();

                    b.Property<string>("IPAddress")
                        .IsRequired();

                    b.Property<int>("LinkId");

                    b.Property<string>("OSInfo")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("UserAgent")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LinkId");

                    b.ToTable("Visitors");
                });

            modelBuilder.Entity("OK.ShortLink.Common.Entities.LinkEntity", b =>
                {
                    b.HasOne("OK.ShortLink.Common.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OK.ShortLink.Common.Entities.LogEntity", b =>
                {
                    b.HasOne("OK.ShortLink.Common.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("OK.ShortLink.Common.Entities.VisitorEntity", b =>
                {
                    b.HasOne("OK.ShortLink.Common.Entities.LinkEntity", "Link")
                        .WithMany()
                        .HasForeignKey("LinkId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}