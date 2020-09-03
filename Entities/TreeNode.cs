﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forgeSampleAPI_DotNetCore.Entities
{
    public class TreeNode:INode
    {
        public TreeNode(string id, string text, string type, bool children)
        {
            this.id = id;
            this.text = text;
            this.type = type;
            this.children = children;
        }

        public string id { get; set; }
        public string text { get; set; }
        public string type { get; set; }
        public bool children { get; set; }
    }
}
