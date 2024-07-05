using Microsoft.AspNetCore.Mvc;

namespace DecisionAnalysis.Web.Apis;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Policy = nameof(UserPolicies.AdminPolicy))]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public string[] Get()
    {
        return ["Apple", "Banana"];
    }
}