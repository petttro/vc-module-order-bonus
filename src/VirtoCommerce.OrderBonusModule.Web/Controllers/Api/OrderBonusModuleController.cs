using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtoCommerce.OrderBonusModule.Core;

namespace VirtoCommerce.OrderBonusModule.Web.Controllers.Api
{
    [Route("api/order-bonus-module")]
    public class OrderBonusModuleController : Controller
    {
        // GET: api/order-bonus-module
        /// <summary>
        /// Get message
        /// </summary>
        /// <remarks>Return "Hello world!" message</remarks>
        [HttpGet]
        [Route("")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public ActionResult<string> Get()
        {
            return Ok(new { result = "Hello world!" });
        }
    }
}
