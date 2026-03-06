
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.EFRepository
{
    public class EFStoreRepository : IStoreRepository
    {
        private OrderManagementDBContext context;
        public EFStoreRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }

        public List<Product> Products => throw new NotImplementedException();
    }
}
