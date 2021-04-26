using DataAccessLayer.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using mvcEntities.CustomModel;
using mvcEntities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccessLayer
{
    public class FoodDataAccess: IFoodDataAccess
    {
        private readonly FoodOrderedDbContext _context;
        public FoodDataAccess(FoodOrderedDbContext context)
        {
            _context = context;
        }
        public List<MenuList> GetMenu()
        {
            return _context.MenuList.ToList();
        }
        public int ReserveTable(Reservation res)
        {
            _context.Reservation.Add(res);
            return _context.SaveChanges();
        }
        public int AddPayment(Payment pay)
        {
            _context.Payment.Add(pay);
            return _context.SaveChanges();
        }
        public int AddOrder(OrderHistory order)
        {
            _context.OrderHistory.Add(order);
            return _context.SaveChanges();
        }
        public int Register(Registration res)
        {
            Registration temp = _context.Registration.FirstOrDefault(x => x.UserName == res.UserName);
            if (temp != null)
            {
                return 2;
            }
            _context.Registration.Add(res);
            return _context.SaveChanges();

        }
        
        public bool Login(Login login)
        {
            return _context.Registration.Any(x => x.UserName == login.UserName && x.Password == login.Password);
        }
        public Registration GetUserDetailByUserName(string userName)
        {
            return _context.Registration.FirstOrDefault(x => x.UserName == userName);
        }
        public MenuList GetMenuId(long id)
        {
            return _context.MenuList.FirstOrDefault(x => x.MenuId == id);
        }
        public List<UserRole> GetRole()
        {
            return _context.UserRole.ToList() ;
        }
      
        public List<ModeOfPayment> GetPaymentMode()
        {
            return _context.ModeOfPayment.ToList();
        }
        public MenuList GetMenuById(long id)
        {
            return _context.MenuList.FirstOrDefault(e => e.MenuId == id);
        }
        public List<CustomMenu> GetCategory()
        {
            return _context.CustomMenu.FromSqlRaw("exec [dbo].[CategoryDetail]").ToList();
        }
        public List<OrderHistoryCustom> GetPastOrders()
        {
            return _context.OrderHistoryCustom.FromSqlRaw("exec [dbo].[PastOrders]").ToList();
        }
        public List<PaymentCustom> GetPrice()
        {
            var result= _context.PaymentCustom.FromSqlRaw("exec [dbo].[PaymentProc]").ToList();
            return result;
        }
        
    }
}
