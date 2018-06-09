﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyJSON.Nodes
{
    public class ArrayNode : INode
    {
        public int index { get; set; }
        public List<INode> Values { get; set; }
    }
}

