using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class State
    {
        public State()
        {
            City = new HashSet<City>();
            Pessoa = new HashSet<Pessoa>();
        }

        public int StateId { get; set; }
        public int? CountryId { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Pessoa> Pessoa { get; set; }
    }
}
