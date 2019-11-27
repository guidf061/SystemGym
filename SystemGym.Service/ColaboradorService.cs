using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemGym.DataAccess.Models;
using SystemGym.Model;
using SystemGym.Model.Address;
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
                        PermissaoId = x.Pessoa.PermissaoId,
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
                        PermissaoId = x.Pessoa.PermissaoId,
                        AlteracaoData = x.Pessoa.AlteracaoData,
                        CriacaoData = x.Pessoa.CriacaoData,
                    }
                })
                .FirstOrDefault();
        }

        public async Task<PagingModel<ColaboradorReturnModel>> SearchAsync(ColaboradorSearchModel model)
        {
            var query = this.context.Colaborador
                .Include(x => x.Pessoa)
                    .ThenInclude(x => x.City)
                .AsQueryable();

            if (!string.IsNullOrEmpty(model.Nome))
            {
                query = query.Where(x => EF.Functions.Like(x.Pessoa.Nome, "%" + model.Nome + "%"));
            }

            if (!string.IsNullOrEmpty(model.NumeroCtps))
            {
                query = query.Where(x => EF.Functions.Like(x.NumeroCtps, "%" + model.NumeroCtps + "%"));
            }

            var pagingModel = new PagingModel<ColaboradorReturnModel>();

            pagingModel.TotalItems = query.Count();

            if (!string.IsNullOrEmpty(model.Sort))
            {
                query = query.OrderBy(x => x.Pessoa.Nome);
            }

            pagingModel.Items = (await query
                .Skip(model.PageSize * (model.Page - 1))
                .Take(model.PageSize)
                .ToListAsync())
                .Select(x => new ColaboradorReturnModel()
                {
                    ColaboradorId = x. ColaboradorId,
                    FuncaoId = x.FuncaoId,
                    SituacaoColaboradorId = x.SituacaoColaboradorId,
                    DocIdentidade = x.DocIdentidade,
                    NumeroCtps = x.NumeroCtps,
                    NumeroPisPasep = x.NumeroPisPasep,
                    NumeroSerieCtps = x.NumeroSerieCtps,
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
                        DataNascimento = x.Pessoa.DataNascimento,
                        AlteracaoData = x.Pessoa.AlteracaoData,
                        CriacaoData = x.Pessoa.CriacaoData,
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
        public void Adicionar(ColaboradorBindingModel model)
        {
            using (var transaction = this.context.Database.BeginTransaction())
            {

                try
                {
                    var colaborador = new Colaborador()
                    {
                        FuncaoId = model.FuncaoId,
                        SituacaoColaboradorId = model.SituacaoColaboradorId,
                        DocIdentidade = model.DocIdentidade,
                        NumeroSerieCtps = model.NumeroSerieCtps,
                        NumeroPisPasep = model.NumeroPisPasep,
                        NumeroCtps = model.NumeroCtps,
                        CriacaoData = DateTime.UtcNow,
                        AlteracaoData = DateTime.UtcNow,
                        PessoaId = pessoaService.Adicionar(model.Pessoa)
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

        public void Alterar (Guid colaboradorId, ColaboradorBindingModel model)
        {
            var colaborador = this.context.Colaborador
                .Where(x => x.ColaboradorId.Equals(colaboradorId))
                .FirstOrDefault();

            colaborador.FuncaoId = model.FuncaoId;
            colaborador.SituacaoColaboradorId = model.SituacaoColaboradorId;
            colaborador.NumeroCtps = model.NumeroCtps;
            colaborador.NumeroPisPasep = model.NumeroPisPasep;
            colaborador.NumeroSerieCtps = model.NumeroSerieCtps;
            colaborador.DocIdentidade = model.DocIdentidade;

            colaborador.AlteracaoData = DateTime.UtcNow;

            this.pessoaService.Alterar(colaborador.PessoaId, model.Pessoa);

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