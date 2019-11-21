using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Pessoa;

namespace SystemGym.Service
{
    public class PessoaService
    {
        private readonly SystemGymContext context;
        public PessoaService(SystemGymContext context)
        {
            this.context = context;
        }

        public List<PessoaReturnModel> GetAll()
        {
            return this.context.Pessoa
                .Include(x => x.Sexo)
                .OrderBy(x => x.Nome)
                .ToList()
                .Select(x => new PessoaReturnModel()
                {
                    PessoaId = x.PessoaId,
                    Nome = x.Nome,
                    Endereco = x.Endereco,
                    Cpf = x.Cpf,
                    Email = x.Email,
                    TelefoneCasa = x.TelefoneCasa,
                    TelefoneCelular = x.TelefoneCelular,
                    SexoId = x.SexoId,
                    AlteracaoData = x.AlteracaoData,
                    CriacaoData = x.CriacaoData,
                    DataNascimento = x.DataNascimento
                })
                .ToList();
        }
        public PessoaReturnModel Get(Guid pessoaId)
        {
            return this.context.Pessoa
                .Where(x => x.PessoaId.Equals(pessoaId))
                .ToList()
                .Select(x => new PessoaReturnModel()
                {
                    PessoaId = x.PessoaId,
                    Nome = x.Nome,
                    Cpf = x.Cpf,
                    Email = x.Email,
                    Endereco = x.Endereco,
                    TelefoneCelular = x.TelefoneCelular,
                    TelefoneCasa = x.TelefoneCasa,
                    PermissaoId = x.PermissaoId,
                    SexoId = x.SexoId,
                    AlteracaoData = x.AlteracaoData,
                    CriacaoData = x.CriacaoData,
                    DataNascimento = x.DataNascimento

                }).FirstOrDefault();
        }



        public Guid Adicionar(PessoaBindingModel pessoaModel)
        {

            var pessoa = new Pessoa()
            {
                Nome = pessoaModel.Nome,
                Cpf = pessoaModel.Cpf,
                Email = pessoaModel.Email,
                TelefoneCelular = pessoaModel.TelefoneCelular,
                TelefoneCasa = pessoaModel.TelefoneCasa,
                Endereco = pessoaModel.Endereco,
                SexoId = pessoaModel.SexoId,
                PermissaoId = pessoaModel.PermissaoId,
                StateId = pessoaModel.StateId,
                CityId = pessoaModel.CityId,
                CountryId = 1058,
                DataNascimento = pessoaModel.DataNascimento,
                AlteracaoData = DateTime.UtcNow,
                CriacaoData = DateTime.UtcNow,

            };

            this.context.Pessoa.Add(pessoa);
            this.context.SaveChanges();

            return pessoa.PessoaId;
        }
        public void Alterar(Guid pessoaId, PessoaBindingModel pessoaModel)
        {
            var pessoa = new Pessoa()
            {
                PessoaId = pessoaId
            };

            this.context.Pessoa.Attach(pessoa);

            pessoa.Nome = pessoaModel.Nome;
            pessoa.Cpf = pessoaModel.Cpf;
            pessoa.Email = pessoaModel.Email;
            pessoa.TelefoneCasa = pessoaModel.TelefoneCasa;
            pessoa.TelefoneCelular = pessoaModel.TelefoneCelular;
            pessoa.Endereco = pessoaModel.Endereco;
            pessoa.SexoId = pessoaModel.SexoId;
            pessoa.PermissaoId = pessoaModel.PermissaoId;
            pessoa.AlteracaoData = DateTime.UtcNow;
            pessoa.DataNascimento = pessoaModel.DataNascimento;
            pessoa.CountryId = 1058;
            pessoa.StateId = pessoaModel.StateId;
            pessoa.CityId = pessoaModel.CityId;

            this.context.SaveChanges();

        }

        public void Delete(Guid pessoaId)
        {
            var pessoa = new Pessoa()
            {
                PessoaId = pessoaId
            };

            this.context.Pessoa.Attach(pessoa);

            this.context.Pessoa.Remove(pessoa);

            this.context.SaveChanges();
        }

        public void Save(Guid PessoaId, PessoaBindingModel pessoaBinding)
        {

        }
    }
}
