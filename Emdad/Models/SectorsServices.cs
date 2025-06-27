using System;
using System.Collections.Generic;

namespace Emdad.Models;

public partial class SectorsServices : BaseEntity
{
    public int SectorsServicesId { get; set; }

    public string SectorsServicesName { get; set; } 

    public string SectorsServicesType { get; set; }

    public string SectorsServicesLink { get; set; } 

    public int SectorsId { get; set; }

    public string SectorsName { get; set; } 
    public Sectors Sectors { get; set; } = null!;
}
