using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class PermittedController
    {
        public int PermittedControllerID { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        [BindNever]
        public ProjController ProjController { get; set; }

        public ICollection<PermittedProjAction> PermittedProjActions { get; set; }

        [NotMapped]
        public List<PermittedProjAction> PermittedProjActionsList { get; set; }
    }
}
