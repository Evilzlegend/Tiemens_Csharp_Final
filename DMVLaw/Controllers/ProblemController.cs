using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DMVLaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        [HttpPost]
        //[Authorize(Roles = "DMV Personnel,Law Enforcement")]
        public OkObjectResult Post([FromBody] List<KeyValuePair<string, string>> value)
        {
            Dictionary<string, string> primary = new Dictionary<string, string>();
            Dictionary<string, string> secondary = new Dictionary<string, string>();
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();
            foreach (var element in value)
            {
                if (primary.ContainsKey(element.Key))
                {
                    if (secondary.ContainsKey(element.Key))
                    {
                        foreach (var second in secondary)
                        {
                            if (second.Key == element.Key)
                            {
                                int newNum = int.Parse(second.Value) + 1;
                                secondary[second.Key] = newNum.ToString();
                            }
                        }
                    }
                    else
                    {
                        secondary.Add(element.Key, "2");
                    }
                }
                else
                {
                    primary.Add(element.Key, element.Value);
                }
            }

            results.Add(primary);
            results.Add(secondary);

            return Ok(results);

        }
    }
}