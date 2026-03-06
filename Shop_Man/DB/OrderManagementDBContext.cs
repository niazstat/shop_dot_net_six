using Microsoft.EntityFrameworkCore;
using Shop_Man.Models;
using Shop_Man.Models.SQLView_SP;
using Shop_Man.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.DB
{
    public class OrderManagementDBContext:DbContext
    {
        public OrderManagementDBContext()
        {
        }
        public OrderManagementDBContext(DbContextOptions<OrderManagementDBContext> options):base(options)
        {
        }

    

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToCompany> UserToCompanys { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<ProjController> ProjControllers { get; set; }

        public DbSet<PermittedController> PermittedControllers { get; set; }
        public DbSet<CustomerCategory> CustomerCategorys { get; set; }
        public DbSet<CustomerSubCategory> CustomerSubCategorys { get; set; }

        public DbSet<UOM> UOMs { get; set; }

        public DbSet<ProdCoCategory> ProdCoCategorys { get; set; }

        public DbSet<ProdType> ProdTypes { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ProdName> ProdNames { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<SupplierCategory> SupplierCategorys { get; set; }
        public DbSet<SupplierSubCategory> SupplierSubCategorys { get; set; }
        public DbSet<CompanyProduct> CompanyProducts { get; set; }
        public DbSet<PaymentMedium> PaymentMediums { get; set; }
        public DbSet<SalesHead> SalesHeads { get; set; }
        public DbSet<SalesDetails> SalesDetailss { get; set; }

        public DbSet<PurchaseHead> PurchaseHeads { get; set; }
        public DbSet<PurchaseDetails> PurchaseDetails { get; set; }

        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }


        public DbSet<CashReceive> CashReceive { get; set; }
        public DbSet<CashPayment> CashPayments { get; set; }
        public DbSet<ChequeTransaction> ChequeTransactions { get; set; }

        public DbSet<Designation> Designations { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ExpensesHead> ExpensesHeads { get; set; }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<DayClose> DayCloses { get; set; }

        public DbSet<SalesReturn> SalesReturns { get; set; }
        public DbSet<SalesReturnDetails> SalesReturnDetails { get; set; }
        public DbSet<SalarySheet> SalarySheets { get; set; }
        public DbSet<SalarySheetHead> SalarySheetHeads { get; set; }

        public DbSet<Adjustment> Adjustments { get; set; }

        public DbSet<DayCloseDetails> DayCloseDetailses { get; set; }
        public DbSet<LimitSetting> LimitSettings { get; set; }

        public DbSet<DeletedSayClose> DeletedSayCloses { get; set; }
        public DbSet<LogEntryEdit> LogEntryEdits { get; set; }

        public DbSet<YearClose> YearCloses { get; set; }

        public DbSet<YearCloseDetails> YearCloseDetails { get; set; }

        public DbSet<YearItemStockClose> YearItemStockCloses { get; set; }

        public DbSet<YearItemStockCloseDetails> YearItemStockCloseDetails { get; set; }

        public DbSet<YearCloseCustomerSumm> YearCloseCustomerSumms { get; set; }
        public DbSet<YearCloseCustomerDetails> YearCloseCustomerDetailses { get; set; }

        public DbSet<ItemsClosing> ItemsClosings { get; set; }

        public DbSet<Tmp_ViewAllCustomerCLose> Tmp_ViewAllCustomerCLose { get; set; }

        public DbSet<SalesAdjust> SalesAdjusts { get; set; }
        public DbSet<SalesAdjustDetails> SalesAdjustDetails { get; set; }


        public DbSet<StockAdjustHead> StockAdjustHead { get; set; }
        public DbSet<StockAdjustDetails> StockAdjustDetails { get; set; }
        public DbSet<SP_Cash_Transaction_Details> CashTransactionDetails { get; set; }


        public DbSet<SP_Current_Stock> SP_Current_Stock { get; set; }
        public DbSet<CustomerPrevBalancce_SP> CustomerPrevBalancce_SP { get; set; }
        public DbSet<SP_CustomerBalance_Datewise> SP_CustomerBalance_Datewise { get; set; }
        public DbSet<SP_CustomerBalance_Details> SP_CustomerBalance_Details { get; set; }
        public DbSet<SP_CustomerBalance_ALL> SP_CustomerBalance_ALL { get; set; }

        public DbSet<SP_SupplierBalance_ALL> SP_SupplierBalance_ALL { get; set; }
        public DbSet<SP_SuppierPrevBalancce> SP_SuppierPrevBalancce { get; set; }
        public DbSet<SP_SuppierBalancceDetails> SP_SuppierBalancceDetails { get; set; }
        public DbSet<SP_SupplierBalance_Datewise> SP_SupplierBalance_Datewise { get; set; }
        public DbSet<SP_Expenses_Datewise> SP_Expenses_Datewise { get; set; }
        public DbSet<SP_Employee_Salary_and_Expenses> SP_Employee_Salary_and_Expenses { get; set; }

        public DbSet<SP_Entry_DayClose> SP_Entry_DayClose { get; set; }
        public DbSet<SP_Process_DayClose> SP_Process_DayClose { get; set; }

        public DbSet<SP_View_DayClose_Details> SP_View_DayClose_Details { get; set; }
        public DbSet<SP_View_DayClose_SALES_PUR_Details> SP_View_DayClose_SALES_PUR_Details { get; set; }
        public DbSet<SP_Process_Delete_DayClose> SP_Process_Delete_DayClose { get; set; }

        public DbSet<SP_Current_Stock_Article_Size> SP_Current_Stock_Article_Size { get; set; }
        public DbSet<SP_Process_YearClose> SP_Process_YearClose { get; set; }
        public DbSet<SP_Employee_Salary_and_Expenses_SingleEmployee> SP_Employee_Salary_and_Expenses_SingleEmployee { get; set; }
        public DbSet<SP_Entry_Year_Close> SP_Entry_Year_Close { get; set; }

        public DbSet<SP_CustomersWithLastClosingYear> SP_CustomersWithLastClosingYear { get; set; }
        public DbSet<SP_View_YearClose_Details> SP_View_YearClose_Details { get; set; }

        public DbSet<SP_Entry_Customer_Year_Close> SP_Entry_Customer_Year_Close { get; set; }
        public DbSet<SP_Process_Customer_Year_Close> SP_Process_Customer_Year_Close { get; set; }
        public DbSet<SP_Datewise_Stock> SP_Datewise_Stock { get; set; }

        public DbSet<View_Customer_And_YearClosing> View_Customer_And_YearClosing { get; set; }
        public DbSet<SP_Customer_Close_SizeDetails> SP_Customer_Close_SizeDetails { get; set; }

        public DbSet<SP_View_Customer_Year_Close> SP_View_Customer_Year_Close { get; set; }

        public DbSet<SP_View_ALL_Customer_Year_Close> SP_View_ALL_Customer_Year_Close { get; set; }
        public DbSet<SP_View_DayClose_Yearwise_Actual> SP_View_DayClose_Yearwise_Actual { get; set; }
        public DbSet<SP_View_DayClose_Daywise_Actual> SP_View_DayClose_Daywise_Actual { get; set; }
        public DbSet<SP_Yearly_Profit_Loss> SP_Yearly_Profit_Loss { get; set; }
        public DbSet<View_Item_Close> View_Item_Close { get; set; }

        public DbSet<LastEntryDates> LastEntryDates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      
            //Auto Increment
            modelBuilder.HasSequence<int>("CustomerNoAutoNumbering")
                .StartsAt(1000);


            modelBuilder.Entity<Customer>().Property(e => e.CustomerNo)

.HasComputedColumnSql(@"CONVERT(VARCHAR, 1000 +  CustomerID )");
           // .HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,OrderDate))+'-'+CONVERT( VARCHAR,OrderNo)");

            modelBuilder.Entity<Order>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
         

            modelBuilder.Entity<Supplier>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Customer>().Property<DateTime>("EntryDatetime")
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Supplier>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<SalesHead>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
            modelBuilder.Entity<SalesHead>().Property(e => e.GeneratedSalesNo).HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,SalesDate))+'-'+CONVERT( VARCHAR,AutoSalesNo)");
            modelBuilder.Entity<SalesHead>().HasIndex(e => e.GeneratedSalesNo);
            modelBuilder.Entity<SalesDetails>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");


            modelBuilder.Entity<PurchaseHead>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
            modelBuilder.Entity<PurchaseHead>().Property(e => e.GeneratedPurchaseHeadNo).HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,PurchaseDate))+'-'+CONVERT( VARCHAR,AutoPurchaseHeadNo)");
           
            modelBuilder.Entity<PurchaseHead>().HasIndex(e => e.GeneratedPurchaseHeadNo);
            modelBuilder.Entity<PurchaseDetails>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");


            modelBuilder.Entity<CashPayment>().Property<DateTime>("EntryDate").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<CashPayment>().Property<DateTime>("EntryDate").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<ChequeTransaction>().Property<DateTime>("EntryDate").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Employee>().Property<DateTime>("EntryDate").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<ExpensesHead>().Property<DateTime>("EntryDate").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Expense>().Property<DateTime>("EntryDate").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<DayClose>().Property<DateTime>("EntryDate").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<YearCloseCustomerDetails>().Property<DateTime>("EntryDate").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<YearCloseCustomerSumm>().Property<DateTime>("EntryDate").HasDefaultValueSql("getdate()");
            modelBuilder.Entity<CashPayment>().Property(e => e.InvoicNo).HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,PaymentDate))+'-'+CONVERT( VARCHAR,InvoicNoSL)");

            modelBuilder.Entity<CashReceive>().Property(e => e.InvoicNo).HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,ReceiveDate))+'-'+CONVERT( VARCHAR,InvoicNoSL)");
            modelBuilder.Entity<ChequeTransaction>().Property(e => e.InvoicNo).HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,TranDate))+'-'+CONVERT( VARCHAR,InvoicNoSL)");

            modelBuilder.Entity<Expense>().Property(e => e.GeneratedAutoSLNo).HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,TranDate))+'-'+CONVERT( VARCHAR,AutoSLNo)");

            modelBuilder.Entity<Employee>().Property(e => e.GeneratedCode)

