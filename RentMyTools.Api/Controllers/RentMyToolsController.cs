using Microsoft.AspNetCore.Mvc;
using RentMyTools.Api.Infrastructure.DataOperations;

namespace RentMyTools.Api.Controllers
{
    public abstract class RentMyToolsController : ControllerBase
    {
        protected IDataOperationExecutor DataOperationExecutor;

        protected RentMyToolsController(IDataOperationExecutor dataOperationExecutor)
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