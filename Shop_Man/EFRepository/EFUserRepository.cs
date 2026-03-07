using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFUserRepository : IUserService
    {
        private readonly IConfiguration _config;

        private OrderManagementDBContext context;



        public IEnumerable<UserToCompany> GetAllUserToCompany => context.UserToCompanys.Include(a => a.Company).Include(a=>a.User);

     

        public EFUserRepository(IConfiguration config, OrderManagementDBContext _context)
        {
             _config=config;
            context = _context;
        }

        public User AuthenticateUser(AuthenticateRequest model)
        {
            User user = context.Users.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);
            //appUsers.SingleOrDefault(x => x.UserName == model.Username && x.Password == model.Password);

            return user;
        }
        public AuthenticateResponse Authenticate(User user)
        {
           

            // authentication successful so generate jwt token
            var token = GenerateJWTToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IQueryable<User> Users => context.Users;

        public IEnumerable<User> GetAll()
        {
            return context.Users;
        }

        public User GetById(int id)
        {
            return context.Users.FirstOrDefault(x => x.UserId== id);
        }

        string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
            new Claim("fullName", userInfo.FullName.ToString()),
            new Claim("role",userInfo.UserRole),
            new Claim("UserId",userInfo.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: _config["Jwt: Issuer"],
            audience: _config["Jwt: Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(6),
            signingCredentials: credentials
            );
         
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Company GetCompanyByUser(int userID)
        {
          //  List <UserToCompany> _obj= GetAllUserToCompany.ToList();
            return GetAllUserToCompany.Where(a => a.User.UserId == userID).Select(b=>b.Company).FirstOrDefault();
        }

        public ResultObj AddUser(User model,UserToCompany comp)
        {
            ResultObj res = new ResultObj();

            UserToCompany com = context.UserToCompanys.SingleOrDefault(a => a.Id == 1);
            if (model.UserId == 0)
            {



                context.Users.Add(model);
                context.UserToCompanys.Add(comp);
            }

            else
            {
                User dbEntry = context.Users
                      .FirstOrDefault(p => p.UserId == model.UserId);
                if (dbEntry != null)
                {
                    dbEntry.UserName = model.UserName;
                    dbEntry.Password = model.Password;
                    dbEntry.Opbalance = model.Opbalance;
                    dbEntry.FullName = model.FullName;
                }
            }
        

            try
            {
                context.SaveChanges();
                res.ResultID = 1;
                res.ResultMessage = "Successfully Added !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }

            return res;
        }

        //        public ResultObj ADDPermission(List<PermittedController> permissionList,User user)
        //        {


        //           // User use = context.Users.AsNoTracking().SingleOrDefault(a => a.UserId == user.UserId);

        //            ResultObj obj = new ResultObj();



        //            using (var transaction = context.Database.BeginTransaction())
        //            {
        //                try
        //                {
        //                     context.Database.ExecuteSqlInterpolated($" Delete from PermittedProjAction where UserId={user.UserId}");
        //                    context.Database.ExecuteSqlInterpolated($" Delete from PermittedControllers where UserId={user.UserId}");



        //                    foreach (PermittedController item in permissionList)
        //                    {

        //                       // item.User = null;
        //                       // item.UserId = user.UserId;

        ////                        var localUSer = context.Users
        ////.Local
        ////.FirstOrDefault(c => c.UserId == user.UserId);

        ////                        if (localUSer != null)
        ////                        {
        ////                            item.User= localUSer ;
        ////                           // 👈 existing tracked entity ব্যবহার করুন
        ////                        }
        ////                        else
        ////                        {
        ////                            context.Attach(user);
        ////                        }




        //                        context.AttachRange(item.ProjController);
        //                        context.PermittedControllers.Add(item) ;

        //                        //}
        //                    }

        //                    context.SaveChanges();
        //                    transaction.Commit();

        //                    return new ResultObj { ResultID = 1, ResultMessage = "Permission Saved" };
        //                }
        //                catch (Exception ex)
        //                {
        //                    transaction.Rollback();
        //                    return new ResultObj { ResultID = -1, ResultMessage = ex.ToString() };
        //                }

        //            }

        //        }


        public ResultObj ADDPermission(List<PermittedController> permissionList, int userId)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                context.Database.ExecuteSqlInterpolated(
                    $"DELETE FROM PermittedProjAction WHERE UserId = {userId}");

                context.Database.ExecuteSqlInterpolated(
                    $"DELETE FROM PermittedControllers WHERE UserId = {userId}");

                var connection = context.Database.GetDbConnection();

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();

                foreach (var item in permissionList)
                {
                    int permittedControllerId;

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.Transaction = transaction.GetDbTransaction();
                        cmd.CommandText = @"
                    INSERT INTO PermittedControllers (UserId, ProjControllerID)
                    OUTPUT INSERTED.PermittedControllerID
                    VALUES (@userId, @controllerId)";

                        var p1 = cmd.CreateParameter();
                        p1.ParameterName = "@userId";
                        p1.Value = userId;

                        var p2 = cmd.CreateParameter();
                        p2.ParameterName = "@controllerId";
                        p2.Value = item.ProjController.ProjControllerID;

                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);

                        permittedControllerId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    foreach (var action in item.PermittedProjActions)
                    {
                        context.Database.ExecuteSqlInterpolated($@"
                    INSERT INTO PermittedProjAction
                    (PermittedControllerID, ProjActionID, UserId)
                    VALUES ({permittedControllerId}, {action.ProjActionID}, {userId})
                ");
                    }
                }

                transaction.Commit();

                return new ResultObj
                {
                    ResultID = 1,
                    ResultMessage = "Permission Saved"
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return new ResultObj
                {
                    ResultID = -1,
                    ResultMessage = ex.Message
                };
            }
        }

        //public ResultObj ADDPermission(List<PermittedController> permissionList,int userId)
        //{
        //    ResultObj obj = new ResultObj();




        //    using (var transaction = context.Database.BeginTransaction())
        //    {
        //        try
        //        {


        //            // Delete old permissions
        //            context.Database.ExecuteSqlInterpolated($"DELETE FROM PermittedProjAction WHERE UserId = {userId}");
        //            context.Database.ExecuteSqlInterpolated($"DELETE FROM PermittedControllers WHERE UserId = {userId}");

        //            // Fix tracking issue
        //            foreach (var item in permissionList)
        //            {
        //                item.User = null;      // prevent EF tracking conflict
        //                item.UserId = userId;  // ensure correct FK

        //                if (item.ProjController != null)
        //                {
        //                    context.Attach(item.ProjController);
        //                    //context.Attach(item.User);
        //                }

        //                context.PermittedControllers.Add(item);
        //            }

        //            // Add all permissions at once
        //            //context.PermittedControllers.AddRange(permissionList);

        //            context.SaveChanges();
        //            transaction.Commit();

        //            return new ResultObj
        //            {
        //                ResultID = 1,
        //                ResultMessage = "Permission Saved"
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();

        //            return new ResultObj
        //            {
        //                ResultID = -1,
        //                ResultMessage = ex.Message
        //            };
        //        }
        //    }
        //}
        public ResultObj UpdateCompany(Company model)
        {
            ResultObj res = new ResultObj();

            if (model.CompanyID == 0)
            {

                res.ResultID = -1;
                res.ResultMessage = "Invalid Company Inforamtion !";
            }

            else
            {


                context.Entry(model).State = EntityState.Modified;

            }

            try
            {
                context.SaveChanges();
                context.Entry(model).State = EntityState.Detached;


                context.Entry(model).Reload();
                //res.ResultNo = cashPayment.GeneratedInvoicNo;
                 res.Obj = model;

                res.ResultID = 1;


                res.ResultMessage = "Successfully Added /Updated !";
            }
            catch (Exception ex)
            {
                res.ResultID = -1;
                res.ResultMessage = ex.ToString();
            }
            return res;
        }

        //public Company GetCompanyById(int comID)
        //{
        //   return context.Query<Company>().FromSql(@"SELECT   [CompanyID],[Name],[Address],[Address2],[Shopname],[ShopAddress],[MobileNo1],[MobileNo2] FROM  [Company] where  [CompanyID]={0}", comID).FirstOrDefault(a=>a.CompanyID==comID);
        //}
        public Company GetCompanyById(int comID)
        {
            return context.Company
                .FromSqlInterpolated(
                    $"SELECT [CompanyID],[Name],[Address],[Address2],[Shopname],[ShopAddress],[MobileNo1],[MobileNo2] FROM [Company] WHERE [CompanyID] = {comID}"
                )
                .AsNoTracking() // read-only
                .FirstOrDefault();
        }

        public bool IsControllerAndActionPermitted(string controller, string action,User user)
        {

            bool res = false;


            res = context.PermittedControllers.Any(a => a.UserId == user.UserId  && a.ProjController.ControllerName== controller && a.PermittedProjActions.Any(s => s.UserId == user.UserId && s.ProjAction.ActionName==action));

            return res;


        }



        //        private List<User> appUsers = new List<User>
        //{
        //new User { FullName = "Vaibhav Bhapkar", UserName = "admin", Password = "1234", UserRole = "Admin",UserId=1},
        //new User { FullName = "Test User", UserName = "user", Password = "1234", UserRole = "User", UserId=2}
        //        };
    }
}
