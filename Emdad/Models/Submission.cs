using System;
using System.Collections.Generic;

namespace Emdad.Models;

public partial class Submission : BaseEntity
{
    public int SubmissionId { get; set; }

    public int CitizenId { get; set; }

    public int SectorsServicesId { get; set; }

    public string SubmissionStatus { get; set; }
    public virtual Citizen Citizen { get; set; }
    public  virtual SectorsServices SectorsServices { get; set; }
    public virtual ICollection<SubmissionData> SubmissionData { get; set; }
}
