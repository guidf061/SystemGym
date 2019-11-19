using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Address;
using SystemGym.Model.Permissao;
using SystemGym.Model.Sexo;


namespace SystemGym.Service
{
    public class CombosListService
    {
        private readonly SystemGymContext context;

        public CombosListService(SystemGymContext context)
        {
            this.context = context;
        }

        public List<SexoReturnModel> GetSexo()
        {
            return this.context.Sexo
               .ToList()
               .Select(x => new SexoReturnModel() {
                   SexoId = x.SexoId,
                   Descricao = x.Descricao,
               })
               .ToList();
        }
        public List<PermissaoReturnModel> GetPermissao()
        {
            return this.context.Permissao
               .ToList()
               .Select(x => new PermissaoReturnModel()
               {
                   PermissaoId = x.PermissaoId,
                   Descricao = x.Descricao,
               })
               .ToList();
        }
    }
}
