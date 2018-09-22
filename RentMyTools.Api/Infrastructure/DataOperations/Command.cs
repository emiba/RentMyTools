using Raven.Client.Documents.Session;

namespace RentMyTools.Api.Infrastructure.DataOperations
{
    public abstract class Command
    {
        public IDocumentSession Session{ get; set; }

        public abstract void Execute();
    }

    public abstract class Command<T> : Command
    {
        public T Result { get; set; }
    }
}