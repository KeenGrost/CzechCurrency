﻿// <auto-generated />
using System;
using CzechCurrency.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CzechCurrency.Data.Migrations.Migrations
{
    [DbContext(typeof(CzechCurrencyDbContext))]
    partial class CzechCurrencyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CzechCurrency.Entities.Currency", b =>
                {
                    b.Property<string>("Code")
                        .HasColumnName("code")
                        .HasColumnType("text");

                    b.Property<int>("Amount")
                        .HasColumnName("amount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .HasColumnName("number")
                        .HasColumnType("text");

                    b.HasKey("Code");

                    b.ToTable("currencies");

                    b.HasData(
                        new
                        {
                            Code = "CZK",
                            Amount = 1,
                            Name = "Чешская крона",
                            Number = "203"
                        },
                        new
                        {
                            Code = "RUB",
                            Amount = 1,
                            Name = "Российский рубль",
                            Number = "643"
                        },
                        new
                        {
                            Code = "USD",
                            Amount = 1,
                            Name = "Доллар США",
                            Number = "840"
                        },
                        new
                        {
                            Code = "EUR",
                            Amount = 1,
                            Name = "Евро",
                            Number = "978"
                        });
                });

            modelBuilder.Entity("CzechCurrency.Entities.ExchangeRate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CurrencyCode")
                        .HasColumnName("code")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnName("date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Value")
                        .HasColumnName("value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyCode");

                    b.ToTable("exchange_rates");
                });

            modelBuilder.Entity("CzechCurrency.Entities.ExchangeRate", b =>
                {
                    b.HasOne("CzechCurrency.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyCode");
                });
#pragma warning restore 612, 618
        }
    }
}
