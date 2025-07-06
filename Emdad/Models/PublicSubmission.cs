using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Emdad.Models;

public partial class PublicSubmission : BaseEntity
{
    public int PublicSubmissionId { get; set; }

    public string CitizenNationalId { get; set; }

    public string PublicSubmissionFullName { get; set; }

    public string CitizenEmail { get; set; }

    public string PublicSubmissionPhone { get; set; }

    public string PublicSubmissionLocation { get; set; }

    public string PublicSubmissionDescription { get; set; }
    public int? AssignedSectorId { get; set; }

    [ForeignKey("AssignedSectorId")]
    public Sectors Sector { get; set; }
}
