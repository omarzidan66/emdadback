namespace Emdad.Models.Repositories
{
    public class SectorsServicesRepository : IRepository<SectorsServices>
    {
        public EmdadContext db { get; }
        public SectorsServicesRepository(EmdadContext _db)
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

        public void Add(SectorsServices entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            entity.CreateDate = DateTime.Now;
            entity.EditDate = DateTime.Now;
            db.SectorsServices.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int Id, SectorsServices entity)
        {
            var data = Find(Id);
            data.IsDelete = true;
            data.EditDate = DateTime.Now;
            db.Update(data);
            db.SaveChanges();
        }

        public SectorsServices Find(int Id)
        {
            return db.SectorsServices.SingleOrDefault(x => x.SectorsServicesId == Id);
        }

        public void Update(int Id, SectorsServices entity)
        {
            entity.EditDate = DateTime.Now;
            db.Update(entity);
            db.SaveChanges();
        }

        public List<SectorsServices> View()
        {
            return db.SectorsServices.Where(x => x.IsDelete == false).ToList();
        }

        public List<SectorsServices> ViewClient()
        {
            return db.SectorsServices.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
        }
    }
}
