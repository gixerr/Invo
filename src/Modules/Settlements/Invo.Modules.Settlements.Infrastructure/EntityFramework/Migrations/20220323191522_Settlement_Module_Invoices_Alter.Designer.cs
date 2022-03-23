﻿// <auto-generated />
using System;
using Invo.Modules.Settlements.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Invo.Modules.Settlements.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(SettlementsDbContext))]
    [Migration("20220323191522_Settlement_Module_Invoices_Alter")]
    partial class Settlement_Module_Invoices_Alter
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("settlements")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Invo.Modules.Settlements.Domain.Entities.CostInvoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsCarInvoice")
                        .HasColumnType("boolean");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<decimal>("VatAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("CostInvoices");
                });

            modelBuilder.Entity("Invo.Modules.Settlements.Domain.Entities.IncomeInvoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfIssue")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("NetAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("Number")
                        .HasColumnType("text");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<decimal>("VatAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("IncomeInvoices");
                });
#pragma warning restore 612, 618
        }
    }
}
