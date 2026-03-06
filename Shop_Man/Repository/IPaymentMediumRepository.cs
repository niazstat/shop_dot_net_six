using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
    public interface IPaymentMediumRepository
    {

        IQueryable<PaymentMedium> PaymentMediums { get; }
        ResultObj SaveCustomer(PaymentMedium paymentMedium);
        ResultObj UpdateCustomer(PaymentMedium paymentMedium);
        ResultObj DeleteCustomer(PaymentMedium paymentMedium);
        PaymentMedium  GetCahPaymentMedium { get; }

        PaymentMedium GetChequePaymentMedium { get; }
        PaymentMedium GetPaymentMedium(int id);
    }
}
