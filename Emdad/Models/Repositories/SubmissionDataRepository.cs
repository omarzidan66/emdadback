
namespace Emdad.Models.Repositories
{
    public class SubmissionDataRepository : IRepository<SubmissionData>
    {
        public EmdadContext db { get; }
        public SubmissionDataRepository(EmdadContext _db)
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

        public void Add(SubmissionData entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            entity.CreateDate = DateTime.Now;
            entity.EditDate = DateTime.Now;
            db.SubmissionData.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int Id, SubmissionData entity)
        {
            var data = Find(Id);
            data.IsDelete = true;
            data.EditDate = DateTime.Now;
            db.Update(data);
            db.SaveChanges();
        }

        public SubmissionData Find(int Id)
        {
            return db.SubmissionData.SingleOrDefault(x => x.SubmissionDataId == Id);
        }

        public void Update(int Id, SubmissionData entity)
        {
            entity.EditDate = DateTime.Now;
            db.Update(entity);
            db.SaveChanges();
        }

        public List<SubmissionData> View()
        {
            return db.SubmissionData.Where(x => x.IsDelete == false).ToList();
        }

        public List<SubmissionData> ViewClient()
        {
            return db.SubmissionData.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
        }
    }
}
