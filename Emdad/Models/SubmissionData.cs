using System;
using System.Collections.Generic;

namespace Emdad.Models;

public partial class SubmissionData : BaseEntity
{
    public int SubmissionDataId { get; set; }

    public int? SubmissionId { get; set; }

    public int? FormFieldId { get; set; }

    public string SubmissionDataValue { get; set; }
    public virtual Submission Submission { get; set; }
    public virtual FormField FormField { get; set; }
}
