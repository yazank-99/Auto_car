using project.Data;
using System.Collections.Generic;
using System.Linq;

namespace project.Models.Repository
{
    public class dbRentCustomerRepository : IRepository<RentCustomer>
    {
        public AppDbContext AppDbContext { get; }

        public dbRentCustomerRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public void Active(int Id, RentCustomer entity)
        {
            RentCustomer data = Find(Id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public void Add(RentCustomer entity)
        {

            AppDbContext.Add(entity);
            AppDbContext.SaveChanges();
        }

        public void Delete(int Id, RentCustomer entity)
        {
            RentCustomer data = Find(Id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public RentCustomer Find(int Id)
        {
            return AppDbContext.RentCustomer.SingleOrDefault(data => data.RentId == Id);
        }

        public void Update(int Id, RentCustomer entity)
        {
            AppDbContext.RentCustomer.Update(entity);
            AppDbContext.SaveChanges();
        }

        public IList<RentCustomer> View()
        {
            return AppDbContext.RentCustomer
                                                            .Where(data => !data.IsDelete)
                                                            .ToList();
        }

        public IList<RentCustomer> ViewFrontClinet()
        {
            return AppDbContext.RentCustomer
                                                 .Where(data => !data.IsDelete && data.IsActive)
                                                 .ToList();
        }
    }
}
