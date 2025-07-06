using Emdad.Models;

namespace Emdad.Areas.Admin.ViewModels
{
    public class HomeModel
    {
        public List<Models.SectorsServices> ListSectorsServices { get; set; }
        public List<Models.Sectors> ListSectors { get; set; }
        public List<Models.Submission> ListSubmission { get; set; }
        public PublicSubmission PublicSubmission { get; set; }
        public FeedbackAndSuggestion FeedbackAndSuggestion { get; set; }

        public List<Models.Citizen> ListCitizen { get; set; }

        public List<Models.SubmissionData> ListSubmissionData { get; set; }
        public List<Models.FormField> ListFormField { get; set; }


    }

    public class SubmissionDetailViewModel
    {
        public int SubmissionId { get; set; }
        public List<SubmissionFieldViewModel> Fields { get; set; }
    }

    public class SubmissionFieldViewModel
    {
        public string Label { get; set; }
        public string Value { get; set; }
        public string Type { get; set; } // "text", "file", etc
    }
}