.HasComputedColumnSql(@"CONVERT(VARCHAR, 1000 +  EmployeeID )");

            modelBuilder.Entity<SalesReturn>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
            modelBuilder.Entity<SalesReturn>().Property(e => e.GeneratedReturnNo).HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,ReturnDate))+'-'+CONVERT( VARCHAR,AutoReturnNo)");
            modelBuilder.Entity<SalesReturn>().HasIndex(e => e.GeneratedReturnNo);
            modelBuilder.Entity<SalarySheet>().Property(e => e.GeneratedSalarySheetNo).HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,PayDate))+'-'+CONVERT( VARCHAR,AutoSalarySheetNo)");
            modelBuilder.Entity<Adjustment>().Property(e => e.InvoicNo).HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,PaymentDate))+'-'+CONVERT( VARCHAR,InvoicNoSL)");

            modelBuilder.Entity<SalesAdjust>().Property(e => e.GeneratedSalesAdjustNo).HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,SalesAdjustDate))+'-'+CONVERT( VARCHAR,AutoSalesAdjustNo)");
            modelBuilder.Entity<StockAdjustHead>().Property(e => e.GeneratedStockAdjustNo).HasComputedColumnSql(@"CONVERT( VARCHAR,DATEPART(YEAR,AdjustDate))+'-'+CONVERT( VARCHAR,AutoStockAdjust)");

            modelBuilder.Entity<SalesReturnDetails>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<SalarySheet>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<SalarySheetHead>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Adjustment>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
            modelBuilder.Entity<DayCloseDetails>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
            modelBuilder.Entity<LimitSetting>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
            modelBuilder.Entity<DeletedSayClose>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
            modelBuilder.Entity<LogEntryEdit>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<YearClose>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
            modelBuilder.Entity<YearCloseDetails>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
            modelBuilder.Entity<YearItemStockClose>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<YearItemStockCloseDetails>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");
            modelBuilder.Entity<ItemsClosing>().Property<DateTime>("EntryDate")
