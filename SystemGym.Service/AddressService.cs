using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SystemGym.DataAccess.Models;
using SystemGym.Model.Address;

namespace SystemGym.Service
{
    public class AddressService
    {
        private readonly SystemGymContext context;

        public AddressService(SystemGymContext context)
        {
            this.context = context;
        }

        public List<CityReturnModel> GetCity(CitySearchModel searchModel)
        {
            var query = this.context.City
                  .Include(x => x.State)
                  .AsQueryable();

            if (searchModel.CityId.HasValue)
            {
                query = query.Where(x => x.CityId.Equals(searchModel.CityId));
            }

            if (searchModel.StateId.HasValue)
            {
                query = query.Where(x => x.StateId.Equals(searchModel.StateId));
            }

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(x => EF.Functions.Like(x.Name, "%" + searchModel.Name + "%"));
            }

            return query
                .ToList()
                .Select(x => new CityReturnModel() {
                    CityId = x.CityId,
                    Name = x.Name,
                    StateId = x.StateId,
                    State = new StateReturnModel()
                    {
                        StateId = x.State.StateId,
                        CountryId = x.State.CountryId,
                        Acronym = x.State.Acronym,
                        Name  = x.State.Name,
                    }
                })
                .ToList();
        }

        public List<StateReturnModel> GetState()
        {
            return this.context.State
               .ToList()
               .Select(x => new StateReturnModel() {
                   StateId = x.StateId,
                   CountryId = x.CountryId,
                   Acronym = x.Acronym,
                   Name = x.Name,
               })
               .ToList();
        }
    }
}
