using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCodeGen.Entities
{
    public class Component
    {
        public string Name { get; set; }
        public List<Field> Fields { get; set; }
    }
}
