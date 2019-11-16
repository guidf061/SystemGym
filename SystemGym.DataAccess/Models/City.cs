using System;
using System.Collections.Generic;

namespace SystemGym.DataAccess.Models
{
    public partial class City
    {
        public City()
        {
            Pessoa = new HashSet<Pessoa>();
        }

        public int CityId { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<Pessoa> Pessoa { get; set; }
    }
}
