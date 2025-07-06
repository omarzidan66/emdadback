using Emdad.Models;

namespace Emdad.ViewModels
{
    public class HomeModel
    {
        public List<Models.SectorsServices> ListSectorsServices { get; set; }
        public List<Models.Sectors> ListSectors { get; set; }
        public List<Models.Submission> ListSubmission { get; set; }
        public PublicSubmission PublicSubmission { get; set; }
        public FeedbackAndSuggestion FeedbackAndSuggestion { get; set; }

        public List<Models.Citizen> ListCitizen { get; set; }
        public CitizensSettings CitizensSettings { get; set; }


    }

}
