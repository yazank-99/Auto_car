using project.Data;
using System.Collections.Generic;
using System.Linq;

namespace project.Models.Repository
{
    public class dbCarShopRepository : IRepository<CarShop>
    {
        public AppDbContext AppDbContext { get; }

        public dbCarShopRepository(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }
        public void Active(int Id, CarShop entity)
        {
            CarShop data = Find(Id);
            data.IsActive = !data.IsActive;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public void Add(CarShop entity)
        {
            AppDbContext.Add(entity);
            AppDbContext.SaveChanges();
        }

        public void Delete(int Id, CarShop entity)
        {
            CarShop data = Find(Id);
            data.IsDelete = true;
            data.EditUser = entity.EditUser;
            data.EditDate = entity.EditDate;
            Update(Id, data);
        }

        public CarShop Find(int Id)
        {
            return AppDbContext.CarShop.SingleOrDefault(data => data.CarShopId == Id);
        }

        public void Update(int Id, CarShop entity)
        {

            AppDbContext.CarShop.Update(entity);
            AppDbContext.SaveChanges();
        }

        public IList<CarShop> View()
        {
            return AppDbContext.CarShop
                                                             .Where(data => !data.IsDelete)
                                                             .ToList();
        }

        public IList<CarShop> ViewFrontClinet()
        {
            return AppDbContext.CarShop
                                                              .Where(data => !data.IsDelete && data.IsActive)
                                                              .ToList();
        }
    }
}
