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
                .Include(x => x.Tipo)
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
                    TipoId = x.TipoId,
                    AlteracaoData = x.AlteracaoData,
                    CriacaoData = x.CriacaoData
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
                    SexoId = x.SexoId,
                    TipoId = x.TipoId,
                    AlteracaoData = x.AlteracaoData,
                    CriacaoData = x.CriacaoData

                }).FirstOrDefault();
        }



        public Guid Adicionar (PessoaBindingModel pessoaModel)
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
                TipoId = pessoaModel.TipoId,
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
            pessoa.TipoId = pessoaModel.TipoId;
            pessoa.AlteracaoData = DateTime.UtcNow;

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
