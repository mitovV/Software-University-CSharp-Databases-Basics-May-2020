﻿namespace ProductShop.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
            = new HashSet<CategoryProduct>();
    }
}
