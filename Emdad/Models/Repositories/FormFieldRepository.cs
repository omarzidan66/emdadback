
namespace Emdad.Models.Repositories
{
    public class FormFieldRepository : IRepository<FormField>
    {
        public EmdadContext db { get; }
        public FormFieldRepository(EmdadContext _db)
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

        public void Add(FormField entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            entity.CreateDate = DateTime.Now;
            entity.EditDate = DateTime.Now;
            db.FormFields.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int Id, FormField entity)
        {
            var data = Find(Id);
            data.IsDelete = true;
            data.EditDate = DateTime.Now;
            db.Update(data);
            db.SaveChanges();
        }

        public FormField Find(int Id)
        {
            return db.FormFields.SingleOrDefault(x => x.FormFieldId == Id);
        }

        public void Update(int Id, FormField entity)
        {
            entity.EditDate = DateTime.Now;
            db.Update(entity);
            db.SaveChanges();
        }

        public List<FormField> View()
        {
            return db.FormFields.Where(x => x.IsDelete == false).ToList();
        }

        public List<FormField> ViewClient()
        {
            return db.FormFields.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
        }
    }
}
