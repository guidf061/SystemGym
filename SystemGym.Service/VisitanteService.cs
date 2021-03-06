using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemGym.DataAccess.Models;
using SystemGym.Model;
using SystemGym.Model.Address;
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

        public async Task<PagingModel<VisitanteReturnModel>> SearchAsync(VisitanteSearchModel model)
        {
            var query = this.context.Visitante
                .Include(x => x.Pessoa)
                    .ThenInclude(x => x.City)

                .AsQueryable();

            if (!string.IsNullOrEmpty(model.Nome))
            {
                query = query.Where(x => EF.Functions.Like(x.Pessoa.Nome, "%" + model.Nome + "%"));
            }

            if (!string.IsNullOrEmpty(model.DocIdentidade))
            {
                query = query.Where(x => EF.Functions.Like(x.DocIdentidade, "%" + model.DocIdentidade + "%"));
            }

            var pagingModel = new PagingModel<VisitanteReturnModel>();

            pagingModel.TotalItems = query.Count();

            if (!string.IsNullOrEmpty(model.Sort))
            {
                query = query.OrderBy(x => x.Pessoa.Nome);
            }

            pagingModel.Items = (await query
                .Skip(model.PageSize * (model.Page - 1))
                .Take(model.PageSize)
                .ToListAsync())
                .Select(x => new VisitanteReturnModel()
                {   VisitanteId = x.VisitanteId,
                    DocIdentidade = x.DocIdentidade,
                    VisitaData = x.VisitaData,
                    AlteracaoData = x.AlteracaoData,
                    CriacaoData = x.CriacaoData,
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
                        PermissaoId = x.Pessoa.PermissaoId,
                        AlteracaoData = x.Pessoa.AlteracaoData,
                        CriacaoData = x.Pessoa.CriacaoData,
                        DataNascimento = x.Pessoa.DataNascimento,
                        City = x.Pessoa.City == null ? null : new CityReturnModel()
                        {
                            CityId = x.Pessoa.City.CityId,
                            Name = x.Pessoa.City.Name,
                            StateId = x.Pessoa.City.StateId,
                            State = x.Pessoa.City.State == null ? null : new StateReturnModel()
                            {
                                StateId = x.Pessoa.City.State.StateId,
                                Acronym = x.Pessoa.City.State.Acronym,
                                Name = x.Pessoa.City.State.Name,
                                CountryId = x.Pessoa.City.State.CountryId,
                            }
                        }
                    }
                }).ToList();

            return pagingModel;
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
                        PermissaoId = x.Pessoa.PermissaoId,
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
                        PermissaoId = x.Pessoa.PermissaoId,
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

