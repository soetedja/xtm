using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace X.Model
{
    public class CountryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
