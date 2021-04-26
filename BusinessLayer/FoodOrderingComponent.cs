using BusinessLayer.Interface;
using DataAccessLayer.Interface;
using mvcEntities.CustomModel;
using mvcEntities.Entities;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class FoodOrderingComponent:IFoodOrderingComponent
    {
        private readonly IFoodDataAccess _foodDataAccess;
        public FoodOrderingComponent(IFoodDataAccess foodDataAccess)
        {
            _foodDataAccess = foodDataAccess;
        }

        public List<CustomMenu> GetCategory()
        {
            return _foodDataAccess.GetCategory();
        }

        public System.Collections.Generic.List<MenuList> GetMenu()
        {
            return _foodDataAccess.GetMenu();
        }

        public MenuList GetMenuById(long id)
        {
            return _foodDataAccess.GetMenuById(id);
        }

        public List<UserRole> GetRole()
        {
            return _foodDataAccess.GetRole();
        }

        public bool Login(Login login)
        {
            return _foodDataAccess.Login(login);
        }

        public int AddPayment(Payment pay)
        {
            return _foodDataAccess.AddPayment(pay);
        }

        public int Register(Registration res)
        {
            return _foodDataAccess.Register(res);
        }

        public int ReserveTable(Reservation res)
        {
            return _foodDataAccess.ReserveTable(res);
        }

        public List<PaymentCustom> GetPrice()
        {
            return _foodDataAccess.GetPrice();
        }

        public List<ModeOfPayment> GetPaymentMode()
        {
            return _foodDataAccess.GetPaymentMode();
        }

        public List<OrderHistoryCustom> GetPastOrders()
        {
            return _foodDataAccess.GetPastOrders();
        }

        public Registration GetUserDetailByUserName(string userName)
        {
            return _foodDataAccess.GetUserDetailByUserName(userName);
        }

        public MenuList GetMenuId(long id)
        {
            return _foodDataAccess.GetMenuId(id);
        }

        public int AddOrder(OrderHistory order)
        {
            return _foodDataAccess.AddOrder(order);
        }
    }
}
