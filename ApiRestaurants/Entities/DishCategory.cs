using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DishCategory:BaseClass
    {
        public DishCategory()
        {
            IsActive = true;
        }

        public string Name { get; set; }
        public string PathImage { get; set; }
        public bool IsActive { get; set; }
    }
}
