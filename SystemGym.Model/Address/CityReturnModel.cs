using System;
using System.Collections.Generic;

namespace SystemGym.Model.Address
{
    public class CityReturnModel
    {
        public int CityId { get; set; }

        public int? StateId { get; set; }

        public string Name { get; set; }

        public StateReturnModel State { get; set; }
    }
}
