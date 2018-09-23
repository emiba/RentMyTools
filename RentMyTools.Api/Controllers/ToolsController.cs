using Microsoft.AspNetCore.Mvc;
using RentMyTools.Api.Infrastructure.DataOperations;
using RentMyTools.Models;

namespace RentMyTools.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : RentMyToolsEntityController<Tool>
    {
        public ToolsController(IDataOperationExecutor dataOperationExecutor)
            : base(dataOperationExecutor)
        { }
    }
}