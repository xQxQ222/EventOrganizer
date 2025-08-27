using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelHolder.Dto
{
    [AutoConstructor]
    public partial class LocationDto
    {
        public decimal Latitude { get; }

        public decimal Longitude { get; }

        public string? Description { get; }
    }
}
