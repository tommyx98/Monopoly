﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public abstract class Property : Tile
    {
        public Player Owner { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int MortgageValue { get; set; }
        public bool Mortgaged { get; set; }
        
    }
}
