using Shop_Man.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Repository
{
   public interface IDBBackupRepository
    {
        ResultObj TakeDBBackup();
    }
}
