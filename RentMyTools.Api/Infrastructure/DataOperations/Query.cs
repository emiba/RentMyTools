using Raven.Client.Documents.Session;
using RentMyTools.Api.Models;

namespace RentMyTools.Api.Infrastructure.DataOperations
{
    public abstract class Query<T>
    {
        public T Result { get; set; }

        public IDocumentSession Session{ get; set; }

        public abstract void Execute();
    }
}