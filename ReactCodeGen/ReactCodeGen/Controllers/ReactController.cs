using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReactCodeGen.Models;

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

        [HttpPost]
        [Route("/reactCodeGen/api/retrieveAPIInfo")]
        public IActionResult RetrieveAPIInformation([FromBody] string value)
        {
            var apiInfo = RetrieveAPIInfo(value);
            return StatusCode(200,apiInfo);
        }

        private List<APIInfo> RetrieveAPIInfo(string value)
        {
            // This will be replaced by actual open api spec parsing code

            return new List<APIInfo>()
            {
                new APIInfo
                {
                    APIName = "AddPet",
                    APIUrl = "/pet",
                    APIDesc = "Add a new pet to the store",
                    APIType = "POST",
                    TemplateType = "Create",
                    Fields = new List<Field>
                    {
                        new Field
                        {
                            Name="name",
                            ControlType="label"
                        },
                        new Field
                        {
                            Name="status",
                            ControlType="dropdown",
                            Values=new List<string>{"Available","Pending","Sold"}
                        }
                    }
                }
            };            
        }
    }
}
