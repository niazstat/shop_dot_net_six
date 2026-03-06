using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Man.Models
{
    public class UserToCompany
    {

        public int Id { get; set; }
        public User User { get; set; }
        public Company Company { get; set; }
        public int IsDeleted { get; set; }
    }
}
