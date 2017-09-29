﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Myapi.SqlContext;
using System;

namespace Myapi.Migrations
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20170929081647_Application")]
    partial class Application
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("Myapi.Models.Account", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(11);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("ID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Myapi.Models.Application", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AccountID");

                    b.Property<Guid>("AppId");

                    b.Property<string>("AppName")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<string>("AppSecret")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<DateTime>("CreateTime");

                    b.HasKey("ID");

                    b.ToTable("Applications");
                });
#pragma warning restore 612, 618
        }
    }
}
