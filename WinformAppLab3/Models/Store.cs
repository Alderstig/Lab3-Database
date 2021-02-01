using System;
using System.Collections.Generic;

#nullable disable

namespace WinformsAppLab3
{
    public partial class Store
    {
        public Store()
        {
            Employees = new HashSet<Employee>();
            StockBalances = new HashSet<StockBalance>();
        }

        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Adress { get; set; }
        public int? CityId { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<StockBalance> StockBalances { get; set; }
    }
}
