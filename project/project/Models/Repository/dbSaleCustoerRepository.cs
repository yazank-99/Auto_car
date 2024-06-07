using project.Data;
using System.Collections.Generic;
using System.Linq;

namespace project.Models.Repository
{
    public class dbSaleCustoerRepository : IRepository<SaleCustoer>
    {
        public AppDbContext AppDbContext { get; }

        public dbSaleCustoerRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }
        public void Active(int Id, SaleCustoer entity)
        {
            SaleCustoer data = Find(Id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public void Add(SaleCustoer entity)
        {
            AppDbContext.Add(entity);
            AppDbContext.SaveChanges();
        }

        public void Delete(int Id, SaleCustoer entity)
        {
            SaleCustoer data = Find(Id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public SaleCustoer Find(int Id)
        {
            return AppDbContext.SaleCustoer.SingleOrDefault(data => data.SaleId == Id);
        }

        public void Update(int Id, SaleCustoer entity)
        {
            AppDbContext.SaleCustoer.Update(entity);
            AppDbContext.SaveChanges();
        }

        public IList<SaleCustoer> View()
        {
            return AppDbContext.SaleCustoer
                                                            .Where(data => !data.IsDelete)
                                                            .ToList();
        }

        public IList<SaleCustoer> ViewFrontClinet()
        {
            return AppDbContext.SaleCustoer
                                                  .Where(data => !data.IsDelete && data.IsActive)
                                                  .ToList();
        }
    }
}
