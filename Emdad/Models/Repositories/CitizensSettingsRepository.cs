
namespace Emdad.Models.Repositories
{
    public class CitizensSettingsRepository : IRepository<CitizensSettings>
    {
        public EmdadContext db { get; }
        public CitizensSettingsRepository(EmdadContext _db)
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

        public void Add(CitizensSettings entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            entity.CreateDate = DateTime.Now;
            entity.EditDate = DateTime.Now;
            db.CitizensSettings.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int Id, CitizensSettings entity)
        {
            var data = Find(Id);
            data.IsDelete = true;
            data.EditDate = DateTime.Now;
            db.Update(data);
            db.SaveChanges();
        }

        public CitizensSettings Find(int Id)
        {
            return db.CitizensSettings.SingleOrDefault(x => x.CitizensSettingsId == Id);
        }

        public void Update(int Id, CitizensSettings entity)
        {
            entity.EditDate = DateTime.Now;
            db.Update(entity);
            db.SaveChanges();
        }

        public List<CitizensSettings> View()
        {
            return db.CitizensSettings.Where(x => x.IsDelete == false).ToList();
        }

        public List<CitizensSettings> ViewClient()
        {
            return db.CitizensSettings.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
        }
    }
}
