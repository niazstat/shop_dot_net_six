using Microsoft.AspNetCore.Mvc;
using Shop_Man.Models;
using Shop_Man.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Controllers
{
    public class DBBackupController : Controller
    {

        private IDBBackupRepository backupRepository;
        public DBBackupController(IDBBackupRepository _backupRepository)
        {
            backupRepository = _backupRepository;
        }
        public IActionResult Index()
        {

            ResultObj res = backupRepository.TakeDBBackup();
            return View(res);
        }
    }
}
