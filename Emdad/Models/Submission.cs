using System;
using System.Collections.Generic;

namespace Emdad.Models;

public partial class Submission : BaseEntity
{
    public int SubmissionId { get; set; }
    public string SubmissionName { get; set; }


    public int? SectorsServicesId { get; set; }
    public string? SectorsServicesName { get; set; }
    public string? SectorsName { get; set; }
    public string? SectorsServicesType { get; set; }

    public string SubmissionStatus { get; set; }
    public int SectorsId { get; set; }

    public virtual SectorsServices SectorsServices { get; set; }
    public string? CitizenNationalId { get; set; }


    public virtual ICollection<SubmissionData> SubmissionData { get; set; }
}
