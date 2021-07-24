using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gaming_Club.Models
{
    public class DynamicModel
    {
        public IEnumerable<Market> Market { get; set; }
        public Market marketItem { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Games> Games { get; set; }
    }
}