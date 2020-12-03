using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCodeGen.Entities
{
    public class APIInfo
    {
        public string APIName { get; set; }
        public string APIUrl { get; set; }
        public string APIDesc { get; set; }
        public string APISchema { get; set; }
        public string APIType { get; set; } //Get, Post
        public Component Component { get; set; }
        public string TemplateType { get; set; } //add, update, delete
    }
}
