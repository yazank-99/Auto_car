using Microsoft.EntityFrameworkCore;
using project.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace project.Models.Repository
{
    public class dbCarRepository : IRepository<Car>
    {
        public AppDbContext AppDbContext { get; }

        public dbCarRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }


        public void Active(int Id, Car entity)
        {
            Car data = Find(Id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public void Add(Car entity)
        {
            AppDbContext.Car.Add(entity);
            AppDbContext.SaveChanges();
        }

        public void Delete(int Id, Car entity)
        {
            Car data = Find(Id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public Car Find(int Id)
        {
            return AppDbContext.Car
                .SingleOrDefault(data => data.CarId == Id);

        }

        public void Update(int Id, Car entity)
        {
            AppDbContext.Car.Update(entity);
            AppDbContext.SaveChanges();
        }

        public IList<Car> View()
        {
            return AppDbContext.Car.Include(data => data.AppUser)
                .Where(data => !data.IsDelete)
                .ToList();
        }

        public IList<Car> ViewFrontClinet()
        {
            return AppDbContext.Car.Include(data => data.AppUser)
                .Where(data => !data.IsRent && !data.IsSaled && !data.IsDelete && data.IsActive)
                .ToList();
        }
    }
}
