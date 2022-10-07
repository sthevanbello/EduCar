﻿// <auto-generated />
using System;
using EduCar.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EduCar.Migrations
{
    [DbContext(typeof(EduCarContext))]
    partial class EduCarContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EduCar.Models.Cambio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cambio");
                });

            modelBuilder.Entity("EduCar.Models.CaracteristicasGerais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Assentos")
                        .HasColumnType("int");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Portas")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CaracteristicasGerais");
                });

            modelBuilder.Entity("EduCar.Models.Cartao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bandeira")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CPF_CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int>("CVV")
                        .HasMaxLength(3)
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Titular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Vencimento")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Cartao");
                });

            modelBuilder.Entity("EduCar.Models.Concessionaria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdEndereco")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdEndereco");

                    b.ToTable("Concessionaria");
                });

            modelBuilder.Entity("EduCar.Models.Direcao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Direcao");
                });

            modelBuilder.Entity("EduCar.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("EduCar.Models.FichaTecnica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<double>("Consumo")
                        .HasColumnType("float");

                    b.Property<int>("IdCambio")
                        .HasColumnType("int");

                    b.Property<int>("IdDirecao")
                        .HasColumnType("int");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quilometragem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdCambio");

                    b.HasIndex("IdDirecao");

                    b.ToTable("FichaTecnica");
                });

            modelBuilder.Entity("EduCar.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdConcessionaria")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("IdVeiculo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdConcessionaria");

                    b.HasIndex("IdUsuario");

                    b.HasIndex("IdVeiculo");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("EduCar.Models.TipoUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoUsuario");
                });

            modelBuilder.Entity("EduCar.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aceite")
                        .HasColumnType("bit");

                    b.Property<string>("CPF_CNPJ")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdEndereço")
                        .HasColumnType("int");

                    b.Property<int>("IdTipoUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdEndereço");

                    b.HasIndex("IdTipoUsuario");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("EduCar.Models.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdCaracteristicasGerais")
                        .HasColumnType("int");

                    b.Property<int>("IdConcessionaria")
                        .HasColumnType("int");

                    b.Property<int>("IdFichaTecnica")
                        .HasColumnType("int");

                    b.Property<string>("Imagem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IdCaracteristicasGerais");

                    b.HasIndex("IdConcessionaria");

                    b.HasIndex("IdFichaTecnica");

                    b.ToTable("Veiculo");
                });

            modelBuilder.Entity("EduCar.Models.Cartao", b =>
                {
                    b.HasOne("EduCar.Models.Usuario", "Usuario")
                        .WithMany("Cartoes")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("EduCar.Models.Concessionaria", b =>
                {
                    b.HasOne("EduCar.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("IdEndereco")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("EduCar.Models.FichaTecnica", b =>
                {
                    b.HasOne("EduCar.Models.Cambio", "Cambio")
                        .WithMany("FichasTecnicas")
                        .HasForeignKey("IdCambio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduCar.Models.Direcao", "Direcao")
                        .WithMany("FichasTecnicas")
                        .HasForeignKey("IdDirecao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cambio");

                    b.Navigation("Direcao");
                });

            modelBuilder.Entity("EduCar.Models.Pedido", b =>
                {
                    b.HasOne("EduCar.Models.Concessionaria", "Concessionaria")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdConcessionaria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduCar.Models.Usuario", "Usuario")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduCar.Models.Veiculo", "Veiculo")
                        .WithMany()
                        .HasForeignKey("IdVeiculo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Concessionaria");

                    b.Navigation("Usuario");

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("EduCar.Models.Usuario", b =>
                {
                    b.HasOne("EduCar.Models.Endereco", "Endereco")
                        .WithMany()
                        .HasForeignKey("IdEndereço")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduCar.Models.TipoUsuario", "TipoUsuario")
                        .WithMany()
                        .HasForeignKey("IdTipoUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");

                    b.Navigation("TipoUsuario");
                });

            modelBuilder.Entity("EduCar.Models.Veiculo", b =>
                {
                    b.HasOne("EduCar.Models.CaracteristicasGerais", "CaracteristicasGerais")
                        .WithMany()
                        .HasForeignKey("IdCaracteristicasGerais")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduCar.Models.Concessionaria", "Concessionaria")
                        .WithMany("Veiculos")
                        .HasForeignKey("IdConcessionaria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduCar.Models.FichaTecnica", "FichaTecnica")
                        .WithMany()
                        .HasForeignKey("IdFichaTecnica")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CaracteristicasGerais");

                    b.Navigation("Concessionaria");

                    b.Navigation("FichaTecnica");
                });

            modelBuilder.Entity("EduCar.Models.Cambio", b =>
                {
                    b.Navigation("FichasTecnicas");
                });

            modelBuilder.Entity("EduCar.Models.Concessionaria", b =>
                {
                    b.Navigation("Pedidos");

                    b.Navigation("Veiculos");
                });

            modelBuilder.Entity("EduCar.Models.Direcao", b =>
                {
                    b.Navigation("FichasTecnicas");
                });

            modelBuilder.Entity("EduCar.Models.Usuario", b =>
                {
                    b.Navigation("Cartoes");

                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}
