using project.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace project.Models.Repository
{
    public class dbLastnewsupdateRepository : IRepository<Lastnewsupdate>
    {
        public AppDbContext AppDbContext { get; }

        public dbLastnewsupdateRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }


        public void Active(int Id, Lastnewsupdate entity)
        {
            Lastnewsupdate data = Find(Id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public void Add(Lastnewsupdate entity)
        {
            AppDbContext.Add(entity);
            AppDbContext.SaveChanges();
        }

        public void Delete(int Id, Lastnewsupdate entity)
        {
            Lastnewsupdate data = Find(Id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public Lastnewsupdate Find(int Id)
        {
            return AppDbContext.Lastnewsupdate.SingleOrDefault(data => data.LastnewsupdateId == Id);

        }

        public void Update(int Id, Lastnewsupdate entity)
        {
            AppDbContext.Lastnewsupdate.Update(entity);
            AppDbContext.SaveChanges();
        }

        public IList<Lastnewsupdate> View()
        {
            return AppDbContext.Lastnewsupdate
                                                  .Where(data => !data.IsDelete)
                                                  .ToList();
        }

        public IList<Lastnewsupdate> ViewFrontClinet()
        {
            return AppDbContext.Lastnewsupdate
                                                  .Where(data => !data.IsDelete && data.IsActive)
                                                  .ToList();
        }
    }
}
