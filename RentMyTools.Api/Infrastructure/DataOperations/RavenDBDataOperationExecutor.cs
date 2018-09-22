using Raven.Client.Documents;

namespace RentMyTools.Api.Infrastructure.DataOperations
{
    public class RavenDBDataOperationExecutor : IDataOperationExecutor
    {
        private readonly IDocumentStore _store;
        public RavenDBDataOperationExecutor(IDocumentStore store)
        {
            _store = store;
        }

        public void Execute(Command command)
        {
            using(var session = _store.OpenSession())
            {
                command.Session = session;
                command.Execute();
                session.SaveChanges();
            }
        }

        public T Execute<T>(Command<T> command)
        {
            Execute(command as Command);

            return command.Result;
        }

        private void ExecuteCommandInner(Command command)
        {
        }

        public T Execute<T>(Query<T> query)
        {
            using(var session = _store.OpenSession())
            {
                query.Session = session;
                query.Execute();
                return query.Result;
            }
        }
    }
}