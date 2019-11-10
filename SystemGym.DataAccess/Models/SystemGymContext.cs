using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SystemGym.DataAccess.Models
{
    public partial class SystemGymContext : DbContext
    {
        public SystemGymContext(DbContextOptions<SystemGymContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aluno> Aluno { get; set; }
        public virtual DbSet<Ano> Ano { get; set; }
        public virtual DbSet<Colaborador> Colaborador { get; set; }
        public virtual DbSet<FormaPagamento> FormaPagamento { get; set; }
        public virtual DbSet<Funcao> Funcao { get; set; }
        public virtual DbSet<GerenciarAluno> GerenciarAluno { get; set; }
        public virtual DbSet<GerenciarVisitante> GerenciarVisitante { get; set; }
        public virtual DbSet<Mes> Mes { get; set; }
        public virtual DbSet<Pagamento> Pagamento { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Sexo> Sexo { get; set; }
        public virtual DbSet<SituacaoAluno> SituacaoAluno { get; set; }
        public virtual DbSet<SituacaoColaborador> SituacaoColaborador { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }
        public virtual DbSet<TipoNotificacao> TipoNotificacao { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Visitante> Visitante { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.HasIndex(e => e.NumeroCartao)
                    .HasName("IX_Aluno")
                    .IsUnique();

                entity.Property(e => e.AlunoId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AlteracaoData).HasColumnType("datetime");

                entity.Property(e => e.CriacaoData).HasColumnType("datetime");

                entity.Property(e => e.NumeroCartao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pessoa)
                    .WithMany(p => p.Aluno)
                    .HasForeignKey(d => d.PessoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Aluno_Pessoa");

                entity.HasOne(d => d.SituacaoAluno)
                    .WithMany(p => p.Aluno)
                    .HasForeignKey(d => d.SituacaoAlunoId)
                    .HasConstraintName("FK_Aluno_SituacaoAluno");
            });

            modelBuilder.Entity<Ano>(entity =>
            {
                entity.Property(e => e.AnoId).ValueGeneratedNever();

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Colaborador>(entity =>
            {
                entity.Property(e => e.ColaboradorId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AlteracaoData).HasColumnType("datetime");

                entity.Property(e => e.CriacaoData).HasColumnType("datetime");

                entity.HasOne(d => d.Funcao)
                    .WithMany(p => p.Colaborador)
                    .HasForeignKey(d => d.FuncaoId)
                    .HasConstraintName("FK_Colaborador_Funcao");

                entity.HasOne(d => d.Pessoa)
                    .WithMany(p => p.Colaborador)
                    .HasForeignKey(d => d.PessoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Colaborador_Pessoa");

                entity.HasOne(d => d.SituacaoColaborador)
                    .WithMany(p => p.Colaborador)
                    .HasForeignKey(d => d.SituacaoColaboradorId)
                    .HasConstraintName("FK_Colaborador_SituacaoColaborador");
            });

            modelBuilder.Entity<FormaPagamento>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Funcao>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GerenciarAluno>(entity =>
            {
                entity.Property(e => e.GerenciarAlunoId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DataNotificacao).HasColumnType("datetime");

                entity.Property(e => e.DataRelatorio).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Aluno)
                    .WithMany(p => p.GerenciarAluno)
                    .HasForeignKey(d => d.AlunoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GerenciarAluno_Aluno");

                entity.HasOne(d => d.TipoNotificacao)
                    .WithMany(p => p.GerenciarAluno)
                    .HasForeignKey(d => d.TipoNotificacaoId)
                    .HasConstraintName("FK_GerenciarAluno_TipoNotificacao");
            });

            modelBuilder.Entity<GerenciarVisitante>(entity =>
            {
                entity.HasKey(e => e.GerenciarVisitante1);

                entity.Property(e => e.GerenciarVisitante1)
                    .HasColumnName("GerenciarVisitante")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DataVisita).HasColumnType("datetime");

                entity.Property(e => e.HoraVisita).HasColumnType("datetime");

                entity.Property(e => e.NomeVisitante)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Visitante)
                    .WithMany(p => p.GerenciarVisitante)
                    .HasForeignKey(d => d.VisitanteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GerenciarVisitante_Visitante");
            });

            modelBuilder.Entity<Mes>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pagamento>(entity =>
            {
                entity.HasIndex(e => e.PagamentoId)
                    .HasName("IX_Pagamento_Ano")
                    .IsUnique();

                entity.Property(e => e.PagamentoId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DataCriacao).HasColumnType("datetime");

                entity.Property(e => e.DataPagamento).HasColumnType("datetime");

                entity.Property(e => e.ValorMensalidade)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Aluno)
                    .WithMany(p => p.Pagamento)
                    .HasForeignKey(d => d.AlunoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pagamento_Aluno");

                entity.HasOne(d => d.Ano)
                    .WithMany(p => p.Pagamento)
                    .HasForeignKey(d => d.AnoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pagamento_Ano");

                entity.HasOne(d => d.Colaborador)
                    .WithMany(p => p.Pagamento)
                    .HasForeignKey(d => d.ColaboradorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pagamento_Colaborador");

                entity.HasOne(d => d.FormaPagamento)
                    .WithMany(p => p.Pagamento)
                    .HasForeignKey(d => d.FormaPagamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pagamento_FormaPagamento");

                entity.HasOne(d => d.Mes)
                    .WithMany(p => p.Pagamento)
                    .HasForeignKey(d => d.MesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pagamento_Mes");
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasIndex(e => e.Cpf)
                    .HasName("IX_Pessoa")
                    .IsUnique();

                entity.HasIndex(e => e.TelefoneCelular)
                    .HasName("IX_Pessoa_1")
                    .IsUnique();

                entity.Property(e => e.PessoaId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AlteracaoData).HasColumnType("datetime");

                entity.Property(e => e.Cpf)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.CriacaoData).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Endereco)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelefoneCasa)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.TelefoneCelular)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.Sexo)
                    .WithMany(p => p.Pessoa)
                    .HasForeignKey(d => d.SexoId)
                    .HasConstraintName("FK_Pessoa_Sexo");

                entity.HasOne(d => d.Tipo)
                    .WithMany(p => p.Pessoa)
                    .HasForeignKey(d => d.TipoId)
                    .HasConstraintName("FK_Pessoa_Tipo");
            });

            modelBuilder.Entity<Sexo>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SituacaoAluno>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SituacaoColaborador>(entity =>
            {
                entity.Property(e => e.Descricao).HasMaxLength(10);
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoNotificacao>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.UserName)
                    .HasName("IX_Usuario")
                    .IsUnique();

                entity.Property(e => e.UsuarioId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.DataAcesso).HasColumnType("datetime");

                entity.Property(e => e.DataAlteracao).HasColumnType("datetime");

                entity.Property(e => e.DataCadastro).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pessoa)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.PessoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Pessoa");
            });

            modelBuilder.Entity<Visitante>(entity =>
            {
                entity.HasIndex(e => e.DocIdentidade)
                    .HasName("IX_Visitante")
                    .IsUnique();

                entity.Property(e => e.VisitanteId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AlteracaoData).HasColumnType("datetime");

                entity.Property(e => e.CriacaoData).HasColumnType("datetime");

                entity.Property(e => e.DocIdentidade)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VisitaData).HasColumnType("datetime");

                entity.HasOne(d => d.Pessoa)
                    .WithMany(p => p.Visitante)
                    .HasForeignKey(d => d.PessoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visitante_Pessoa");
            });
        }
    }
}
