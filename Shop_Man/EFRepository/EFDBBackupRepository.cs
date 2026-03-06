using Microsoft.EntityFrameworkCore;
using Shop_Man.DB;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.EFRepository
{
    public class EFDBBackupRepository : IDBBackupRepository
    {
        private OrderManagementDBContext context;
        public EFDBBackupRepository(OrderManagementDBContext contx)
        {
            context = contx;
        }
        public ResultObj TakeDBBackup()
        {
            try
            {
                context.Database.ExecuteSqlRaw($"EXEC [dbo].[procBackUpDatabaseShop] ");
                return new ResultObj { ResultID = 1, ResultMessage = "Databas Backup Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultObj { ResultID = -1, ResultMessage = "Databas Backup Failed" };
            }
        }
    }
}
