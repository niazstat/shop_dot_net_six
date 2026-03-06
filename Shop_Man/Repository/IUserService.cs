
using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
  public  interface IUserService
    {
        AuthenticateResponse Authenticate(User model);
        IEnumerable<User> GetAll();
        IQueryable<User> Users { get; }
       User GetById(int id);
        User AuthenticateUser(AuthenticateRequest authenticateRequest);

        bool IsControllerAndActionPermitted(string controller,string action,User user);



        IEnumerable<UserToCompany> GetAllUserToCompany { get; }
        Company GetCompanyByUser(int userID);
        Company GetCompanyById(int comID);
        ResultObj UpdateCompany(Company model);
        ResultObj AddUser(User model,UserToCompany comp);

        ResultObj ADDPermission(List<PermittedController> permissionList,int _userid);
    }
}
