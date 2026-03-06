using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models.SQLView_SP
{
    public class SP_Yearly_Profit_Loss
    {

        public int YearName { get; set; }

       


        public decimal SalesAmount { get; set; }
        public decimal AddLessAmount { get; set; }
        public decimal ReturnAmount { get; set; }
        public decimal SalesAdjustAmount { get; set; }
        public decimal SalesCommision { get; set; }
        public decimal TotalSackNoFee { get; set; }
        public decimal TransportCost { get; set; }

        public decimal TotalAllRecvSales  { get; set; }


        public decimal PurchaseAmount { get; set; }
        public decimal RetPurchaseAmount { get; set; }

        public decimal PurchaseCommission { get; set; }


        public decimal CheckPayment { get; set; }
        public decimal CashPayment { get; set; }




        public decimal StockAdjustAmount { get; set; }






     //   public decimal ReceiveAmount { get; set; }  totlallrecevamountsales
        public decimal CashReceiveAmount { get; set; }
        public decimal CheckRecev { get; set; }



        public decimal SalaryAmount { get; set; }
        public decimal ExpensesAmount { get; set; }

        public decimal TotalAdjustAmountSupplier { get; set; }
        public decimal TotalClosingShortAmountSupplier { get; set; }
        public decimal TotalRejectGoodsAmountSupplier { get; set; }
        public decimal TotalAdjustAmountCustomer { get; set; }
        public decimal TotalClosingShortAmountCustomer { get; set; }
        public decimal TotalRejectGoodsAmountCustomer { get; set; }



        public decimal TotalcostAmountOfsoldItems { get; set; }
        public decimal TotalcostAmountOfReturnedItems { get; set; }

        public decimal SalesQty { get; set; }
        public decimal ReturnQty { get; set; }


        public decimal PurchaseQty { get; set; }
        public decimal TotalCheqPaymentCustYear { get; set; }
        public decimal TotalCashPaymentCustYear { get; set; }
        public decimal TotalAllSupplierPrevBalanceYear { get; set; }
        public decimal TotalAllCustomerPrevBalanceYear { get; set; }


        public decimal TotalStockQty { get; set; }
        public decimal TotalStockValue { get; set; }

        public decimal Dayclose { get; set; }

        [NotMapped]
        public decimal NetSalesQty { get { return SalesQty - ReturnQty; } }



        [NotMapped]
        public decimal SalesWitoutReturn { get { return SalesAmount  - ReturnAmount ; } }


        [NotMapped]
        public decimal NetSales { get { return SalesAmount  - ReturnAmount + AddLessAmount - SalesCommision - TotalAdjustAmountCustomer - TotalClosingShortAmountCustomer- TotalRejectGoodsAmountCustomer; } }


        [NotMapped]
        public decimal TotalPurchaseAmount { get { return PurchaseAmount-PurchaseCommission- RetPurchaseAmount; } }
        [NotMapped]
        public decimal NetPurchaseAmount { get { return TotalPurchaseAmount-TotalAdjustAmountSupplier-TotalClosingShortAmountSupplier-TotalRejectGoodsAmountSupplier; } }


        [NotMapped]
        public decimal CurrentYearSupplierBalance { get { return NetPurchaseAmount-CashPayment- CheckPayment; } }

        [NotMapped]
        public decimal TotalSupplierBalance { get { return CurrentYearSupplierBalance + TotalAllSupplierPrevBalanceYear; } }



        [NotMapped]
        public decimal ReceiveFromCustomer { get { return TotalAllRecvSales+ CashReceiveAmount+ CheckRecev+ TotalSackNoFee; } }

        [NotMapped]
        public decimal NetReceiveFromCustomer { get { return ReceiveFromCustomer- ExpensesAmount- StockAdjustAmount-SalesAdjustAmount; } }

        [NotMapped]
       public decimal Profit { get { return SalesAmount - (TotalcostAmountOfsoldItems - TotalcostAmountOfReturnedItems) - ReturnAmount + AddLessAmount - SalesCommision ; } }


        [NotMapped]
        public decimal NetProfit { get { return Profit  - TotalRejectGoodsAmountCustomer - TotalClosingShortAmountCustomer- TotalAdjustAmountCustomer - SalesAdjustAmount - StockAdjustAmount- ExpensesAmount- SalaryAmount; } }

        [NotMapped]
        public decimal CustomerBalanceCurrentYear { get { return SalesAmount -SalesCommision + (TotalSackNoFee +TransportCost+AddLessAmount- TotalAllRecvSales-CashReceiveAmount-CheckRecev+ TotalCheqPaymentCustYear+ TotalCashPaymentCustYear - TotalAdjustAmountCustomer - TotalClosingShortAmountCustomer - TotalRejectGoodsAmountCustomer-ReturnAmount-SalesAdjustAmount); } }


        [NotMapped]
        public decimal TotalCustomerBalance { get { return CustomerBalanceCurrentYear+TotalAllCustomerPrevBalanceYear; } }

    }
}
