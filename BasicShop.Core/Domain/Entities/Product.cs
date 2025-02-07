﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Core.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public decimal Price { get; set; }  
        public bool IsActive { get; set; }
        public int QuantityInStock {  get; set; }
        public CartProduct CartProduct { get; set; }
    }
}
