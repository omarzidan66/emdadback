using System;
using System.Collections.Generic;

namespace Emdad.Models;

public partial class PublicSubmission : BaseEntity
{
    public int PublicSubmissionId { get; set; }
    public int CitizenId { get; set; }

    public string CitizenNationalId { get; set; }

    public string PublicSubmissionFullName { get; set; }

    public string CitizenEmail { get; set; }

    public string PublicSubmissionPhone { get; set; }

    public string PublicSubmissionLocation { get; set; }

    public string PublicSubmissionDescription { get; set; }
    public Citizen Citizen { get; set; }
}
