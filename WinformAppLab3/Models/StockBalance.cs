using System;
using System.Collections.Generic;

#nullable disable

namespace WinformsAppLab3
{
    public partial class StockBalance
    {
        public int StoreId { get; set; }
        public string Isbn13 { get; set; }
        public int? Amount { get; set; }

        public virtual Book Isbn13Navigation { get; set; }
        public virtual Store Store { get; set; }
    }
}
