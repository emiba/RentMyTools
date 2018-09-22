using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents.Session;
using RentMyTools.Api.Models;

namespace RentMyTools.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        private readonly IDocumentSession _session;

        public ToolsController(IDocumentSession session)
        {
            _session = session;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tool>> Get()
        {
            var data = _session.Query<Tool>();
            return Ok(data.ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult<Tool> GetAction(string id)
        {
            var tool = _session.Load<Tool>(id);
            if (tool == null)
                return NotFound();
            
            return Ok(tool);
        }

        [HttpPost]
        public ActionResult<Tool> Create(Tool tool)
        {
            tool.Id = string.Empty;

            _session.Store(tool);
            _session.SaveChanges();

            return tool;
        }

        [HttpPut]
        public ActionResult<Tool> Update(Tool tool)
        {
            if (string.IsNullOrEmpty(tool.Id))
                return NotFound();

            var entity = _session.Load<Tool>(tool.Id);
            if (entity == null)
                return NotFound();

            entity.Description = tool.Description;
            entity.Title = tool.Title;

            _session.SaveChanges();

            return entity;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var tool = _session.Load<Tool>(id);
            if (tool == null)
                return NotFound();

            _session.Delete(tool);
            _session.SaveChanges();

            return NoContent();
        }
    }
}