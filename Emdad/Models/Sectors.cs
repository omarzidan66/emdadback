using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Emdad.Models;

public partial class Sectors : BaseEntity
{

    public int SectorsId { get; set; }

    public string SectorsName { get; set; } 

    public string SectorsLink { get; set; }
    public string SectorIcon { get; set; }
    public string SectorDesc { get; set; }
}
