using System;
using System.Collections.Generic;

#nullable disable

namespace WinformsAppLab3
{
    public partial class City
    {
        public City()
        {
            Employees = new HashSet<Employee>();
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public string City1 { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
