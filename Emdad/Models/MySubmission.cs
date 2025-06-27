using System;
using System.Collections.Generic;

namespace Emdad.Models;

public partial class MySubmission : BaseEntity
{
    public int MySubmissionId { get; set; }

    public int SectorsServicesId { get; set; }

    public string SectorsServicesName { get; set; }
    public SectorsServices SectorsServices { get; set; }
}
