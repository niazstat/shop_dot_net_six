using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFPaymentMediumRepository : IPaymentMediumRepository
    {

        private OrderManagementDBContext context;
        public EFPaymentMediumRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }

        public IQueryable<PaymentMedium> PaymentMediums =>context.PaymentMediums;

        public PaymentMedium GetCahPaymentMedium => context.PaymentMediums.SingleOrDefault(a=>a.Name.ToUpper() == "CASH");

        public PaymentMedium GetChequePaymentMedium => context.PaymentMediums.SingleOrDefault(a => a.Name.ToUpper() == "CHEQUE");

        public ResultObj DeleteCustomer(PaymentMedium paymentMedium)
        {
            throw new NotImplementedException();
        }

        public PaymentMedium GetPaymentMedium(int id)
        {
            return PaymentMediums.SingleOrDefault(a => a.PaymentMediumID == id);
        }

        public ResultObj SaveCustomer(PaymentMedium paymentMedium)
        {
            ResultObj res = new ResultObj();
            if (paymentMedium.PaymentMediumID == 0)
            {
                context.PaymentMediums.Add(paymentMedium);


            }
            else
            {

                PaymentMedium dbEntry = context.PaymentMediums
                       .FirstOrDefault(p => p.PaymentMediumID == paymentMedium.PaymentMediumID);
                if (dbEntry != null)
                {
                    dbEntry.Name = paymentMedium.Name;
                  
                }
            }


            try
            {
                context.SaveChanges();
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

        public ResultObj UpdateCustomer(PaymentMedium paymentMedium)
        {
            throw new NotImplementedException();
        }
    }
}
