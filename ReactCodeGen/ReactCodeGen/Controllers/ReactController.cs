using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Readers;
using ReactCodeGen.BC;
using ReactCodeGen.Entities;
using ReactCodeGen.Models;
using YamlDotNet.Serialization;
using static ReactCodeGen.Models.OpenApiModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactCodeGen.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {
        
        [HttpGet]
        [Route("/reactCodeGen")]
        public async Task<IActionResult> Get()
        {
            return StatusCode(200, "Welcome to React CodeGen API");
        }

        #region Using YamlDotNet DLL

        //[HttpPost]
        //[Route("/reactCodeGen/api/retrieveAPIInfo")]
        public IActionResult RetrieveAPIInformation(string value)
        {
            Dictionary<object, object> yamlObject;
            ReactControllerBc reactBc = new ReactControllerBc();
            List<APIInfo> apiInfos;
            using (var reader = new StreamReader(@"C:\Users\Naveen\Desktop\UI from Open API Doc\Petstore.yml"))
            //using (var reader = new StreamReader(value)) //todo discuss 
            {
                var deserializer = new Deserializer();
                yamlObject = deserializer.Deserialize<dynamic>(reader.ReadToEnd());
            }

            if (yamlObject != null)
            {
                var apiInfo = reactBc.RetrieveAPIInfo(yamlObject);
                return StatusCode(200, apiInfo);
            }
            return StatusCode(404, "Error");
        }

        [HttpGet]
        [Route("/reactCodeGen/api/retrieveAPISchemas")]
        public IActionResult RetrieveAPISchemas()
        {
            Dictionary<object, object> yamlObject;
            ReactControllerBc reactBc = new ReactControllerBc();
            using (var reader = new StreamReader(@"C:\Users\Naveen\Desktop\UI from Open API Doc\Petstore.yml"))
            //using (var reader = new StreamReader(value)) //todo discuss 
            {
                var deserializer = new Deserializer();
                yamlObject = deserializer.Deserialize<dynamic>(reader.ReadToEnd());
            }
            List<Component> componentSchema;
            if (yamlObject != null)
            {
                componentSchema = reactBc.RetrieveAPISchemas(yamlObject);
                return StatusCode(200, componentSchema);
            }
            return StatusCode(200, "Welcome to React CodeGen API");
        }
        #endregion

        #region Using Microsoft.OpenApi DLL

        [HttpPost]
        [Route("/reactCodeGen/api/retrieveAPIInfo")]
        public OpenApiPath GetOpenApi([FromBody] Entities.OpenApi data)
        {
            var openApi = new OpenApiPath();

            OpenApiDiagnostic diagnostic = new OpenApiDiagnostic();
            //var specData = new StreamReader(@"C:\Users\Naveen\Desktop\UI from Open API Doc\Demo\Petstore.yml").ReadToEnd();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(data.Data)))
            {
                var openApiDocument = new OpenApiStreamReader().Read(ms, out diagnostic);
                foreach (var tag in openApiDocument.Tags)
                {
                    openApi.Tags.Add(new OpenApiTag()
                    {
                        Tag = tag.Name,
                        Description = tag.Description
                    });
                }
                if (openApi.Tags.Count == 0)
                {
                    openApi.Tags.Add(new OpenApiTag()
                    {
                        Tag = "Default"
                    });
                }

                foreach (var path in openApiDocument.Paths)
                {
                    foreach (var operation in path.Value.Operations)
                    {
                        var objOperation = new OpenApiOperation()
                        {
                            Id = operation.Value.OperationId,
                            Name = path.Key,
                            Verb = operation.Key.ToString()
                        };

                        foreach (var param in operation.Value.Parameters)
                        {
                            var objparam = new OpenApiOperationParam()
                            {
                                Name = param.Name,
                                Type = param.Schema.Type,
                                IsRequired = param.Required,
                                Description = param.Description
                            };

                            if (param.Schema.Enum != null && param.Schema.Enum.Count > 0)
                            {
                                foreach (var val in param.Schema.Enum)
                                {
                                    objparam.Values.Add((val as Microsoft.OpenApi.Any.OpenApiString).Value);
                                }
                            }

                            if (param.Schema.Type == "array")
                            {
                                objparam.Type = "string"; // hard code
                                foreach (var val in param.Schema.Items.Enum)
                                {
                                    objparam.Values.Add((val as Microsoft.OpenApi.Any.OpenApiString).Value);
                                }
                            }

                            objOperation.ParamTree.Add(new ParameterTree()
                            {
                                Name = objparam.Name,
                                Type = objparam.Type,
                                Node = 1,
                                Position = "query",
                                Values = objparam.Values
                            });

                            objOperation.Params.Add(objparam);
                        }

                        if (operation.Value.RequestBody != null && operation.Value.RequestBody.Content.Count > 0)
                        {
                            var content = operation.Value.RequestBody.Content.FirstOrDefault();
                            objOperation.BodyParams = GetBodyParam(content.Value.Schema, null, objOperation.ParamTree, 0)[0].Property;
                        }

                        foreach (var server in openApiDocument.Servers)
                        {
                            objOperation.Server.Add(server.Url);
                        }

                        if (operation.Value.Tags != null && operation.Value.Tags.Count > 0)
                        {
                            openApi.Tags.Where(x => x.Tag == operation.Value.Tags[0].Name).FirstOrDefault().Operations.Add(objOperation);
                        }
                        else
                        {
                            openApi.Tags.Where(x => x.Tag == "Default").FirstOrDefault().Operations.Add(objOperation);
                        }
                    }
                }
            }

            return openApi;
        }

        private List<OpenApiOperationParam> GetBodyParam(Microsoft.OpenApi.Models.OpenApiSchema schema, string key, List<ParameterTree> tree, int node, bool isReq=false)
        {
            var paramLst = new List<OpenApiOperationParam>();
            var param = new OpenApiOperationParam()
            {
                Name = key,
                Type = schema.Type,
                Description = schema.Description,
                IsRequired = isReq
            };

            if (schema.Enum != null && schema.Enum.Count>0)
            {
                foreach (var val in schema.Enum)
                {
                    param.Values.Add((val as Microsoft.OpenApi.Any.OpenApiString).Value);
                }
            }

            if (!string.IsNullOrEmpty(key))
            {
                tree.Add(new ParameterTree()
                {
                    Name = param.Name,
                    Type = param.Type,
                    Node = node,
                    Position = "body",
                    Values = param.Values
                });
            }

            if (schema.Properties != null && schema.Properties.Count>0)
            {
                foreach (var prop in schema.Properties)
                {
                    param.Property.AddRange(GetBodyParam(prop.Value, prop.Key, tree, node + 1, schema.Required.Any(x => x.Equals(prop.Key))));
                }
            }

            if (schema.Type == "array" && schema.Items != null)
            {
                //param.Type = schema.Items.Properties.Count == 0 ? "string" : "object"; // hard code
                
                if (schema.Items.Properties != null && schema.Items.Properties.Count > 0)
                {
                    foreach (var prop in schema.Items.Properties)
                    {
                        param.Property.AddRange(GetBodyParam(prop.Value, prop.Key, tree, node + 1, schema.Required.Any(x => x.Equals(key))));
                    }
                }
                else
                {
                    param.Type = schema.Type;
                    param.Type += " of " + schema.Items.Type;
                }
            }

            paramLst.Add(param);

            return paramLst;
        }
        #endregion

    }
}
