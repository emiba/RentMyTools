using Microsoft.AspNetCore.Mvc;
using RentMyTools.Api.Infrastructure.DataOperations;
using RentMyTools.Api.Models;

namespace RentMyTools.Api.Controllers
{
    public abstract class RentMyToolsController : ControllerBase
    {
        protected IDataOperationExecutor DataOperationExecutor;

        public RentMyToolsController(IDataOperationExecutor dataOperationExecutor)
        {
            DataOperationExecutor = dataOperationExecutor;
        }

        public void ExecuteCommand(Command command)
        {
            DataOperationExecutor.Execute(command);
        }

        public T ExecuteCommand<T>(Command<T> command)
        {
            return DataOperationExecutor.Execute(command);
        }

        public T ExecuteQuery<T>(Query<T> query)
        {
            return DataOperationExecutor.Execute(query);
        }
    }
}