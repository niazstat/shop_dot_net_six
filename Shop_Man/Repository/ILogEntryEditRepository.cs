using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
   public interface ILogEntryEditRepository
    {
        IQueryable<LogEntryEdit> ILogEntryEdits { get; }

        ResultObj SaveLogEntryEdit(LogEntryEdit logEntryEdit);
    }
}
