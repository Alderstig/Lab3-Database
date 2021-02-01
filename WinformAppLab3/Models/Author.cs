using System;
using System.Collections.Generic;

#nullable disable

namespace WinformsAppLab3
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
