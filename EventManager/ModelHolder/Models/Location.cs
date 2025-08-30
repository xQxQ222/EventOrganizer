using System;
using System.Collections.Generic;

namespace ModelHolder.Models;

[AutoConstructor]
public partial class Location
{

    public decimal Latitude { get; }

    public decimal Longitude { get;}
}
