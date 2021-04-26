using mvcEntities.CustomModel;
using mvcEntities.Entities;
using System.Collections.Generic;

namespace DataAccessLayer.Interface
{
    public interface IFoodDataAccess
    {
        List<MenuList> GetMenu();
        int ReserveTable(Reservation res);
        int Register(Registration res);
        bool Login(Login login);
        Registration GetUserDetailByUserName(string userName);
        List<UserRole> GetRole();
        MenuList GetMenuById(long id);
        List<CustomMenu> GetCategory();
        int AddPayment(Payment pay);
        List<PaymentCustom> GetPrice();
        List<ModeOfPayment> GetPaymentMode();
        List<OrderHistoryCustom> GetPastOrders();
        MenuList GetMenuId(long id);
        int AddOrder(OrderHistory order);

    }
}
