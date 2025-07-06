using Emdad.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Emdad.Areas.Admin.ViewModels
{
    public class AdminManageViewModel
    {
        public List<Admins> Admins { get; set; }
        public List<Sectors> AllSectors { get; set; }
        public List<PublicSubmission> ListPublicSubmission { get; set; }
        public List<FeedbackAndSuggestion> ListFeedbackAndSuggestion { get; set; }
    }

}