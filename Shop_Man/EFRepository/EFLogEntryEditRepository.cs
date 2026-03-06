using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFLogEntryEditRepository : ILogEntryEditRepository
    {
        private OrderManagementDBContext context;
        public EFLogEntryEditRepository(OrderManagementDBContext ctx)
        {
            context = ctx;
        }
        public IQueryable<LogEntryEdit> ILogEntryEdits => context.LogEntryEdits;

        public ResultObj SaveLogEntryEdit(LogEntryEdit logEntryEdit)
        {
            throw new NotImplementedException();
        }
    }
}
