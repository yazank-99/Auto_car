    using project.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace project.Models.Repository
{
    public class dbCallBackRepository : IRepository<CallBack>
    {

        public dbCallBackRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public AppDbContext AppDbContext { get; }

        public void Active(int Id, CallBack entity)
        {
            CallBack data = Find(Id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public void Add(CallBack entity)
        {
            AppDbContext.Add(entity);
            AppDbContext.SaveChanges();
        }

        public void Delete(int Id, CallBack entity)
        {
            CallBack data = Find(Id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public CallBack Find(int Id)
        {
            return AppDbContext.CallBack.SingleOrDefault(data => data.CallBackId == Id);

        }

        public void Update(int Id, CallBack entity)
        {
            AppDbContext.CallBack.Update(entity);
            AppDbContext.SaveChanges();
        }

        public IList<CallBack> View()
        {
            return AppDbContext.CallBack
                                      .Where(data => !data.IsDelete)
                                      .ToList();
        }

        public IList<CallBack> ViewFrontClinet()
        {
            return AppDbContext.CallBack
                                       .Where(data => !data.IsDelete)
                                       .ToList();
        }
    }
}
