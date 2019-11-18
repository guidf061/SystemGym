using System;
using System.Collections.Generic;
using System.Text;

namespace SystemGym.Model
{
    public class PagingModel<T>
    {
        public int TotalItems { get; set; }

        public List<T> Items { get; set; }
    }
}
