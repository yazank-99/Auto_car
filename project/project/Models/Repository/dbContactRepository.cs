using project.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace project.Models.Repository
{
    public class dbContactRepository : IRepository<Contact>
    {
        public AppDbContext AppDbContext { get; }

        public dbContactRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }


        public void Active(int Id, Contact entity)
        {

            Contact data = Find(Id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public void Add(Contact entity)
        {
            AppDbContext.Add(entity);
            AppDbContext.SaveChanges();
        }

        public void Delete(int Id, Contact entity)
        {
            Contact data = Find(Id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public Contact Find(int Id)
        {
            return AppDbContext.Contact.SingleOrDefault(data => data.ContactId == Id);

        }

        public void Update(int Id, Contact entity)
        {
            AppDbContext.Contact.Update(entity);
            AppDbContext.SaveChanges();
        }

        public IList<Contact> View()
        {
            return AppDbContext.Contact
                                                 .Where(data => !data.IsDelete)
                                                 .ToList();
        }

        public IList<Contact> ViewFrontClinet()
        {
            return AppDbContext.Contact
                                                   .Where(data => !data.IsDelete && data.IsActive)
                                                   .ToList();
        }
    }
}
