using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemGym.DataAccess.Migrations
{
    public partial class Migracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ano",
                columns: table => new
                {
                    AnoId = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(unicode: false, maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ano", x => x.AnoId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "FormaPagamento",
                columns: table => new
                {
                    FormaPagamentoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamento", x => x.FormaPagamentoId);
                });

            migrationBuilder.CreateTable(
                name: "Funcao",
                columns: table => new
                {
                    FuncaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcao", x => x.FuncaoId);
                });

            migrationBuilder.CreateTable(
                name: "Mes",
                columns: table => new
                {
                    MesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    CriacaoDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mes", x => x.MesId);
                });

            migrationBuilder.CreateTable(
                name: "Permissao",
                columns: table => new
                {
                    PermissaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissao", x => x.PermissaoId);
                });

            migrationBuilder.CreateTable(
                name: "Plano",
                columns: table => new
                {
                    PlanoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Valor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plano", x => x.PlanoId);
                });

            migrationBuilder.CreateTable(
                name: "Sexo",
                columns: table => new
                {
                    SexoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexo", x => x.SexoId);
                });

            migrationBuilder.CreateTable(
                name: "SituacaoColaborador",
                columns: table => new
                {
                    SituacaoColaboradorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SituacaoColaborador", x => x.SituacaoColaboradorId);
                });

            migrationBuilder.CreateTable(
                name: "SituacaoMatricula",
                columns: table => new
                {
                    SituacaoMatriculaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SituacaoMatricula", x => x.SituacaoMatriculaId);
                });

            migrationBuilder.CreateTable(
                name: "TipoNotificacao",
                columns: table => new
                {
                    TipoNotificacaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoNotificacao", x => x.TipoNotificacaoId);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Acronym = table.Column<string>(unicode: false, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_State_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    StateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_City_State",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    PessoaId = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    Nome = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Cpf = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    SexoId = table.Column<int>(nullable: false),
                    PermissaoId = table.Column<int>(nullable: true),
                    Endereco = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    TelefoneCelular = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    TelefoneCasa = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CriacaoData = table.Column<DateTime>(type: "datetime", nullable: true),
                    AlteracaoData = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime", nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.PessoaId);
                    table.ForeignKey(
                        name: "FK_Pessoa_City",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pessoa_Country",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pessoa_Permissao",
                        column: x => x.PermissaoId,
                        principalTable: "Permissao",
                        principalColumn: "PermissaoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pessoa_Sexo",
                        column: x => x.SexoId,
                        principalTable: "Sexo",
                        principalColumn: "SexoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pessoa_State",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    AlunoId = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    PessoaId = table.Column<Guid>(nullable: false),
                    NumeroCartao = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    NumeroWhatsapp = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CriacaoData = table.Column<DateTime>(type: "datetime", nullable: true),
                    AlteracaoData = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.AlunoId);
                    table.ForeignKey(
                        name: "FK_Aluno_Pessoa",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Colaborador",
                columns: table => new
                {
                    ColaboradorId = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    PessoaId = table.Column<Guid>(nullable: false),
                    FuncaoId = table.Column<int>(nullable: true),
                    SituacaoColaboradorId = table.Column<int>(nullable: true),
                    CriacaoData = table.Column<DateTime>(type: "datetime", nullable: true),
                    AlteracaoData = table.Column<DateTime>(type: "datetime", nullable: false),
                    NumeroSerieCtps = table.Column<string>(unicode: false, maxLength: 4, nullable: true),
                    NumeroCtps = table.Column<string>(unicode: false, maxLength: 7, nullable: true),
                    NumeroPisPasep = table.Column<string>(unicode: false, maxLength: 11, nullable: true),
                    DocIdentidade = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaborador", x => x.ColaboradorId);
                    table.ForeignKey(
                        name: "FK_Colaborador_Funcao",
                        column: x => x.FuncaoId,
                        principalTable: "Funcao",
                        principalColumn: "FuncaoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Colaborador_Pessoa",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Colaborador_SituacaoColaborador",
                        column: x => x.SituacaoColaboradorId,
                        principalTable: "SituacaoColaborador",
                        principalColumn: "SituacaoColaboradorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    PessoaId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    DataAcesso = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuario_Pessoa",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visitante",
                columns: table => new
                {
                    VisitanteId = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    PessoaId = table.Column<Guid>(nullable: false),
                    DocIdentidade = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    VisitaData = table.Column<DateTime>(type: "datetime", nullable: true),
                    CriacaoData = table.Column<DateTime>(type: "datetime", nullable: true),
                    AlteracaoData = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitante", x => x.VisitanteId);
                    table.ForeignKey(
                        name: "FK_Visitante_Pessoa",
                        column: x => x.PessoaId,
                        principalTable: "Pessoa",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GerenciarAluno",
                columns: table => new
                {
                    GerenciarAlunoId = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    AlunoId = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Telefone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    DataNotificacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    TipoNotificacaoId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    DataRelatorio = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GerenciarAluno", x => x.GerenciarAlunoId);
                    table.ForeignKey(
                        name: "FK_GerenciarAluno_Aluno",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "AlunoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GerenciarAluno_TipoNotificacao",
                        column: x => x.TipoNotificacaoId,
                        principalTable: "TipoNotificacao",
                        principalColumn: "TipoNotificacaoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatriculaAluno",
                columns: table => new
                {
                    MatriculaAlunoId = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    AlunoId = table.Column<Guid>(nullable: false),
                    SituacaoMatriculaId = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    CriacaoDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CancelamentoDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AlteracaoDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatriculaAluno", x => x.MatriculaAlunoId);
                    table.ForeignKey(
                        name: "FK_MatriculaAluno_Aluno",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "AlunoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatriculaAluno_SituacaoMatricula",
                        column: x => x.SituacaoMatriculaId,
                        principalTable: "SituacaoMatricula",
                        principalColumn: "SituacaoMatriculaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatriculaColaborador",
                columns: table => new
                {
                    MatriculaColaboradorId = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    ColaboradorId = table.Column<Guid>(nullable: false),
                    SituacaoMatriculaId = table.Column<int>(nullable: true),
                    Ativo = table.Column<bool>(nullable: true),
                    CriacaoDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CancelamentoDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatriculaColaborador", x => x.MatriculaColaboradorId);
                    table.ForeignKey(
                        name: "FK_MatriculaColaborador_Colaborador",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaborador",
                        principalColumn: "ColaboradorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatriculaColaborador_SituacaoMatricula",
                        column: x => x.SituacaoMatriculaId,
                        principalTable: "SituacaoMatricula",
                        principalColumn: "SituacaoMatriculaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    PagamentoId = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    AlunoId = table.Column<Guid>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: true),
                    PlanoId = table.Column<int>(nullable: false),
                    ValorMensalidade = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    MesId = table.Column<int>(nullable: false),
                    AnoId = table.Column<int>(nullable: false),
                    FormaPagamentoId = table.Column<int>(nullable: false),
                    PagamentoDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CriacaoDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AlteracaoDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.PagamentoId);
                    table.ForeignKey(
                        name: "FK_Pagamento_Aluno",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "AlunoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagamento_Ano",
                        column: x => x.AnoId,
                        principalTable: "Ano",
                        principalColumn: "AnoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagamento_FormaPagamento",
                        column: x => x.FormaPagamentoId,
                        principalTable: "FormaPagamento",
                        principalColumn: "FormaPagamentoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagamento_Mes",
                        column: x => x.MesId,
                        principalTable: "Mes",
                        principalColumn: "MesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagamento_Plano",
                        column: x => x.PlanoId,
                        principalTable: "Plano",
                        principalColumn: "PlanoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagamento_Usuario",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GerenciarVisitante",
                columns: table => new
                {
                    GerenciarVisitante = table.Column<Guid>(nullable: false, defaultValueSql: "(newid())"),
                    VisitanteId = table.Column<Guid>(nullable: false),
                    DataVisita = table.Column<DateTime>(type: "datetime", nullable: false),
                    HoraVisita = table.Column<DateTime>(type: "datetime", nullable: false),
                    QuantidadeVisita = table.Column<int>(nullable: true),
                    NomeVisitante = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GerenciarVisitante", x => x.GerenciarVisitante);
                    table.ForeignKey(
                        name: "FK_GerenciarVisitante_Visitante",
                        column: x => x.VisitanteId,
                        principalTable: "Visitante",
                        principalColumn: "VisitanteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno",
                table: "Aluno",
                column: "NumeroCartao",
                unique: true,
                filter: "[NumeroCartao] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_PessoaId",
                table: "Aluno",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_City_StateId",
                table: "City",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_FuncaoId",
                table: "Colaborador",
                column: "FuncaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_PessoaId",
                table: "Colaborador",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Colaborador_SituacaoColaboradorId",
                table: "Colaborador",
                column: "SituacaoColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_GerenciarAluno_AlunoId",
                table: "GerenciarAluno",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_GerenciarAluno_TipoNotificacaoId",
                table: "GerenciarAluno",
                column: "TipoNotificacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_GerenciarVisitante_VisitanteId",
                table: "GerenciarVisitante",
                column: "VisitanteId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaAluno_AlunoId",
                table: "MatriculaAluno",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaAluno_SituacaoMatriculaId",
                table: "MatriculaAluno",
                column: "SituacaoMatriculaId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaColaborador_ColaboradorId",
                table: "MatriculaColaborador",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "IX_MatriculaColaborador_SituacaoMatriculaId",
                table: "MatriculaColaborador",
                column: "SituacaoMatriculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_AlunoId",
                table: "Pagamento",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_AnoId",
                table: "Pagamento",
                column: "AnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_FormaPagamentoId",
                table: "Pagamento",
                column: "FormaPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_MesId",
                table: "Pagamento",
                column: "MesId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_Ano",
                table: "Pagamento",
                column: "PagamentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_PlanoId",
                table: "Pagamento",
                column: "PlanoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_UsuarioId",
                table: "Pagamento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CityId",
                table: "Pessoa",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CountryId",
                table: "Pessoa",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa",
                table: "Pessoa",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_PermissaoId",
                table: "Pessoa",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_SexoId",
                table: "Pessoa",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_StateId",
                table: "Pessoa",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_1",
                table: "Pessoa",
                column: "TelefoneCelular",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_State_CountryId",
                table: "State",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PessoaId",
                table: "Usuario",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario",
                table: "Usuario",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitante",
                table: "Visitante",
                column: "DocIdentidade",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitante_PessoaId",
                table: "Visitante",
                column: "PessoaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GerenciarAluno");

            migrationBuilder.DropTable(
                name: "GerenciarVisitante");

            migrationBuilder.DropTable(
                name: "MatriculaAluno");

            migrationBuilder.DropTable(
                name: "MatriculaColaborador");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "TipoNotificacao");

            migrationBuilder.DropTable(
                name: "Visitante");

            migrationBuilder.DropTable(
                name: "Colaborador");

            migrationBuilder.DropTable(
                name: "SituacaoMatricula");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Ano");

            migrationBuilder.DropTable(
                name: "FormaPagamento");

            migrationBuilder.DropTable(
                name: "Mes");

            migrationBuilder.DropTable(
                name: "Plano");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Funcao");

            migrationBuilder.DropTable(
                name: "SituacaoColaborador");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Permissao");

            migrationBuilder.DropTable(
                name: "Sexo");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
