using System;
using System.Collections.Generic;

#nullable disable

namespace WinformsAppLab3
{
    public partial class Employee
    {
        public int Id { get; set; }
        public int? StoreId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public int? CityId { get; set; }
        public string PostalCode { get; set; }
        public string HomePhone { get; set; }

        public virtual City City { get; set; }
        public virtual Store Store { get; set; }
    }
}
