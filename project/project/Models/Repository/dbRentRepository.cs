using Microsoft.EntityFrameworkCore;
using project.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace project.Models.Repository
{
    public class dbRentRepository : IRepository<Rent>
    {
        public AppDbContext AppDbContext { get; }

        public dbRentRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }


        public void Active(int Id, Rent entity)
        {
            Rent data = Find(Id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public void Add(Rent entity)
        {
            AppDbContext.Add(entity);
            AppDbContext.SaveChanges();
        }

        public void Delete(int Id, Rent entity)
        {
            Rent data = Find(Id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public Rent Find(int Id)
        {
            return AppDbContext.Rent.SingleOrDefault(data => data.RentId == Id);


        }

        public void Update(int Id, Rent entity)
        {
            AppDbContext.Rent.Update(entity);
            AppDbContext.SaveChanges();
        }

        public IList<Rent> View()
        {
            return AppDbContext.Rent.Include(data => data.AppUser).Include(data => data.Car)
                                                            .Where(data => !data.IsDelete)
                                                            .ToList();
        }

        public IList<Rent> ViewFrontClinet()
        {
            return AppDbContext.Rent.Include(data => data.AppUser).Include(data => data.Car)
                                                .Where(data => !data.IsDelete && data.IsActive)
                                                .ToList();
        }
    }
}
