using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BigShop___.Models
{
    public class WishList
    {
        public int WishListID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual List<Product> Products { get; set; }
       
        [NotMapped]
        public string userId { get; set; }
    }
}