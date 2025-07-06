using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Emdad.Models;

public partial class EmdadContext : IdentityDbContext
{
    public EmdadContext()
    {
    }

    public EmdadContext(DbContextOptions<EmdadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FeedbackAndSuggestion> FeedbackAndSuggestions { get; set; }

    public virtual DbSet<FormField> FormFields { get; set; }

    public virtual DbSet<MySubmission> MySubmissions { get; set; }

    public virtual DbSet<PublicSubmission> PublicSubmissions { get; set; }

    public virtual DbSet<Sectors> Sectors { get; set; }

    public virtual DbSet<SectorsServices> SectorsServices { get; set; }

    public virtual DbSet<Submission> Submission { get; set; }

    public virtual DbSet<SubmissionData> SubmissionData { get; set; }
    public virtual DbSet<Citizen> Citizen { get; set; }
    public virtual DbSet<Admins> Admins { get; set; }
    public virtual DbSet<CitizensSettings> CitizensSettings { get; set; }

    
}
