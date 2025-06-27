using System;
using System.Collections.Generic;

namespace Emdad.Models;

public partial class Sectors : BaseEntity
{
    public int SectorsId { get; set; }

    public string SectorsName { get; set; } = null!;

    public string SectorsLink { get; set; } = null!;
}
