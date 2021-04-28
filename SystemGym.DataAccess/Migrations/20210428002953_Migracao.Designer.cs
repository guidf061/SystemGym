﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SystemGym.DataAccess.Models;

namespace SystemGym.DataAccess.Migrations
{
    [DbContext(typeof(SystemGymContext))]
    [Migration("20210428002953_Migracao")]
    partial class Migracao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SystemGym.DataAccess.Models.Aluno", b =>
                {
                    b.Property<Guid>("AlunoId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime?>("AlteracaoData")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("CriacaoData")
                        .HasColumnType("datetime");

                    b.Property<string>("NumeroCartao")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("NumeroWhatsapp")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<Guid>("PessoaId");

                    b.HasKey("AlunoId");

                    b.HasIndex("NumeroCartao")
                        .IsUnique()
                        .HasName("IX_Aluno")
                        .HasFilter("[NumeroCartao] IS NOT NULL");

                    b.HasIndex("PessoaId");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Ano", b =>
                {
                    b.Property<int>("AnoId");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.HasKey("AnoId");

                    b.ToTable("Ano");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("StateId");

                    b.HasKey("CityId");

                    b.HasIndex("StateId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Colaborador", b =>
                {
                    b.Property<Guid>("ColaboradorId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime>("AlteracaoData")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("CriacaoData")
                        .HasColumnType("datetime");

                    b.Property<string>("DocIdentidade")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("FuncaoId");

                    b.Property<string>("NumeroCtps")
                        .HasMaxLength(7)
                        .IsUnicode(false);

                    b.Property<string>("NumeroPisPasep")
                        .HasMaxLength(11)
                        .IsUnicode(false);

                    b.Property<string>("NumeroSerieCtps")
                        .HasMaxLength(4)
                        .IsUnicode(false);

                    b.Property<Guid>("PessoaId");

                    b.Property<int?>("SituacaoColaboradorId");

                    b.HasKey("ColaboradorId");

                    b.HasIndex("FuncaoId");

                    b.HasIndex("PessoaId");

                    b.HasIndex("SituacaoColaboradorId");

                    b.ToTable("Colaborador");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("CountryId");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.FormaPagamento", b =>
                {
                    b.Property<int>("FormaPagamentoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("FormaPagamentoId");

                    b.ToTable("FormaPagamento");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Funcao", b =>
                {
                    b.Property<int>("FuncaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("FuncaoId");

                    b.ToTable("Funcao");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.GerenciarAluno", b =>
                {
                    b.Property<Guid>("GerenciarAlunoId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(newid())");

                    b.Property<Guid>("AlunoId");

                    b.Property<DateTime?>("DataNotificacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataRelatorio")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Nome")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Telefone")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("TipoNotificacaoId");

                    b.HasKey("GerenciarAlunoId");

                    b.HasIndex("AlunoId");

                    b.HasIndex("TipoNotificacaoId");

                    b.ToTable("GerenciarAluno");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.GerenciarVisitante", b =>
                {
                    b.Property<Guid>("GerenciarVisitante1")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("GerenciarVisitante")
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime>("DataVisita")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("HoraVisita")
                        .HasColumnType("datetime");

                    b.Property<string>("NomeVisitante")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("QuantidadeVisita");

                    b.Property<Guid>("VisitanteId");

                    b.HasKey("GerenciarVisitante1");

                    b.HasIndex("VisitanteId");

                    b.ToTable("GerenciarVisitante");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.MatriculaAluno", b =>
                {
                    b.Property<Guid>("MatriculaAlunoId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime?>("AlteracaoDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("AlunoId");

                    b.Property<bool>("Ativo");

                    b.Property<DateTime?>("CancelamentoDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CriacaoDate")
                        .HasColumnType("datetime");

                    b.Property<int>("SituacaoMatriculaId");

                    b.HasKey("MatriculaAlunoId");

                    b.HasIndex("AlunoId");

                    b.HasIndex("SituacaoMatriculaId");

                    b.ToTable("MatriculaAluno");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.MatriculaColaborador", b =>
                {
                    b.Property<Guid>("MatriculaColaboradorId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(newid())");

                    b.Property<bool?>("Ativo");

                    b.Property<DateTime>("CancelamentoDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("ColaboradorId");

                    b.Property<DateTime?>("CriacaoDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("SituacaoMatriculaId");

                    b.HasKey("MatriculaColaboradorId");

                    b.HasIndex("ColaboradorId");

                    b.HasIndex("SituacaoMatriculaId");

                    b.ToTable("MatriculaColaborador");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Mes", b =>
                {
                    b.Property<int>("MesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CriacaoDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.HasKey("MesId");

                    b.ToTable("Mes");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Pagamento", b =>
                {
                    b.Property<Guid>("PagamentoId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime?>("AlteracaoDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("AlunoId");

                    b.Property<int>("AnoId");

                    b.Property<DateTime>("CriacaoDate")
                        .HasColumnType("datetime");

                    b.Property<int>("FormaPagamentoId");

                    b.Property<int>("MesId");

                    b.Property<DateTime>("PagamentoDate")
                        .HasColumnType("datetime");

                    b.Property<int>("PlanoId");

                    b.Property<Guid?>("UsuarioId");

                    b.Property<string>("ValorMensalidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("PagamentoId");

                    b.HasIndex("AlunoId");

                    b.HasIndex("AnoId");

                    b.HasIndex("FormaPagamentoId");

                    b.HasIndex("MesId");

                    b.HasIndex("PagamentoId")
                        .IsUnique()
                        .HasName("IX_Pagamento_Ano");

                    b.HasIndex("PlanoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pagamento");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Permissao", b =>
                {
                    b.Property<int>("PermissaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("PermissaoId");

                    b.ToTable("Permissao");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Pessoa", b =>
                {
                    b.Property<Guid>("PessoaId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime?>("AlteracaoData")
                        .HasColumnType("datetime");

                    b.Property<int?>("CityId");

                    b.Property<int?>("CountryId");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime?>("CriacaoData")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Endereco")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int?>("PermissaoId");

                    b.Property<int>("SexoId");

                    b.Property<int?>("StateId");

                    b.Property<string>("TelefoneCasa")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("TelefoneCelular")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("PessoaId");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("Cpf")
                        .IsUnique()
                        .HasName("IX_Pessoa");

                    b.HasIndex("PermissaoId");

                    b.HasIndex("SexoId");

                    b.HasIndex("StateId");

                    b.HasIndex("TelefoneCelular")
                        .IsUnique()
                        .HasName("IX_Pessoa_1");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Plano", b =>
                {
                    b.Property<int>("PlanoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("Valor");

                    b.HasKey("PlanoId");

                    b.ToTable("Plano");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Sexo", b =>
                {
                    b.Property<int>("SexoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.HasKey("SexoId");

                    b.ToTable("Sexo");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.SituacaoColaborador", b =>
                {
                    b.Property<int>("SituacaoColaboradorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasMaxLength(10);

                    b.HasKey("SituacaoColaboradorId");

                    b.ToTable("SituacaoColaborador");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.SituacaoMatricula", b =>
                {
                    b.Property<int>("SituacaoMatriculaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("SituacaoMatriculaId");

                    b.ToTable("SituacaoMatricula");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acronym")
                        .IsRequired()
                        .HasMaxLength(2)
                        .IsUnicode(false);

                    b.Property<int?>("CountryId");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("StateId");

                    b.HasIndex("CountryId");

                    b.ToTable("State");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.TipoNotificacao", b =>
                {
                    b.Property<int>("TipoNotificacaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("TipoNotificacaoId");

                    b.ToTable("TipoNotificacao");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Usuario", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime?>("DataAcesso")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<Guid>("PessoaId");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("UsuarioId");

                    b.HasIndex("PessoaId");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasName("IX_Usuario");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Visitante", b =>
                {
                    b.Property<Guid>("VisitanteId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("(newid())");

                    b.Property<DateTime?>("AlteracaoData")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("CriacaoData")
                        .HasColumnType("datetime");

                    b.Property<string>("DocIdentidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<Guid>("PessoaId");

                    b.Property<DateTime?>("VisitaData")
                        .HasColumnType("datetime");

                    b.HasKey("VisitanteId");

                    b.HasIndex("DocIdentidade")
                        .IsUnique()
                        .HasName("IX_Visitante");

                    b.HasIndex("PessoaId");

                    b.ToTable("Visitante");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Aluno", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.Pessoa", "Pessoa")
                        .WithMany("Aluno")
                        .HasForeignKey("PessoaId")
                        .HasConstraintName("FK_Aluno_Pessoa");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.City", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.State", "State")
                        .WithMany("City")
                        .HasForeignKey("StateId")
                        .HasConstraintName("FK_City_State");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Colaborador", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.Funcao", "Funcao")
                        .WithMany("Colaborador")
                        .HasForeignKey("FuncaoId")
                        .HasConstraintName("FK_Colaborador_Funcao");

                    b.HasOne("SystemGym.DataAccess.Models.Pessoa", "Pessoa")
                        .WithMany("Colaborador")
                        .HasForeignKey("PessoaId")
                        .HasConstraintName("FK_Colaborador_Pessoa");

                    b.HasOne("SystemGym.DataAccess.Models.SituacaoColaborador", "SituacaoColaborador")
                        .WithMany("Colaborador")
                        .HasForeignKey("SituacaoColaboradorId")
                        .HasConstraintName("FK_Colaborador_SituacaoColaborador");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.GerenciarAluno", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.Aluno", "Aluno")
                        .WithMany("GerenciarAluno")
                        .HasForeignKey("AlunoId")
                        .HasConstraintName("FK_GerenciarAluno_Aluno");

                    b.HasOne("SystemGym.DataAccess.Models.TipoNotificacao", "TipoNotificacao")
                        .WithMany("GerenciarAluno")
                        .HasForeignKey("TipoNotificacaoId")
                        .HasConstraintName("FK_GerenciarAluno_TipoNotificacao");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.GerenciarVisitante", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.Visitante", "Visitante")
                        .WithMany("GerenciarVisitante")
                        .HasForeignKey("VisitanteId")
                        .HasConstraintName("FK_GerenciarVisitante_Visitante");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.MatriculaAluno", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.Aluno", "Aluno")
                        .WithMany("MatriculaAluno")
                        .HasForeignKey("AlunoId")
                        .HasConstraintName("FK_MatriculaAluno_Aluno");

                    b.HasOne("SystemGym.DataAccess.Models.SituacaoMatricula", "SituacaoMatricula")
                        .WithMany("MatriculaAluno")
                        .HasForeignKey("SituacaoMatriculaId")
                        .HasConstraintName("FK_MatriculaAluno_SituacaoMatricula");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.MatriculaColaborador", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.Colaborador", "Colaborador")
                        .WithMany("MatriculaColaborador")
                        .HasForeignKey("ColaboradorId")
                        .HasConstraintName("FK_MatriculaColaborador_Colaborador");

                    b.HasOne("SystemGym.DataAccess.Models.SituacaoMatricula", "SituacaoMatricula")
                        .WithMany("MatriculaColaborador")
                        .HasForeignKey("SituacaoMatriculaId")
                        .HasConstraintName("FK_MatriculaColaborador_SituacaoMatricula");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Pagamento", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.Aluno", "Aluno")
                        .WithMany("Pagamento")
                        .HasForeignKey("AlunoId")
                        .HasConstraintName("FK_Pagamento_Aluno");

                    b.HasOne("SystemGym.DataAccess.Models.Ano", "Ano")
                        .WithMany("Pagamento")
                        .HasForeignKey("AnoId")
                        .HasConstraintName("FK_Pagamento_Ano");

                    b.HasOne("SystemGym.DataAccess.Models.FormaPagamento", "FormaPagamento")
                        .WithMany("Pagamento")
                        .HasForeignKey("FormaPagamentoId")
                        .HasConstraintName("FK_Pagamento_FormaPagamento");

                    b.HasOne("SystemGym.DataAccess.Models.Mes", "Mes")
                        .WithMany("Pagamento")
                        .HasForeignKey("MesId")
                        .HasConstraintName("FK_Pagamento_Mes");

                    b.HasOne("SystemGym.DataAccess.Models.Plano", "Plano")
                        .WithMany("Pagamento")
                        .HasForeignKey("PlanoId")
                        .HasConstraintName("FK_Pagamento_Plano");

                    b.HasOne("SystemGym.DataAccess.Models.Usuario", "Usuario")
                        .WithMany("Pagamento")
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("FK_Pagamento_Usuario");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Pessoa", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.City", "City")
                        .WithMany("Pessoa")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK_Pessoa_City");

                    b.HasOne("SystemGym.DataAccess.Models.Country", "Country")
                        .WithMany("Pessoa")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK_Pessoa_Country");

                    b.HasOne("SystemGym.DataAccess.Models.Permissao", "Permissao")
                        .WithMany("Pessoa")
                        .HasForeignKey("PermissaoId")
                        .HasConstraintName("FK_Pessoa_Permissao");

                    b.HasOne("SystemGym.DataAccess.Models.Sexo", "Sexo")
                        .WithMany("Pessoa")
                        .HasForeignKey("SexoId")
                        .HasConstraintName("FK_Pessoa_Sexo");

                    b.HasOne("SystemGym.DataAccess.Models.State", "State")
                        .WithMany("Pessoa")
                        .HasForeignKey("StateId")
                        .HasConstraintName("FK_Pessoa_State");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.State", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.Country", "Country")
                        .WithMany("State")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK_State_Country");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Usuario", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.Pessoa", "Pessoa")
                        .WithMany("Usuario")
                        .HasForeignKey("PessoaId")
                        .HasConstraintName("FK_Usuario_Pessoa");
                });

            modelBuilder.Entity("SystemGym.DataAccess.Models.Visitante", b =>
                {
                    b.HasOne("SystemGym.DataAccess.Models.Pessoa", "Pessoa")
                        .WithMany("Visitante")
                        .HasForeignKey("PessoaId")
                        .HasConstraintName("FK_Visitante_Pessoa");
                });
#pragma warning restore 612, 618
        }
    }
}
