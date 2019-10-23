using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Colaborador;
using SystemGym.Model.Pessoa;

namespace SystemGym.Service
{
    public class ColaboradorService
    {
        private readonly SystemGymContext context;
        private readonly PessoaService pessoaService;
        public ColaboradorService(SystemGymContext context, PessoaService pessoaService)
        {
            this.context = context;
            this.pessoaService = pessoaService;

        }

        public List<ColaboradorReturnModel> GetAll()
        {
            return this.context.Colaborador
                .Include(x => x.Pessoa)
                .OrderBy(x => x.Pessoa.Nome)
                .ToList().Select(x => new ColaboradorReturnModel()
                {
                    ColaboradorId = x.ColaboradorId,
                    FuncaoId = x.FuncaoId,
                    SituacaoColaboradorId = x.SituacaoColaboradorId,
                    CriacaoData = x.CriacaoData,
                    AlteracaoData = x.AlteracaoData,
                    Pessoa = new PessoaReturnModel()
                    {
                        PessoaId = x.Pessoa.PessoaId,
                        Nome = x.Pessoa.Nome,
                        Cpf = x.Pessoa.Cpf,
                        Email = x.Pessoa.Email,
                        TelefoneCasa = x.Pessoa.TelefoneCasa,
                        TelefoneCelular = x.Pessoa.TelefoneCelular,
                        Endereco = x.Pessoa.Endereco,
                        SexoId = x.Pessoa.SexoId,
                        TipoId = x.Pessoa.TipoId,
                        AlteracaoData = x.Pessoa.AlteracaoData,
                        CriacaoData = x.Pessoa.CriacaoData,
                    }
                })
                .ToList();
        }
        public ColaboradorReturnModel Get(Guid colaboradorId)
        {
            return this.context.Colaborador
                .Include(x => x.Pessoa)
                .Where(x => x.ColaboradorId.Equals(colaboradorId))
                .ToList().Select(x => new ColaboradorReturnModel()
                {
                    ColaboradorId = x.ColaboradorId,
                    FuncaoId = x.FuncaoId,
                    SituacaoColaboradorId = x.SituacaoColaboradorId,
                    CriacaoData = x.CriacaoData,
                    AlteracaoData = x.AlteracaoData,
                    Pessoa = new PessoaReturnModel()
                    {
                        PessoaId = x.Pessoa.PessoaId,
                        Nome = x.Pessoa.Nome,
                        Cpf = x.Pessoa.Cpf,
                        Email = x.Pessoa.Email,
                        TelefoneCasa = x.Pessoa.TelefoneCasa,
                        TelefoneCelular = x.Pessoa.TelefoneCelular,
                        Endereco = x.Pessoa.Endereco,
                        SexoId = x.Pessoa.SexoId,
                        TipoId = x.Pessoa.TipoId,
                        AlteracaoData = x.Pessoa.AlteracaoData,
                        CriacaoData = x.Pessoa.CriacaoData,
                    }
                })
                .FirstOrDefault();
        }
        public void Adicionar(ColaboradorBindingModel colaboradorModel)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {

                try
                {
                    var colaborador = new Colaborador()
                    {
                        FuncaoId = colaboradorModel.FuncaoId,
                        SituacaoColaboradorId = colaboradorModel.SituacaoColaboradorId,
                        CriacaoData = DateTime.UtcNow,
                        AlteracaoData = DateTime.UtcNow,
                        PessoaId = pessoaService.Adicionar(colaboradorModel.Pessoa)
                    };

                    this.context.Colaborador.Add(colaborador);
                    this.context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;

                }
            }
        }

        public void Alterar (Guid colaboradorId, ColaboradorBindingModel colaboradorModel)
        {
            var colaborador = this.context.Colaborador
                .Where(x => x.ColaboradorId.Equals(colaboradorId))
                .FirstOrDefault();

            colaborador.FuncaoId = colaboradorModel.FuncaoId;
            colaborador.SituacaoColaboradorId = colaborador.SituacaoColaboradorId;
            colaborador.AlteracaoData = DateTime.UtcNow;

            this.pessoaService.Alterar(colaborador.PessoaId, colaboradorModel.Pessoa);

            this.context.SaveChanges();


        }

       public void Delete (Guid colaboradorId)
        {
            var colaborador = this.context.Colaborador
                .Where(x => x.ColaboradorId.Equals(colaboradorId))
                .FirstOrDefault();

            this.context.Colaborador.Remove(colaborador);

            this.pessoaService.Delete(colaborador.PessoaId);

            this.context.SaveChanges();
        }
    }
}