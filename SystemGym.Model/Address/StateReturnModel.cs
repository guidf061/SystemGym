using System;
using System.Collections.Generic;

namespace SystemGym.Model.Address
{
    public class StateReturnModel
    {

        public int StateId { get; set; }

        public int? CountryId { get; set; }

        public string Name { get; set; }

        public string Acronym { get; set; }
    }
}
