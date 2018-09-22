using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;
using RentMyTools.Api.Infrastructure.DataOperations;
using RentMyTools.Api.Models;

namespace RentMyTools.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : RentMyToolsController
    {
        public ToolsController(IDataOperationExecutor dataOperationExecutor)
            : base(dataOperationExecutor)
        { }

        [HttpGet]
        public ActionResult<IEnumerable<Tool>> Get()
        {
            return Ok(ExecuteQuery(new FindEntitiesQuery<Tool>()));
        }

        [HttpGet("{id}")]
        public ActionResult<Tool> Get(string id)
        {
            var tool = ExecuteQuery(new GetEntityByIdQuery<Tool> { Id = id });
            if (tool == null)
                return NotFound();
            
            return Ok(tool);
        }

        [HttpPost]
        public ActionResult<Tool> Create(Tool tool)
        {
            var response = SaveTool(tool);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        private Tool SaveTool(Tool tool)
        {
            return DataOperationExecutor.Execute(new SaveEntityCommand<Tool>
            {
                EntityToSave = tool,
            });
        }

        [HttpPut]
        public ActionResult<Tool> Update(Tool tool)
        {
            if (string.IsNullOrEmpty(tool.Id))
                return NotFound();

            var response = SaveTool(tool);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                DataOperationExecutor.Execute(new DeleteEntityCommand<Tool>{ Id = id });
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}