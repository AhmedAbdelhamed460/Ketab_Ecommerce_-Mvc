using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ketab_Models.ViewModel
{
    public class ShopingCartVM
    {
        public IEnumerable<ShoppingCart> shoppingCartsList { get; set; }
        public double orderList { get; set; }
    }
}
