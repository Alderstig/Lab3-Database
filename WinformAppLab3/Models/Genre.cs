﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WinformsAppLab3
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        public int Id { get; set; }
        public string Genre1 { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
