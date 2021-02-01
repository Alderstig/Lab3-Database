using System;
using System.Collections.Generic;

#nullable disable

namespace WinformsAppLab3
{
    public partial class Book
    {
        public Book()
        {
            StockBalances = new HashSet<StockBalance>();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public int? Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? AuthorId { get; set; }
        public int? GenreId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual ICollection<StockBalance> StockBalances { get; set; }
    }
}
