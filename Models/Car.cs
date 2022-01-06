using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace InAndOut.Models
{

    public class CarModel
    {
        ///<summary>
        /// Gets or sets Customers.
        ///</summary>
        public List<Car> Cars { get; set; }

        ///<summary>
        /// Gets or sets CurrentPageIndex.
        ///</summary>
        public int CurrentPageIndex { get; set; }

        ///<summary>
        /// Gets or sets PageCount.
        ///</summary>
        public int PageCount { get; set; }
    }
    public class Car
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
       
        public string CarImage { get; set; }

    }
}