.HasDefaultValueSql("getdate()");

            modelBuilder.Entity<SP_Cash_Transaction_Details>().HasNoKey();


            modelBuilder.Entity<SP_Current_Stock>().HasNoKey();
            modelBuilder.Entity<CustomerPrevBalancce_SP>().HasNoKey();
            modelBuilder.Entity<SP_CustomerBalance_Datewise>().HasNoKey();
            modelBuilder.Entity<SP_CustomerBalance_Details>().HasNoKey();
            modelBuilder.Entity<SP_CustomerBalance_ALL>().HasNoKey();

            modelBuilder.Entity<SP_SupplierBalance_ALL>().HasNoKey();
            modelBuilder.Entity<SP_SuppierPrevBalancce>().HasNoKey();
            modelBuilder.Entity<SP_SuppierBalancceDetails>().HasNoKey();
            modelBuilder.Entity<SP_SupplierBalance_Datewise>().HasNoKey();
            modelBuilder.Entity<SP_Expenses_Datewise>().HasNoKey();
            modelBuilder.Entity<SP_Employee_Salary_and_Expenses>().HasNoKey();

            modelBuilder.Entity<SP_Entry_DayClose>().HasNoKey();
            modelBuilder.Entity<SP_Process_DayClose>().HasNoKey();

            modelBuilder.Entity<SP_View_DayClose_Details>().HasNoKey();
            modelBuilder.Entity<SP_View_DayClose_SALES_PUR_Details>().HasNoKey();
            modelBuilder.Entity<SP_Process_Delete_DayClose>().HasNoKey();

            modelBuilder.Entity<SP_Current_Stock_Article_Size>().HasNoKey();
            modelBuilder.Entity<SP_Process_YearClose>().HasNoKey();
            modelBuilder.Entity<SP_Employee_Salary_and_Expenses_SingleEmployee>().HasNoKey();
            modelBuilder.Entity<SP_Entry_Year_Close>().HasNoKey();

            modelBuilder.Entity<SP_CustomersWithLastClosingYear>().HasNoKey();
            modelBuilder.Entity<SP_View_YearClose_Details>().HasNoKey();

            modelBuilder.Entity<SP_Entry_Customer_Year_Close>().HasNoKey();
            modelBuilder.Entity<SP_Process_Customer_Year_Close>().HasNoKey();
            modelBuilder.Entity<SP_Datewise_Stock>().HasNoKey();

            modelBuilder.Entity<View_Customer_And_YearClosing>().HasNoKey();
            modelBuilder.Entity<SP_Customer_Close_SizeDetails>().HasNoKey();

            modelBuilder.Entity<SP_View_Customer_Year_Close>().HasNoKey();

            modelBuilder.Entity<SP_View_ALL_Customer_Year_Close>().HasNoKey();
            modelBuilder.Entity<SP_View_DayClose_Yearwise_Actual>().HasNoKey();
            modelBuilder.Entity<SP_View_DayClose_Daywise_Actual>().HasNoKey();
            modelBuilder.Entity<SP_Yearly_Profit_Loss>().HasNoKey();
            modelBuilder.Entity<View_Item_Close>().HasNoKey();

            modelBuilder.Entity<LastEntryDates>().HasNoKey();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                //entityType.SetTableName(entityType.DisplayName());

                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

        }
    }
}
