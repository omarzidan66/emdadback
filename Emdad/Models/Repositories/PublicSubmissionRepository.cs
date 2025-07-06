
namespace Emdad.Models.Repositories
{
    public class PublicSubmissionRepository : IRepository<PublicSubmission>
    {
        public EmdadContext db { get; }
        public PublicSubmissionRepository(EmdadContext _db)
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

        public void Add(PublicSubmission entity)
        {
            entity.IsActive = false;
            entity.IsDelete = false;
            entity.CreateDate = DateTime.Now;
            entity.EditDate = DateTime.Now;
            db.PublicSubmissions.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int Id, PublicSubmission entity)
        {
            var data = Find(Id);
            data.IsDelete = true;
            data.EditDate = DateTime.Now;
            db.Update(data);
            db.SaveChanges();
        }

        public PublicSubmission Find(int Id)
        {
            return db.PublicSubmissions.SingleOrDefault(x => x.PublicSubmissionId == Id);
        }

        public void Update(int Id, PublicSubmission entity)
        {
            entity.EditDate = DateTime.Now;
            db.Update(entity);
            db.SaveChanges();
        }

        public List<PublicSubmission> View()
        {
            return db.PublicSubmissions.Where(x => x.IsDelete == false).ToList();
        }

        public List<PublicSubmission> ViewClient()
        {
            return db.PublicSubmissions.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
        }
    }
}
