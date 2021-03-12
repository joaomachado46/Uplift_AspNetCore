using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uplift.DataModels.ViewModels
{
    public class CartVM
    {
        public List<Service> ServiceList { get; set; }

        public OrderHeader OrderHeaders { get; set; }
    }
}
