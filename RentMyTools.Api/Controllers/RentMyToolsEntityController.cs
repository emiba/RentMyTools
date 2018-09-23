using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentMyTools.Api.Infrastructure.DataOperations;
using RentMyTools.Models;

namespace RentMyTools.Api.Controllers
{
    public abstract class RentMyToolsEntityController<TEntity> : RentMyToolsController where TEntity : Entity, new()
    {
        protected RentMyToolsEntityController(IDataOperationExecutor dataOperationExecutor) 
            : base(dataOperationExecutor)
        { }

        [HttpGet]
        public virtual ActionResult<IEnumerable<TEntity>> Get()
        {
            return Ok(ExecuteQuery(new FindEntitiesQuery<TEntity>()));
        }

        [HttpGet("{id}")]
        public virtual ActionResult<TEntity> Get(string id)
        {
            var tool = ExecuteQuery(new GetEntityByIdQuery<TEntity> { Id = id });
            if (tool == null)
                return NotFound();

            return Ok(tool);
        }

        [HttpPost]
        public ActionResult<TEntity> Create(TEntity entity)
        {
            var response = SaveEntity(entity);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        private TEntity SaveEntity(TEntity entity)
        {
            return DataOperationExecutor.Execute(new SaveEntityCommand<TEntity>
            {
                EntityToSave = entity,
            });
        }

        [HttpPut]
        public ActionResult<TEntity> Update(TEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
                return NotFound();

            var response = SaveEntity(entity);
            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                DataOperationExecutor.Execute(new DeleteEntityCommand<TEntity> { Id = id });
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
