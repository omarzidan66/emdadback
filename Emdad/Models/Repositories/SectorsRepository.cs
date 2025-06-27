
namespace Emdad.Models.Repositories
{
    public class SectorsRepository : IRepository<Sectors>
    {
        public EmdadContext db { get; }
        public SectorsRepository(EmdadContext _db)
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

        public void Add(Sectors entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            entity.CreateDate = DateTime.Now;
            entity.EditDate = DateTime.Now;
            db.Sectors.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int Id, Sectors entity)
        {
            var data = Find(Id);
            data.IsDelete = true;
            data.EditDate = DateTime.Now;
            db.Update(data);
            db.SaveChanges();
        }

        public Sectors Find(int Id)
        {
            return db.Sectors.SingleOrDefault(x => x.SectorsId == Id);
        }

        public void Update(int Id, Sectors entity)
        {
            entity.EditDate = DateTime.Now;
            db.Update(entity);
            db.SaveChanges();
        }

        public List<Sectors> View()
        {
            return db.Sectors.Where(x => x.IsDelete == false).ToList();
        }

        public List<Sectors> ViewClient()
        {
            return db.Sectors.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
        }
    }
}
