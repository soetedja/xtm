using System;
using System.Collections.Generic;

namespace X.Domain
{
    public class Country: BaseEntity
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
