using ReactCodeGen.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ReactCodeGen.BC
{
    public class ReactControllerBc
    {
        public List<APIInfo> RetrieveAPIInfo(Dictionary<object, object> yamlObject)
        {
            List<APIInfo> apiInfos = new List<APIInfo>(); 
            foreach (var item in yamlObject)
            {
                    // code here
                 if (item.Key.Equals("paths"))
                 {
                     GenerateApis((Dictionary<object, object>)item.Value, apiInfos);
                 }
             }
            return apiInfos;
         }
            //return new List<APIInfo>()
            //{
            //    new APIInfo
            //    {
            //        APIName = "AddPet",
            //        APIUrl = "/pet",
            //        APIDesc = "Add a new pet to the store",
            //        APIType = "POST",
            //        TemplateType = "Create",
            //        Component = new Component
            //        {
            //            Fields = new  List<Field>{
            //            new Field
            //            {
            //                Name="name",
            //                ControlType="label"
            //            },
            //            new Field
            //            {
            //                Name="status",
            //                ControlType="dropdown",
            //                Values=new List<string>{"Available","Pending","Sold"}
            //            }
            //            }
            //        }
            //    }
            
        

        private void GenerateApis(Dictionary<object, object> apis, List<APIInfo> apiInfos)
        {
            foreach (var apiName in apis)
            {
                foreach (var apiMeta in apiName.Value as Dictionary<object, object>)
                {
                    var apiObject = new APIInfo
                    {
                        APIName = apiName.Key.ToString(),
                        APIType = apiMeta.Key.ToString(),
                    };
                    foreach (var item in apiMeta.Value as Dictionary<object, object>)
                    {
                        if (item.Key.Equals("requestBody"))
                        {
                            foreach (var reqBodyItems in item.Value as Dictionary<object, object>)
                            {
                                if (reqBodyItems.Key.Equals("description"))
                                    apiObject.APIDesc = reqBodyItems.Value.ToString();
                                if (reqBodyItems.Key.Equals("content"))
                                    apiObject.APISchema = GetSchemaPath((Dictionary<object, object>)reqBodyItems.Value);
                            }
                        }
                    }
                    apiInfos.Add(apiObject);
                }
            }
        }

        public List<Component> RetrieveAPISchemas(Dictionary<object, object> yamlObject)
        {
            List<Component> componentSchema;
            foreach (var item in yamlObject)
            {
                // code here
                if (item.Key.Equals("components"))
                {
                    componentSchema = ExploreSchema((Dictionary<object, object>)item.Value);
                    return componentSchema;
                }
            }
            return null; //todo handle 
        }

        private List<Component> ExploreSchema(Dictionary<object, object> schemas)
        {
            List<Component> componentSchema = null;
            foreach (var schema in schemas)
            {
                if (schema.Key.Equals("schemas"))
                {
                    componentSchema = new List<Component>();
                    GenerateComponent((Dictionary<object, object>)schema.Value, componentSchema);
                }
            }
            return componentSchema;
        }

        private void GenerateComponent(Dictionary<object, object> schemaValue, List<Component> componentSchema)
        {
            foreach (var model in schemaValue)
            {
                Component comp = new Component
                {
                    Name = model.Key.ToString()
                };
                var modelValue = model.Value as Dictionary<object, object>;
                var props = modelValue.FirstOrDefault(x => x.Key.Equals("properties")).Value as Dictionary<object, object>;
                if (props != null)
                {
                    comp.Fields = new List<Field>();
                    foreach (var property in props)
                    {
                        comp.Fields.Add(AddPropertyToComponent(property));
                    }
                }
                componentSchema.Add(comp);
            }
        }

        private Field AddPropertyToComponent(KeyValuePair<object, object> property) //todo: required pend
        {
            Field field = new Field
            {
                Name = property.Key.ToString()
            };
            foreach (var item in property.Value as Dictionary<object, object>)
            {
                if (item.Key.Equals("type"))
                    field.DataType = item.Value.ToString();
                if (item.Key.Equals("$ref"))
                    field.DataType = item.Value.ToString().Split('/').Last();
            }
            return field;
        }

        private string GetSchemaPath(Dictionary<object, object> contentValuePairs)
        {
            var reqSchemaObj = (Dictionary<object, object>)contentValuePairs.FirstOrDefault(x => x.Key.Equals("application/json")).Value;
            if (reqSchemaObj != null)
            {
                var schemaName = reqSchemaObj ?? (Dictionary<object, object>)reqSchemaObj.FirstOrDefault(x => x.Key.Equals("schema")).Value;
                return schemaName != null ? schemaName.FirstOrDefault().Value.ToString().Split('/').Last() : null;
            }
            return null;
        }
    }
}
