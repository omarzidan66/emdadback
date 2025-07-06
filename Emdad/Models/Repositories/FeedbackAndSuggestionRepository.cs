
namespace Emdad.Models.Repositories
{
    public class FeedbackAndSuggestionRepository : IRepository<FeedbackAndSuggestion>
    {
        public EmdadContext db { get; }
        public FeedbackAndSuggestionRepository(EmdadContext _db)
        {
            db = _db;
        }
        public void Active(int Id)
        {
            var data = Find(Id);
            data.IsActive = !data.IsActive;
            data.EditDate = DateTime.Now;
            db.Update(data);
            db.SaveChanges();
        }

        public void Add(FeedbackAndSuggestion entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            entity.CreateDate = DateTime.Now;
            entity.EditDate = DateTime.Now;
            db.FeedbackAndSuggestions.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int Id, FeedbackAndSuggestion entity)
        {
            var data = Find(Id);
            data.IsDelete = true;
            data.EditDate = DateTime.Now;
            db.Update(data);
            db.SaveChanges();
        }

        public FeedbackAndSuggestion Find(int Id)
        {
            return db.FeedbackAndSuggestions.SingleOrDefault(x => x.FeedbackAndSuggestionId == Id);
        }

        public void Update(int Id, FeedbackAndSuggestion entity)
        {
            entity.EditDate = DateTime.Now;
            db.Update(entity);
            db.SaveChanges();
        }

        public List<FeedbackAndSuggestion> View()
        {
            return db.FeedbackAndSuggestions.Where(x => x.IsDelete == false).ToList();
        }

        public List<FeedbackAndSuggestion> ViewClient()
        {
            return db.FeedbackAndSuggestions.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
        }
    }
}
