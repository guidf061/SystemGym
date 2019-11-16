using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class Country
    {
        public Country()
        {
            Pessoa = new HashSet<Pessoa>();
            State = new HashSet<State>();
        }

        public int CountryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pessoa> Pessoa { get; set; }
        public virtual ICollection<State> State { get; set; }
    }
}
