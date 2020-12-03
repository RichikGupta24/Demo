using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactCodeGen.Models
{
    public class OpenApiModels
    {
        public class OpenApiData
        {
            public string Data { get; set; }
        }

        public class OpenApi
        {
            public string Operation { get; set; }
            public string Verb { get; set; }
            public List<Parameter> Param { get; set; }

            public OpenApi()
            {
                Param = new List<Parameter>();
            }
        }

        public class Parameter
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public List<string> Values { get; set; }
            public bool IsRequired { get; set; }
            public int Element { get; set; }
            public List<Parameter> properties { get; set; }

            public Parameter()
            {
                properties = new List<Parameter>();
            }
        }
    }
}
