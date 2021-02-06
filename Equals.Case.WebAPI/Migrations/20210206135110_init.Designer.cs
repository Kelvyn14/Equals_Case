﻿// <auto-generated />
using System;
using Equals.Case.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Equals.Case.WebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210206135110_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("Equals.Case.WebAPI.Model.Adquirente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.Property<int>("PeriodicidadeId");

                    b.HasKey("Id");

                    b.ToTable("Adquirentes");
                });

            modelBuilder.Entity("Equals.Case.WebAPI.Model.Arquivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdquirenteId");

                    b.Property<string>("Conteudo");

                    b.Property<DateTime?>("DataRecebimento");

                    b.Property<string>("Nome");

                    b.Property<bool>("Recebido");

                    b.HasKey("Id");

                    b.ToTable("Arquivos");
                });

            modelBuilder.Entity("Equals.Case.WebAPI.Model.Periodicidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Periodo");

                    b.HasKey("Id");

                    b.ToTable("Periodicidades");
                });
#pragma warning restore 612, 618
        }
    }
}
