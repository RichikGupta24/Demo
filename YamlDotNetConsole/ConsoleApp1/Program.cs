using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<object, object> yamlObject;
            //using(var reader = new StreamReader(@"C:\Users\Naveen\Desktop\UI from Open API Doc\Petstore.yml"))
            using (var reader = new StreamReader(@"C:\Users\Naveen\Desktop\UI from Open API Doc\openapi.json"))
            {
                var deserializer = new Deserializer();
                yamlObject = deserializer.Deserialize<dynamic>(reader.ReadToEnd());
            }

            if (yamlObject != null)
            {
                foreach (var item in yamlObject)
                {
                    // code here
                }
            }
        }
    }
}
