namespace RentMyTools.Api.Infrastructure.DataOperations
{
    public interface IDataOperationExecutor
    {
        void Execute(Command command);
        T Execute<T>(Command<T> command);
        T Execute<T>(Query<T> query);
    }
}