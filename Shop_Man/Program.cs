using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.DependencyInjection;
using OrderManagement.EFRepository;
using Shop_Man.DB;
using Shop_Man.EFRepository;
using Shop_Man.Infrastructure;
using Shop_Man.Models;
using Shop_Man.Repository;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;

// --------------------
// Services
// --------------------

builder.Services.AddDbContext<OrderManagementDBContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.CommandTimeout(120))
    .EnableSensitiveDataLogging()
);

// Repositories
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddScoped<IUserService, EFUserRepository>();
builder.Services.AddScoped<IPagePermissionRepository, EFPagePermissionRepository>();
builder.Services.AddScoped<ICustomerRepository, EFCustomerRepository>();
builder.Services.AddScoped<ICustomerCategory, EFCustomerCategoryRepository>();
builder.Services.AddScoped<ICustomerSubCategory, EFCustomerSubCategoryRepository>();
builder.Services.AddScoped<IDistrictRepository, EFDistrictRepository>();
builder.Services.AddScoped<IProductsRepositor, EFProductRepository>();
builder.Services.AddScoped<ISupplierRepository, EFSupplierRepository>();
builder.Services.AddScoped<ISupplierCategory, EFSupplierCategory>();
builder.Services.AddScoped<ISupplierSubCategory, EFSupplierSubCategory>();
builder.Services.AddScoped<ICompanyProductRepository, EFCompanyProductRepository>();
builder.Services.AddScoped<IPaymentMediumRepository, EFPaymentMediumRepository>();
builder.Services.AddScoped<ISalesRepository, EFSalesRepository>();
builder.Services.AddScoped<IPurchaseRepository, EFPurchaseRepository>();
builder.Services.AddScoped<ICashPaymentRepository, EFCashPaymentRepository>();
builder.Services.AddScoped<ICashReceiveRepository, EFCashReceiveRepository>();
builder.Services.AddScoped<IChequeTransactionRepository, EFChequeTransactionRepository>();
builder.Services.AddScoped<IEmployeeRepository, EFEmployeeRepository>();
builder.Services.AddScoped<IExpensesRepository, EFExpensesRepository>();
builder.Services.AddScoped<IDayCloseRepository, EFDayCloseRepository>();
builder.Services.AddScoped<ISalesReturnRepository, EFSalesReturnRepository>();
builder.Services.AddScoped<IDBBackupRepository, EFDBBackupRepository>();
builder.Services.AddScoped<ISalarySheetRepository, EFSalarySheetRepository>();
builder.Services.AddScoped<IAdjustmentRepository, EFAdjustmentRepository>();
builder.Services.AddScoped<ILogEntryEditRepository, EFLogEntryEditRepository>();
builder.Services.AddScoped<ISalesAdjust, EFSalesAdjustRepository>();
builder.Services.AddScoped<IStockAdjustRepository, EFStockAdjustRepository>();

// Cart + HttpContext
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Authorization
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
    config.AddPolicy(Policies.User, Policies.UserPolicy());
});

//// MVC + JSON
//builder.Services.AddControllersWithViews()
//    .AddNewtonsoftJson(options =>
//        options.SerializerSettings.ReferenceLoopHandling =
//            Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
var app = builder.Build();

// --------------------
// Middleware pipeline
// --------------------

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStatusCodePages();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

// Routes
app.MapControllerRoute(
    name: "login",
    pattern: "",
    defaults: new { controller = "Home", action = "LogIn" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");

// Seed data
SeedData.EnsurePopulated(app);

app.Run();
