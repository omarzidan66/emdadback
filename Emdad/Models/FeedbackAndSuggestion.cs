using System;
using System.Collections.Generic;

namespace Emdad.Models;

public partial class FeedbackAndSuggestion : BaseEntity
{
    public int FeedbackAndSuggestionId { get; set; }

    public string CitizenNationalId { get; set; }

    public string CitizenFullName { get; set; }

    public string CitizenEmail { get; set; }

    public string FeedbackAndSuggestionsPhone { get; set; }

    public string FeedbackAndSuggestionsLocation { get; set; }

    public string FeedbackAndSuggestionsSector { get; set; }

    public string FeedbackAndSuggestionsDescription { get; set; }
    public SectorsServices SectorsServices { get; set; }
    public Citizen Citizen { get; set; }
}
