
namespace Emdad.Models.Repositories
{
    public class SubmissionRepository : IRepository<Submission>
    {
        public EmdadContext db { get; }
        public SubmissionRepository(EmdadContext _db)
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

        public void Add(Submission entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            entity.CreateDate = DateTime.Now;
            entity.EditDate = DateTime.Now;
            db.Submission.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int Id, Submission entity)
        {
            var data = Find(Id);
            data.IsDelete = true;
            data.EditDate = DateTime.Now;
            db.Update(data);
            db.SaveChanges();
        }

        public Submission Find(int Id)
        {
            return db.Submission.SingleOrDefault(x => x.SubmissionId == Id);
        }

        public void Update(int Id, Submission entity)
        {
            entity.EditDate = DateTime.Now;
            db.Update(entity);
            db.SaveChanges();
        }

        public List<Submission> View()
        {
            return db.Submission.Where(x => x.IsDelete == false).ToList();
        }

        public List<Submission> ViewClient()
        {
            return db.Submission.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
        }
    }
}
