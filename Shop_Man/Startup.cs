//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using OrderManagement.EFRepository;
//using Shop_Man.DB;
//using Shop_Man.EFRepository;
//using Shop_Man.Infrastructure;
//using Shop_Man.Models;
//using Shop_Man.Repository;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Shop_Man
//{
//    public class Startup
//    {

//        public IConfiguration Configuration { get; }
//        public Startup(IConfiguration configuration) => Configuration = configuration;
//        // This method gets called by the runtime. Use this method to add services to the container.
//        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddDbContext<OrderManagementDBContext>(options =>
//         options.UseSqlServer(
//               Configuration.GetConnectionString("DefaultConnection"),
//               sqlOptions => sqlOptions.CommandTimeout(120)


//             ).EnableSensitiveDataLogging()
//         );

//            services.AddScoped<IStoreRepository, EFStoreRepository>();
//            services.AddScoped<IOrderRepository, EFOrderRepository>();
//            services.AddScoped<IUserService, EFUserRepository>();
//            services.AddScoped<IPagePermissionRepository, EFPagePermissionRepository>();
//            services.AddScoped<ICustomerRepository, EFCustomerRepository>();
//            services.AddScoped<ICustomerCategory, EFCustomerCategoryRepository>();
//            services.AddScoped<ICustomerSubCategory, EFCustomerSubCategoryRepository>();
//            services.AddScoped<IDistrictRepository, EFDistrictRepository>();
//            services.AddScoped<IProductsRepositor, EFProductRepository>();
//            //services.AddDistributedMemoryCache();
//            //services.AddSession();
//            services.AddScoped<ISupplierRepository, EFSupplierRepository>();
//            services.AddScoped<ISupplierCategory, EFSupplierCategory>();
//            services.AddScoped<ISupplierSubCategory, EFSupplierSubCategory>();
//            services.AddScoped<ICompanyProductRepository, EFCompanyProductRepository>();
//            services.AddScoped<IPaymentMediumRepository, EFPaymentMediumRepository>();
//            services.AddScoped<ISalesRepository, EFSalesRepository>();
//            services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();
//            services.AddScoped<ICashPaymentRepository, EFCashPaymentRepository>();
//            services.AddScoped<ICashReceiveRepository, EFCashReceiveRepository>();
//            services.AddScoped<IChequeTransactionRepository, EFChequeTransactionRepository>();
//            services.AddScoped<IEmployeeRepository, EFEmployeeRepository>();
//            services.AddScoped<IExpensesRepository, EFExpensesRepository>();
//            services.AddScoped<IDayCloseRepository, EFDayCloseRepository>();
//            services.AddScoped<ISalesReturnRepository, EFSalesReturnRepository>();
//            services.AddScoped<IDBBackupRepository, EFDBBackupRepository>();

//            services.AddScoped<ISalarySheetRepository, EFSalarySheetRepository>();
//            services.AddScoped<IAdjustmentRepository, EFAdjustmentRepository>();
//            services.AddScoped<ILogEntryEditRepository, EFLogEntryEditRepository>();
//            services.AddScoped<ISalesAdjust, EFSalesAdjustRepository>();

//            services.AddScoped<IStockAdjustRepository, EFStockAdjustRepository>();



//            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
//            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//            services.AddAuthorization(config =>
//            {
//                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
//                config.AddPolicy(Policies.User, Policies.UserPolicy());
//            });
//            services.AddMvc()
//   .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
//        {

//            app.UseDeveloperExceptionPage();
//            app.UseStatusCodePages();
//            app.UseStaticFiles();
//            //if (env.IsDevelopment())
//            //{
//            //    app.UseDeveloperExceptionPage();
//            //}
//            app.UseAuthentication();
        
//            app.UseMiddleware<JwtMiddleware>();
//            app.UseMvc(routes => {


//                routes.MapRoute(
//                name: null,
//                template: "",
//                defaults: new
//                {
              
//                    controller = "Home",
//                    action = "LogIn"
//                    // productPage = 1
//                });

//                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");

//            });

//            SeedData.EnsurePopulated(app);
//        }
//    }
//}
