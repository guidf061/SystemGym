using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Pessoa;
using SystemGym.Model.Visitante;

namespace SystemGym.Service
{
    public class VisitanteService
    {
        private readonly SystemGymContext context;
        private readonly PessoaService pessoaService;
        public VisitanteService(SystemGymContext context, PessoaService pessoaService)
        {
            this.context = context;
            this.pessoaService = pessoaService;
        }

        public List<VisitanteReturnModel> GetAll()
        {
            return this.context.Visitante
                .Include(x => x.Pessoa)
                .OrderBy(x => x.Pessoa.Nome)
                .ToList()
                .Select(x => new VisitanteReturnModel()
                {

                  VisitanteId = x.VisitanteId,
                  DocIdentidade = x.DocIdentidade,
                  VisitaData = x.VisitaData,
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
        public VisitanteReturnModel Get(Guid visitanteId)
        {
            return this.context.Visitante
                .Include(x => x.Pessoa)
                .Where(x => x.VisitanteId.Equals(visitanteId))
                .ToList()
                .Select(x => new VisitanteReturnModel()
                {
                    VisitanteId = x.VisitanteId,
                    DocIdentidade = x.DocIdentidade,
                    VisitaData = x.VisitaData,
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

        public void Adicionar(VisitanteBindingModel visitanteModel)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {

                try
                {
                    var visitante = new Visitante()
                    {
                        DocIdentidade = visitanteModel.DocIdentidade,
                        VisitaData = DateTime.UtcNow,
                        CriacaoData = DateTime.UtcNow,
                        AlteracaoData =DateTime.UtcNow,
                        PessoaId = pessoaService.Adicionar(visitanteModel.Pessoa)
                    };

                    this.context.Visitante.Add(visitante);
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

        public void Alterar(Guid visitanteId, VisitanteBindingModel visitanteModel)
        {
            var visitante = this.context.Visitante
                .Where(x => x.VisitanteId.Equals(visitanteId))
                .FirstOrDefault();

            visitante.DocIdentidade = visitanteModel.DocIdentidade;
            visitante.VisitaData = DateTime.UtcNow;
            visitante.AlteracaoData = DateTime.UtcNow;

            this.pessoaService.Alterar(visitante.PessoaId, visitanteModel.Pessoa);

            this.context.SaveChanges();
        }
        public void Delete(Guid visitanteId)
        {
            var visitante = this.context.Visitante
                .Where(x => x.VisitanteId.Equals(visitanteId))
                .FirstOrDefault();


            this.context.Visitante.Remove(visitante);

            this.pessoaService.Delete(visitante.PessoaId);

            this.context.SaveChanges();

        }
    }
}

