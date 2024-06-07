using Microsoft.EntityFrameworkCore;
using project.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace project.Models.Repository
{
    public class dbSaleRepository : IRepository<Sale>
    {
        public AppDbContext AppDbContext { get; }

        public dbSaleRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public void Active(int Id, Sale entity)
        {
            Sale data = Find(Id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public void Add(Sale entity)
        {
            AppDbContext.Add(entity);
            AppDbContext.SaveChanges();
        }

        public void Delete(int Id, Sale entity)
        {
            Sale data = Find(Id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public Sale Find(int Id)
        {
            return AppDbContext.Sale.SingleOrDefault(data => data.SaleId == Id);


        }

        public void Update(int Id, Sale entity)
        {
            AppDbContext.Sale.Update(entity);
            AppDbContext.SaveChanges();
        }

        public IList<Sale> View()
        {
            return AppDbContext.Sale.Include(data => data.AppUser).Include(data => data.Car)
                                                  .Where(data => !data.IsDelete)
                                                  .ToList();
        }

        public IList<Sale> ViewFrontClinet()
        {
            return AppDbContext.Sale.Include(data => data.AppUser).Include(data => data.Car)
                                                   .Where(data => !data.IsDelete && data.IsActive)
                                                   .ToList();
        }
    }
}
