using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCodeGen.Entities
{
    public class Field
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public List<string> Values { get; set; }
        public bool IsRequired { get; set; }
        public string ControlType { get; set; }
    }
}
