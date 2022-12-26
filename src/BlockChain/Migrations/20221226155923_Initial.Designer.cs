﻿// <auto-generated />
using BlockChain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlockChain.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20221226155923_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BlockChain.Block", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Data")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PreviousHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("SignedData")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("SignedHash")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Blocks");
                });
#pragma warning restore 612, 618
        }
    }
}