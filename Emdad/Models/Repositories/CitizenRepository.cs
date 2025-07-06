
using System;

namespace Emdad.Models.Repositories
{
    public class CitizenRepository : IRepository<Citizen>
    {
        public EmdadContext db { get; }

        public CitizenRepository(EmdadContext _db)
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

        public void Add(Citizen entity)
        {
            entity.IsActive = true;
            entity.IsDelete = false;
            entity.CreateDate = DateTime.Now;
            entity.EditDate = DateTime.Now;
            db.Citizen.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int Id, Citizen entity)
        {
            var data = Find(Id);
            data.IsDelete = true;
            data.EditDate = DateTime.Now;
            db.Update(data);
            db.SaveChanges();
        }

        public Citizen Find(int Id)
        {
            return db.Citizen.SingleOrDefault(x => x.CitizenId == Id);
        }

        public void Update(int Id, Citizen entity)
        {
            entity.EditDate = DateTime.Now;
            db.Update(entity);
            db.SaveChanges();
        }


        //Admin
        public List<Citizen> View()
        {
            return db.Citizen.Where(x => x.IsDelete == false).ToList();
        }

        //Client
        public List<Citizen> ViewClient()
        {
            return db.Citizen.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
        }
    }
}
