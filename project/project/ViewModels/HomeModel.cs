using project.Models;
using System.Collections.Generic;

namespace project.ViewModels
{
    public class HomeModel
    {
        public IList<Car> MostCarResearch { get; set; } 
        public IList<Car> ListCar { get; set; } 
        public IList<Lastnewsupdate> lastNew { get; set; } 
        public CarModel Car { get; set; }
        public CallBack CallBack { get; set; }

        public IList<CallBack> lstCallBack { get; set; }
        public Contact Contact { get; set; }
    }
}
