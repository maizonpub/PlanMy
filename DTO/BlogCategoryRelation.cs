﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class BlogCategoryRelation
    {
        public int Id { get; set; }
        public Blog Blog { get; set; }
        public BlogCategory Category { get; set; }
    }
}
